using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] protected Define.State _state;
    [SerializeField] public Define.WorldObject WorldObjectType { get; protected set; }
    [SerializeField] protected float _walkSpeed = 5f;
    [SerializeField] protected float _runSpeed = 10f;
    [SerializeField] protected float _angularSpeed = 30f;

    public Define.State State
    {
        get { return _state; }
        set
        {
            // 무분별한 State 변경 방지
            if (value == _state)
                return;

            _state = value;
            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case Define.State.Die:
                    // anim.CrossFade("Die", 0.1f);
                    break;
                case Define.State.Idle:
                    anim.CrossFade("Idle", 0.05f);
                    break;
                case Define.State.Walking:
                    anim.CrossFade("Walk", 0.05f);
                    break;
                case Define.State.Running:
                    anim.CrossFade("Run", 0.05f);
                    break;
                case Define.State.Attack:
                    // anim.CrossFade("Attack", 0.1f);
                    break;
            }
        }
    }

    void Start()
    {
        OnStart();
    }

    void Update()
    {
        OnUpdate();
    }

    protected abstract void OnStart();
    protected abstract void OnUpdate();
}
