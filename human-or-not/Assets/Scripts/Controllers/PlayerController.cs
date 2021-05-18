using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    Animator _animator;
    CharacterController _characterController;
    bool _hasExitState;
    bool _hasEndedState { get { return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f; } }

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
                    _animator.CrossFadeInFixedTime("Die", 0.05f);  // 바로 애니메이션 실행
                    _hasExitState = true;
                    break;
                case Define.State.Idle:
                    if (_hasEndedState == true)
                        _animator.CrossFadeInFixedTime("Idle", 0.05f);
                    else
                        _animator.CrossFade("Idle", 0.05f);
                    _hasExitState = false;
                    break;
                case Define.State.Walking:
                    _animator.CrossFade("Walk", 0.05f);
                    _hasExitState = false;
                    break;
                case Define.State.Running:
                    _animator.CrossFade("Run", 0.05f);
                    _hasExitState = false;
                    break;
                case Define.State.Attack:
                    _animator.CrossFadeInFixedTime("Attack", 0.05f);
                    _hasExitState = true;
                    break;
                case Define.State.Jump:
                    _animator.CrossFadeInFixedTime("Jump", 0.05f);
                    _hasExitState = true;
                    break;
            }
        }
    }

    protected override void OnStart()
    {
        // Get Component
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

        // Settings
        State = Define.State.Idle;
        WorldObjectType = Define.WorldObject.Player;

        // if (GetComponent<MyPlayer>() == null)
        //     return;

        // Listener
        Manager.Input.MouseAction -= OnMouseEvent;  // Pooling으로 인해 두 번 등록 방지
        Manager.Input.MouseAction += OnMouseEvent;

        Manager.Input.PadAction -= OnPadEvent;      // Pooling으로 인해 두 번 등록 방지
        Manager.Input.PadAction += OnPadEvent;

        Manager.Input.KeyAction -= OnKeyEvent;
        Manager.Input.KeyAction += OnKeyEvent;
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
                UpdateWalking();
                break;
            case Define.State.Running:
                UpdateRunning();
                break;
            case Define.State.Attack:
                UpdateAttack();
                break;
            case Define.State.Jump:
                UpdateJump();
                break;
        }
    }

    void Move(float speed, Define.State state, Vector3 velocity)
    {
        if (velocity == Vector3.zero)
            return;

        // 방향
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), _angularSpeed * Time.deltaTime);

        // transform.Translate(velocity * speed * Time.deltaTime, Space.World);
        _characterController.Move(velocity * speed * Time.deltaTime);
        State = state;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
            return;

        if (Manager.Game.GetWorldObjectType(hit.gameObject) == Define.WorldObject.Player && Manager.Player.GetPlayerState(hit.gameObject) == Define.State.Attack)
        {
            if (State != Define.State.Jump)
                State = Define.State.Died;
        }
    }

    #region UpdateState
    void UpdateDied() { }
    void UpdateIdle() { }
    void UpdateWalking() { }
    void UpdateRunning() { }
    void UpdateAttack()
    {
        if (_hasEndedState == true)
            State = Define.State.Idle;
    }
    void UpdateJump()
    {
        if (_hasEndedState == true)
            State = Define.State.Idle;
    }
    #endregion

    #region Mobile
    void OnPadEvent(Define.PadEvent padEvent, Vector3 dir)
    {
        if (_hasExitState == true)
            return;

        switch (padEvent)
        {
            case Define.PadEvent.OnAttack:
                State = Define.State.Attack;
                break;
            case Define.PadEvent.OnJump:
                State = Define.State.Jump;
                break;
            case Define.PadEvent.OnIdle:
                State = Define.State.Idle;
                break;
            case Define.PadEvent.OnWalk:
                Move(_walkSpeed, Define.State.Walking, dir);
                break;
            case Define.PadEvent.OnRun:
                Move(_runSpeed, Define.State.Running, dir);
                break;
        }
    }
    #endregion

    #region PC
    void OnMouseEvent(Define.MouseEvent mouseEvent)
    {
        if (_hasExitState == true)
            return;

        if (mouseEvent == Define.MouseEvent.LeftClick)
            State = Define.State.Attack;

    }

    void OnKeyEvent(Define.KeyEvent keyEvent, Vector3 dir)
    {

        if (_hasExitState == true)
            return;

        switch (keyEvent)
        {
            case Define.KeyEvent.None:
                State = Define.State.Idle;
                break;
            case Define.KeyEvent.WASD:
                Move(_walkSpeed, Define.State.Walking, dir);
                break;
            case Define.KeyEvent.ShiftWASD:
                Move(_runSpeed, Define.State.Running, dir);
                break;
            case Define.KeyEvent.SpaceBar:
                State = Define.State.Jump;
                break;
        }
    }
    #endregion
}
