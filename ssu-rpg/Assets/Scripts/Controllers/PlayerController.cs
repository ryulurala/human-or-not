using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    Vector3 _destPos;
    float _walkSpeed = 5f;
    float _angularSpeed = 10f;

    public Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;
            Animator anim = GetComponent<Animator>();
            switch (_state)
            {
                case Define.State.Idle:
                    // anim.CrossFade("Idle", 0.1f);
                    break;
                case Define.State.Walking:
                    // anim.CrossFade("Walk", 0.1f);
                    break;
                case Define.State.Running:
                    // anim.CrossFade("Run", 0.1f);
                    break;
            }
        }
    }

    protected override void OnStart()
    {
        // Settings
        _state = Define.State.Idle;
        WorldObjectType = Define.WorldObject.Player;

        // Listener
        Manager.Input.MouseAction -= OnMouseEvent;  // Pooling으로 인해 두 번 등록 방지
        Manager.Input.MouseAction += OnMouseEvent;
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
                UpdateWalk();
                break;
            case Define.State.Running:
                UpdateRun();
                break;
        }
    }

    #region UpdateState
    void UpdateDie() { }
    void UpdateIdle() { }
    void UpdateWalk()
    {
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_walkSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), _angularSpeed * Time.deltaTime);
        }
    }
    void UpdateRun() { }
    #endregion

    #region OnEvent
    void OnMouseEvent(Define.MouseEvent mouseEvent)
    {
        if (_state == Define.State.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        bool isHit = Physics.Raycast(ray, out hitInfo, 100f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(Camera.main.transform.position, Input.mousePosition - Camera.main.transform.position * 100f, Color.green, 1f);

        switch (mouseEvent)
        {
            case Define.MouseEvent.Down:
                break;
            case Define.MouseEvent.Press:
                break;
            case Define.MouseEvent.Up:
                break;
            case Define.MouseEvent.Click:
                if (isHit)
                {
                    Debug.Log($"이동! {hitInfo.point}");
                    _destPos = hitInfo.point;
                    State = Define.State.Walking;
                }
                break;
        }
    }
    #endregion
}
