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

        Manager.Input.TouchAction -= OnTouchEvent;  // Pooling으로 인해 두 번 등록 방지
        Manager.Input.TouchAction += OnTouchEvent;
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

    #region Mobile
    void OnTouchEvent(Define.TouchEvent touchEvent)
    {
        if (_state == Define.State.Die)
            return;

        if (touchEvent == Define.TouchEvent.TabWithOne)
            MovePoint();
    }
    #endregion

    #region PC
    void OnMouseEvent(Define.MouseEvent mouseEvent)
    {
        if (_state == Define.State.Die)
            return;

        if (mouseEvent == Define.MouseEvent.RightClick)
            MovePoint();
    }
    #endregion

    void MovePoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100f, LayerMask.GetMask("Ground")))
        {
            Debug.Log($"이동! {hitInfo.point}");
            _destPos = hitInfo.point;
            State = Define.State.Walking;
        }

        Debug.DrawRay(Camera.main.transform.position, Input.mousePosition - Camera.main.transform.position * 100f, Color.green, 1f);
    }
}
