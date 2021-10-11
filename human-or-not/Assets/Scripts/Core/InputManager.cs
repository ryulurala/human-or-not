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
        _inputAction = null;

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
    public Action<Definition.PadEvent, Vector3> PadAction = null;
    public GameSceneUI.GamePad GamePad = null;

    void OnPadEvent()
    {
        if (GamePad == null)
            return;

        if (GamePad.RotatePanelTapped == GameSceneUI.GamePad.RotatePanelTap.Begin)
            PadAction.Invoke(Definition.PadEvent.BeginRotate, GamePad.TouchPoint);
        else if (GamePad.RotatePanelTapped == GameSceneUI.GamePad.RotatePanelTap.On)
            PadAction.Invoke(Definition.PadEvent.OnRotate, GamePad.TouchPoint);

        if (GamePad.Zoomed == true)
            PadAction.Invoke(Definition.PadEvent.OnZoom, Vector3.right * GamePad.ZoomValue);    // x를 zoom value로

        if (GamePad.ButtonClicked == GameSceneUI.GamePad.ButtonClick.Attack)
        {
            PadAction.Invoke(Definition.PadEvent.OnAttack, Vector3.zero);
            GamePad.ButtonClicked = GameSceneUI.GamePad.ButtonClick.None;
        }
        else if (GamePad.ButtonClicked == GameSceneUI.GamePad.ButtonClick.Jump)
        {
            PadAction.Invoke(Definition.PadEvent.OnJump, Vector3.zero);
            GamePad.ButtonClicked = GameSceneUI.GamePad.ButtonClick.None;
        }

        // 조이스틱 방향
        Vector3 dir = new Vector3(GamePad.Direction.x, 0, GamePad.Direction.y);
        // 카메라가 보는 방향으로 회전
        dir = Quaternion.Euler(0, Camera.main.transform.parent.rotation.eulerAngles.y, 0) * dir;
        dir = dir.normalized;

        if (GamePad.JoyStickDetected == GameSceneUI.GamePad.JoystickDetect.Center)
            PadAction.Invoke(Definition.PadEvent.OnIdle, dir);
        else if (GamePad.JoyStickDetected == GameSceneUI.GamePad.JoystickDetect.CloseToCenter)
            PadAction.Invoke(Definition.PadEvent.OnWalk, dir);
        else if (GamePad.JoyStickDetected == GameSceneUI.GamePad.JoystickDetect.FarFromCenter)
            PadAction.Invoke(Definition.PadEvent.OnRun, dir);
    }
    #endregion

    #region PC
    public Action<Definition.MouseEvent> MouseAction = null;
    public Action<Definition.KeyEvent, Vector3> KeyAction = null;

    void OnMouseEvent()
    {
        if (EventSystem.current.IsPointerOverGameObject() || MouseAction == null)
            return;

        // 공격
        if (Input.GetMouseButtonDown(0))
            MouseAction.Invoke(Definition.MouseEvent.LeftClick);

        // 회전
        if (Input.GetMouseButtonDown(1))
            MouseAction.Invoke(Definition.MouseEvent.RightDown);
        else if (Input.GetMouseButton(1))
            MouseAction.Invoke(Definition.MouseEvent.RightPressed);

        // 확대 / 축소
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
            MouseAction.Invoke(Definition.MouseEvent.ScrollWheel);
    }

    void OnKeyEvent()
    {
        if (KeyAction == null)
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
            KeyAction.Invoke(Definition.KeyEvent.None, dir);
        else if (Input.GetKey(KeyCode.LeftShift))
            KeyAction.Invoke(Definition.KeyEvent.ShiftWASD, dir);
        else
            KeyAction.Invoke(Definition.KeyEvent.WASD, dir);

        if (Input.GetKeyDown(KeyCode.Space))
            KeyAction.Invoke(Definition.KeyEvent.SpaceBar, dir);
    }
    #endregion
}
