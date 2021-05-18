using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
    [SerializeField] protected float _walkSpeed = 5f;
    [SerializeField] protected float _runSpeed = 10f;
    [SerializeField] protected float _angularSpeed = 30f;
    [SerializeField] protected Define.State _state;
    [SerializeField] public abstract Define.State State { get; set; }
    [SerializeField] public Define.WorldObject WorldObjectType { get; protected set; }

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
