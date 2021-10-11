using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour
{
    [SerializeField] float _walkSpeed = 5f;
    [SerializeField] float _runSpeed = 10f;
    [SerializeField] float _angularSpeed = 30f;
    [SerializeField] Definition.State _state;

    [SerializeField] bool _hasTargetPoint = false;
    [SerializeField] Vector3 _destPos;
    [SerializeField] float _maxRange = 50f;

    Animator _animator;
    NavMeshAgent _navMeshAgent;

    public Definition.State State
    {
        get { return _state; }
        set
        {
            // 무분별한 State 변경 방지
            if (value == _state)
                return;

            _state = value;
            switch (_state)
            {
                case Definition.State.Died:
                    _animator.CrossFade("Die", 0.05f);  // Trigger Animation
                    break;
                case Definition.State.Idle:
                    _animator.CrossFade("Idle", 0.05f);
                    break;
                case Definition.State.Walking:
                    _animator.CrossFade("Walk", 0.05f);
                    break;
                case Definition.State.Running:
                    _animator.CrossFade("Run", 0.05f);
                    break;
            }
        }
    }

    // void OnTriggerStay(Collider other)
    // {
    //     // Die
    //     if (Manager.Game.GetWorldObjectType(other.gameObject) == Definition.WorldObject.Player && Manager.Player.GetPlayerState(other.gameObject) == Definition.State.Attack)
    //         State = Definition.State.Died;
    // }

    void Start()
    {
        // Get Component
        _animator = GetComponent<Animator>();
        _navMeshAgent = gameObject.GetOrAddComponent<NavMeshAgent>();
        _navMeshAgent.isStopped = false;

        State = Definition.State.Idle;

        StartCoroutine(DefineTargetPoint());
    }

    void Update()
    {
        switch (_state)
        {
            case Definition.State.Died:
                UpdateDied();
                break;
            case Definition.State.Idle:
                UpdateIdle();
                break;
            case Definition.State.Walking:
            case Definition.State.Running:
                UpdateMoving();
                break;
        }
    }

    IEnumerator DefineTargetPoint()
    {
        while (true)
        {
            float randSeconds = Random.Range(5f, 10f);
            yield return new WaitForSeconds(randSeconds);
            // 5 ~ 10초 뒤

            if (State == Definition.State.Died)
                break;

            Vector3 point;
            if (Manager.Game.RandomPoint(transform.position, _maxRange, out point, routineCount: 30))
            {
                _destPos = point;
                _hasTargetPoint = true;     // 움직여라!
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            }
        }
    }

    #region UpdateState
    void UpdateDied()
    {
        if (_navMeshAgent.isStopped == false)
        {
            _navMeshAgent.isStopped = true;
            this.enabled = false;
        }
    }
    void UpdateIdle()
    {
        if (State == Definition.State.Died)
            return;

        if (_hasTargetPoint == true)
        {
            int randNum = Random.Range(1, 100);
            if (randNum <= 80)
                State = Definition.State.Walking;   // 80% 확률
            else
                State = Definition.State.Running;   // 20% 확률
        }
    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            _navMeshAgent.ResetPath();
            _hasTargetPoint = false;
            State = Definition.State.Idle;
        }
        else
        {
            _navMeshAgent.SetDestination(_destPos);
            if (State == Definition.State.Walking)
                _navMeshAgent.speed = _walkSpeed;
            else
                _navMeshAgent.speed = _runSpeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), _angularSpeed * Time.deltaTime);
        }
    }

    #endregion
}
