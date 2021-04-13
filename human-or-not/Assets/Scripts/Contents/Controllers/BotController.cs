using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : BaseController
{
    [SerializeField] bool _hasTargetPoint = false;
    [SerializeField] Vector3 _destPos;
    [SerializeField] float _maxRange = 50f;

    Animator _animator;
    NavMeshAgent _navMeshAgent;

    public override Define.State State
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
                case Define.State.Died:
                    _animator.CrossFadeInFixedTime("Die", 0.05f);  // Trigger Animation
                    break;
                case Define.State.Idle:
                    _animator.CrossFade("Idle", 0.05f);
                    break;
                case Define.State.Walking:
                    _animator.CrossFade("Walk", 0.05f);
                    break;
                case Define.State.Running:
                    _animator.CrossFade("Run", 0.05f);
                    break;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Die
        if (Manager.Game.GetWorldObjectType(other.gameObject) == Define.WorldObject.Player && Manager.Player.GetPlayerState(other.gameObject) == Define.State.Attack)
            State = Define.State.Died;
    }

    protected override void OnStart()
    {
        // Get Component
        _animator = GetComponent<Animator>();
        _navMeshAgent = gameObject.GetOrAddComponent<NavMeshAgent>();
        _navMeshAgent.isStopped = false;

        State = Define.State.Idle;
        WorldObjectType = Define.WorldObject.Bot;

        StartCoroutine(DefineTargetPoint());
    }

    protected override void OnUpdate()
    {
        switch (_state)
        {
            case Define.State.Died:
                UpdateDied();
                break;
            case Define.State.Idle:
                UpdateIdle();
                break;
            case Define.State.Walking:
            case Define.State.Running:
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

            if (State == Define.State.Died)
                break;

            Vector3 point;
            if (RandomPoint(transform.position, _maxRange, out point))
            {
                _destPos = point;
                _hasTargetPoint = true;     // 움직여라!
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
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
        if (State == Define.State.Died)
            return;

        if (_hasTargetPoint == true)
        {
            int randNum = Random.Range(1, 100);
            if (randNum <= 80)
                State = Define.State.Walking;   // 80% 확률
            else
                State = Define.State.Running;   // 20% 확률
        }
    }

    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            _navMeshAgent.ResetPath();
            _hasTargetPoint = false;
            State = Define.State.Idle;
        }
        else
        {
            _navMeshAgent.SetDestination(_destPos);
            if (State == Define.State.Walking)
                _navMeshAgent.speed = _walkSpeed;
            else
                _navMeshAgent.speed = _runSpeed;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), _angularSpeed * Time.deltaTime);
        }
    }

    #endregion
}
