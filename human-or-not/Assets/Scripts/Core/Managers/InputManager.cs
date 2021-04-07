using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    Action _inputAction = null;

    public void OnUpdate()
    {
        if (_inputAction != null)
            _inputAction.Invoke();
    }

    public void Init()
    {
        if (Util.IsMobile)
        {
            // Mobile
            MouseAction = null;
            KeyAction = null;

            GameObject go = GameObject.Find("GamePad");
            if (go == null)
                return;

            Gamepad = go.GetComponent<GamePad>();

            _inputAction -= OnPadEvent;
            _inputAction += OnPadEvent;

        }
        else
        {
            // PC
            PadAction = null;

            _inputAction -= OnMouseEvent;
            _inputAction += OnMouseEvent;

            _inputAction -= OnKeyEvent;
            _inputAction += OnKeyEvent;
        }
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
    public GamePad Gamepad = null;

    void OnPadEvent()
    {
        if (Gamepad == null)
            return;

        if (Gamepad.RotatePanelTapped == GamePad.RotatePanelTap.Begin)
            PadAction.Invoke(Define.PadEvent.BeginRotate, Gamepad.TouchPoint);
        else if (Gamepad.RotatePanelTapped == GamePad.RotatePanelTap.On)
            PadAction.Invoke(Define.PadEvent.OnRotate, Gamepad.TouchPoint);

        if (Gamepad.Zoomed == true)
            PadAction.Invoke(Define.PadEvent.OnZoom, Vector3.right * Gamepad.ZoomValue);    // x를 zoom value로

        if (Gamepad.ButtonClicked == GamePad.ButtonClick.Attack)
        {
            PadAction.Invoke(Define.PadEvent.OnAttack, Vector3.zero);
            Gamepad.ButtonClicked = GamePad.ButtonClick.None;
        }
        else if (Gamepad.ButtonClicked == GamePad.ButtonClick.Jump)
        {
            PadAction.Invoke(Define.PadEvent.OnJump, Vector3.zero);
            Gamepad.ButtonClicked = GamePad.ButtonClick.None;
        }

        // 조이스틱 방향
        Vector3 dir = new Vector3(Gamepad.Direction.x, 0, Gamepad.Direction.y);
        // 카메라가 보는 방향으로 회전
        dir = Quaternion.Euler(0, Camera.main.transform.parent.rotation.eulerAngles.y, 0) * dir;
        dir = dir.normalized;

        if (Gamepad.JoyStickDetected == GamePad.JoystickDetect.Center)
            PadAction.Invoke(Define.PadEvent.OnIdle, dir);
        else if (Gamepad.JoyStickDetected == GamePad.JoystickDetect.CloseToCenter)
            PadAction.Invoke(Define.PadEvent.OnWalk, dir);
        else if (Gamepad.JoyStickDetected == GamePad.JoystickDetect.FarFromCenter)
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
