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
                    _animator.CrossFade("Die", 0.05f);
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
                case Definition.State.Attack:
                    _animator.CrossFade("Attack", 0.05f);
                    StartCoroutine(WaitForExitState());
                    break;
                case Definition.State.Jump:
                    _animator.CrossFade("Jump", 0.05f);
                    StartCoroutine(WaitForExitState());
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

    void Move(float speed, Vector3 velocity)
    {
        if (velocity == Vector3.zero)
            return;

        // 방향
        Quaternion direction = Quaternion.LookRotation(velocity);
        transform.rotation = Quaternion.Slerp(transform.rotation, direction, _angularSpeed * Time.deltaTime);

        // 위치
        _characterController.Move(velocity * speed * Time.deltaTime);
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

    IEnumerator WaitForExitState()
    {
        _hasExitState = true;

        while (System.Enum.GetName(typeof(Definition.State), _state) != _animator.GetCurrentAnimatorClipInfo(0)[0].clip.name)
        {
            Debug.Log($"Animation State: {_animator.GetCurrentAnimatorClipInfo(0)[0].clip.name}");
            Debug.Log($"Definition State: {_state}");
            yield return null;
        }

        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length * 0.8f);

        State = Definition.State.Idle;

        _hasExitState = false;
    }

    #region Mobile
    void OnPadEvent(Definition.PadEvent padEvent, Vector3 dir)
    {
        // 끝날 때까지 기다려야 하는 State
        if (_hasExitState)
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
                Move(_walkSpeed, dir);          // Moving
                State = Definition.State.Walking;
                break;
            case Definition.PadEvent.OnRun:
                Move(_runSpeed, dir);           // Moving
                State = Definition.State.Running;
                break;
        }
    }
    #endregion

    #region PC
    void OnMouseEvent(Definition.MouseEvent mouseEvent)
    {
        // 끝날 때까지 기다려야 하는 State
        if (_hasExitState)
            return;

        if (mouseEvent == Definition.MouseEvent.LeftClick)
            State = Definition.State.Attack;
    }

    void OnKeyEvent(Definition.KeyEvent keyEvent, Vector3 dir)
    {
        // 끝날 때까지 기다려야 하는 State
        if (_hasExitState)
            return;

        switch (keyEvent)
        {
            case Definition.KeyEvent.None:
                State = Definition.State.Idle;
                break;
            case Definition.KeyEvent.WASD:
                Move(_walkSpeed, dir);
                State = Definition.State.Walking;
                break;
            case Definition.KeyEvent.ShiftWASD:
                Move(_runSpeed, dir);
                State = Definition.State.Running;
                break;
            case Definition.KeyEvent.SpaceBar:
                State = Definition.State.Jump;
                break;
        }
    }
    #endregion
}
