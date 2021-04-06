using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    Action _inputAction = null;

    public void OnAwake()
    {
        if (Util.IsMobile)
        {
            // Mobile
            if (GamePad.Pad != null)
            {
                _inputAction -= OnPadEvent;
                _inputAction += OnPadEvent;
            }

            MouseAction = null;
            KeyAction = null;
        }
        else
        {
            // PC
            _inputAction -= OnMouseEvent;
            _inputAction += OnMouseEvent;

            _inputAction -= OnKeyEvent;
            _inputAction += OnKeyEvent;

            PadAction = null;
        }
    }

    public void OnUpdate()
    {
        _inputAction.Invoke();
    }

    public void Clear()
    {
        if (Util.IsMobile)
        {
            // Mobile
            PadAction = null;
        }
        else
        {
            // PC
            MouseAction = null;
            KeyAction = null;
        }
    }

    #region Mobile
    public Action<Define.PadEvent, Vector3> PadAction = null;

    void OnPadEvent()
    {
        if (PadAction == null)
            return;

        if (GamePad.Pad.RotatePanelTapped == GamePad.RotatePanelTap.Begin)
            PadAction.Invoke(Define.PadEvent.BeginRotate, GamePad.Pad.TouchPoint);
        else if (GamePad.Pad.RotatePanelTapped == GamePad.RotatePanelTap.On)
            PadAction.Invoke(Define.PadEvent.OnRotate, GamePad.Pad.TouchPoint);

        if (GamePad.Pad.Zoomed == true)
            PadAction.Invoke(Define.PadEvent.OnZoom, Vector3.right * GamePad.Pad.ZoomValue);    // x를 zoom value로

        if (GamePad.Pad.ButtonClicked == GamePad.ButtonClick.Attack)
        {
            PadAction.Invoke(Define.PadEvent.OnAttack, Vector3.zero);
            GamePad.Pad.ButtonClicked = GamePad.ButtonClick.None;
        }
        else if (GamePad.Pad.ButtonClicked == GamePad.ButtonClick.Jump)
        {
            PadAction.Invoke(Define.PadEvent.OnJump, Vector3.zero);
            GamePad.Pad.ButtonClicked = GamePad.ButtonClick.None;
        }

        // 조이스틱 방향
        Vector3 dir = new Vector3(GamePad.Pad.Direction.x, 0, GamePad.Pad.Direction.y);
        // 카메라가 보는 방향으로 회전
        dir = Quaternion.Euler(0, Camera.main.transform.parent.rotation.eulerAngles.y, 0) * dir;
        dir = dir.normalized;

        if (GamePad.Pad.JoyStickDetected == GamePad.JoystickDetect.Center)
            PadAction.Invoke(Define.PadEvent.OnIdle, dir);
        else if (GamePad.Pad.JoyStickDetected == GamePad.JoystickDetect.CloseToCenter)
            PadAction.Invoke(Define.PadEvent.OnWalk, dir);
        else if (GamePad.Pad.JoyStickDetected == GamePad.JoystickDetect.FarFromCenter)
            PadAction.Invoke(Define.PadEvent.OnRun, dir);
    }
    #endregion

    #region PC
    public Action<Define.MouseEvent> MouseAction = null;
    public Action<Define.KeyEvent, Vector3> KeyAction = null;

    void OnMouseEvent()
    {
        if (EventSystem.current.IsPointerOverGameObject() || MouseAction == null)
            return;

        // 공격
        if (Input.GetMouseButtonDown(0))
            MouseAction.Invoke(Define.MouseEvent.LeftClick);

        // 회전
        if (Input.GetMouseButtonDown(1))
            MouseAction.Invoke(Define.MouseEvent.RightDown);
        else if (Input.GetMouseButton(1))
            MouseAction.Invoke(Define.MouseEvent.RightPressed);

        // 확대 / 축소
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            MouseAction.Invoke(Define.MouseEvent.ScrollWheel);
    }

    void OnKeyEvent()
    {
        if (EventSystem.current.IsPointerOverGameObject() || KeyAction == null)
            return;

        // 방향 벡터 축적, 카메라가 보는 방향이 forward
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            dir += new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        if (Input.GetKey(KeyCode.S))
            dir += new Vector3(-Camera.main.transform.forward.x, 0, -Camera.main.transform.forward.z);
        if (Input.GetKey(KeyCode.A))
            dir += new Vector3(-Camera.main.transform.right.x, 0, -Camera.main.transform.right.z);
        if (Input.GetKey(KeyCode.D))
            dir += new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);
        dir = dir.normalized;

        // 걷기, 뛰기 둘 중 하나 무조건 실행 -> 속도 벡터 전달
        if (dir == Vector3.zero)
            KeyAction.Invoke(Define.KeyEvent.None, dir);
        else if (Input.GetKey(KeyCode.LeftShift))
            KeyAction.Invoke(Define.KeyEvent.ShiftWASD, dir);
        else
            KeyAction.Invoke(Define.KeyEvent.WASD, dir);

        if (Input.GetKeyDown(KeyCode.Space))
            KeyAction.Invoke(Define.KeyEvent.SpaceBar, dir);
    }
    #endregion
}
