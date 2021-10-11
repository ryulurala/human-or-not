using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _walkSpeed = 5f;
    [SerializeField] float _runSpeed = 10f;
    [SerializeField] float _angularSpeed = 30f;
    [SerializeField] Definition.State _state;

    Animator _animator;
    CharacterController _characterController;
    bool _hasExitState;
    bool _hasEndedState { get { return _animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.95f; } }

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
                    _animator.CrossFadeInFixedTime("Die", 0.05f);  // 바로 애니메이션 실행
                    _hasExitState = true;
                    break;
                case Definition.State.Idle:
                    if (_hasEndedState == true)
                        _animator.CrossFadeInFixedTime("Idle", 0.05f);
                    else
                        _animator.CrossFade("Idle", 0.05f);
                    _hasExitState = false;
                    break;
                case Definition.State.Walking:
                    _animator.CrossFade("Walk", 0.05f);
                    _hasExitState = false;
                    break;
                case Definition.State.Running:
                    _animator.CrossFade("Run", 0.05f);
                    _hasExitState = false;
                    break;
                case Definition.State.Attack:
                    _animator.CrossFadeInFixedTime("Attack", 0.05f);
                    _hasExitState = true;
                    break;
                case Definition.State.Jump:
                    _animator.CrossFadeInFixedTime("Jump", 0.05f);
                    _hasExitState = true;
                    break;
            }
        }
    }

    void Start()
    {
        // Get Component
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

        // Settings
        State = Definition.State.Idle;

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
                UpdateWalking();
                break;
            case Definition.State.Running:
                UpdateRunning();
                break;
            case Definition.State.Attack:
                UpdateAttack();
                break;
            case Definition.State.Jump:
                UpdateJump();
                break;
        }
    }

    void Move(float speed, Definition.State state, Vector3 velocity)
    {
        if (velocity == Vector3.zero)
            return;

        // 방향
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(velocity), _angularSpeed * Time.deltaTime);

        // transform.Translate(velocity * speed * Time.deltaTime, Space.World);
        _characterController.Move(velocity * speed * Time.deltaTime);
        State = state;
    }

    // void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     if (hit.gameObject.layer.Equals(LayerMask.NameToLayer("Ground")))
    //         return;

    //     if (Manager.Game.GetWorldObjectType(hit.gameObject) == Definition.WorldObject.Player && Manager.Player.GetPlayerState(hit.gameObject) == Definition.State.Attack)
    //     {
    //         if (State != Definition.State.Jump)
    //             State = Definition.State.Died;
    //     }
    // }

    #region UpdateState
    void UpdateDied() { }
    void UpdateIdle() { }
    void UpdateWalking() { }
    void UpdateRunning() { }
    void UpdateAttack()
    {
        if (_hasEndedState == true)
            State = Definition.State.Idle;  // 코루틴으로?: 대기 시간 후 idle로 변경
    }
    void UpdateJump()
    {
        if (_hasEndedState == true)
            State = Definition.State.Idle;  // 코루틴으로?: 대기 시간 후 idle로 변경
    }
    #endregion

    #region Mobile
    void OnPadEvent(Definition.PadEvent padEvent, Vector3 dir)
    {
        if (_hasExitState == true)
            return;

        switch (padEvent)
        {
            case Definition.PadEvent.OnAttack:
                State = Definition.State.Attack;
                break;
            case Definition.PadEvent.OnJump:
                State = Definition.State.Jump;
                break;
            case Definition.PadEvent.OnIdle:
                State = Definition.State.Idle;
                break;
            case Definition.PadEvent.OnWalk:
                Move(_walkSpeed, Definition.State.Walking, dir);
                break;
            case Definition.PadEvent.OnRun:
                Move(_runSpeed, Definition.State.Running, dir);
                break;
        }
    }
    #endregion

    #region PC
    void OnMouseEvent(Definition.MouseEvent mouseEvent)
    {
        if (_hasExitState == true)
            return;

        if (mouseEvent == Definition.MouseEvent.LeftClick)
            State = Definition.State.Attack;

    }

    void OnKeyEvent(Definition.KeyEvent keyEvent, Vector3 dir)
    {

        if (_hasExitState == true)
            return;

        switch (keyEvent)
        {
            case Definition.KeyEvent.None:
                State = Definition.State.Idle;
                break;
            case Definition.KeyEvent.WASD:
                Move(_walkSpeed, Definition.State.Walking, dir);
                break;
            case Definition.KeyEvent.ShiftWASD:
                Move(_runSpeed, Definition.State.Running, dir);
                break;
            case Definition.KeyEvent.SpaceBar:
                State = Definition.State.Jump;
                break;
        }
    }
    #endregion
}
