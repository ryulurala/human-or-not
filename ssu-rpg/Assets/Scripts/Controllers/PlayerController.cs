using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    Vector3 _destPos;
    float _walkSpeed = 5f;
    float _runSpeed = 10f;
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
                case Define.State.Die:
                    anim.CrossFade("Die", 0.25f);
                    break;
                case Define.State.Idle:
                    anim.CrossFade("Idle", 0f);
                    break;
                case Define.State.Walking:
                    anim.CrossFade("Walk", 0f);
                    break;
                case Define.State.Running:
                    anim.CrossFade("Run", 0f);
                    break;
                case Define.State.Attack:
                    // anim.CrossFade("Attack", 0.1f);
                    break;
            }
        }
    }

    protected override void OnStart()
    {
        // Settings
        State = Define.State.Idle;
        WorldObjectType = Define.WorldObject.Player;

        // Listener
        Manager.Input.MouseAction -= OnMouseEvent;  // Pooling으로 인해 두 번 등록 방지
        Manager.Input.MouseAction += OnMouseEvent;

        Manager.Input.TouchAction -= OnTouchEvent;  // Pooling으로 인해 두 번 등록 방지
        Manager.Input.TouchAction += OnTouchEvent;

        Manager.Input.KeyAction -= OnKeyEvent;
        Manager.Input.KeyAction += OnKeyEvent;
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
                UpdateWalking();
                break;
            case Define.State.Running:
                UpdateRunning();
                break;
            case Define.State.Attack:
                UpdateAttack();
                break;
        }
    }

    #region UpdateState
    void UpdateDie() { }
    void UpdateIdle() { }
    void UpdateWalking()
    {
        if (!Input.anyKey)
            State = Define.State.Idle;
    }
    void UpdateRunning() { }
    void UpdateAttack()
    {

    }

    #endregion

    #region Mobile
    void OnTouchEvent(Define.TouchEvent touchEvent)
    {
        if (_state == Define.State.Die)
            return;

        if (touchEvent == Define.TouchEvent.TabWithOne)
            MovePoint(Input.GetTouch(0).position);
    }
    #endregion

    #region PC
    void OnMouseEvent(Define.MouseEvent mouseEvent)
    {
        if (_state == Define.State.Die)
            return;

        // if (mouseEvent == Define.MouseEvent.LeftClick)
        // State = Define.State.Attack;
    }

    void OnKeyEvent(Define.KeyEvent keyEvent, Vector3 dir)
    {
        if (_state == Define.State.Die)
            return;

        if (keyEvent == Define.KeyEvent.WASD)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), _angularSpeed * Time.deltaTime);
            GetComponent<CharacterController>().Move(dir * _walkSpeed * Time.deltaTime);
            State = Define.State.Walking;
        }
        else if (keyEvent == Define.KeyEvent.ShiftWASD)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), _angularSpeed * Time.deltaTime);
            GetComponent<CharacterController>().Move(dir * _runSpeed * Time.deltaTime);
            State = Define.State.Running;
        }
        else if (keyEvent == Define.KeyEvent.SpaceBar)
        {
            Debug.Log("Jump!");
        }
    }
    #endregion

    void MovePoint(Vector2 point)
    {
        Ray ray = Camera.main.ScreenPointToRay(point);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100f, LayerMask.GetMask("Ground")))
        {
            _destPos = hitInfo.point;
            State = Define.State.Walking;
            Debug.DrawRay(_destPos, Vector3.up, Color.red, 1.0f);
        }
    }

}
