using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NonPlayerController : BaseController
{
    [SerializeField] bool _hasTargetPoint = false;
    [SerializeField] Vector3 _destPos;
    [SerializeField] float _maxRange = 50.0f;

    public Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;
            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case Define.State.Die:
                    anim.CrossFade("Die", 0.1f);
                    break;
                case Define.State.Idle:
                    anim.CrossFade("Idle", 0.1f);
                    break;
                case Define.State.Walking:
                    anim.CrossFade("Walk", 0.1f);
                    break;
                case Define.State.Running:
                    anim.CrossFade("Run", 0.1f);
                    break;
            }
        }
    }

    protected override void OnStart()
    {
        State = Define.State.Idle;
        WorldObjectType = Define.WorldObject.NonPlayer;

        StartCoroutine(DefineTargetPoint());
    }

    protected override void OnUpdate()
    {
        switch (_state)
        {
            case Define.State.Die:
                UpdateDie();
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
        while (_state != Define.State.Die)
        {
            float randSeconds = Random.Range(5f, 10f);
            yield return new WaitForSeconds(randSeconds);
            // 5 ~ 10초 뒤

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
    void UpdateDie() { }
    void UpdateIdle()
    {
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
        NavMeshAgent nma = gameObject.GetOrAddCompontent<NavMeshAgent>();

        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            nma.ResetPath();
            _hasTargetPoint = false;
            State = Define.State.Idle;
        }
        else
        {
            nma.SetDestination(_destPos);
            nma.speed = 10f;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10f * Time.deltaTime);
        }
    }

    #endregion
}
