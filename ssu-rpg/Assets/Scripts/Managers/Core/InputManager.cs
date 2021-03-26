using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    Action _inputAction = null;
    float _pressedTime = 0f;
    Vector2 _startPos;

    public void OnAwake()
    {
        if (Util.IsMobile)
        {
            // Mobile
            _inputAction -= OnTouchEvent;
            _inputAction += OnTouchEvent;
            MouseAction = null;
        }
        else
        {
            // PC
            _inputAction -= OnMouseEvent;
            _inputAction += OnMouseEvent;

            _inputAction -= OnKeyEvent;
            _inputAction += OnKeyEvent;
            TouchAction = null;
        }
    }

    public void OnUpdate()
    {
        // UI Click 상태
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (_inputAction != null)
            _inputAction.Invoke();
    }

    public void Clear()
    {
        MouseAction = null;
        TouchAction = null;
    }

    #region Mobile
    public Action<Define.TouchEvent> TouchAction = null;

    void OnTouchEvent()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                TouchAction.Invoke(Define.TouchEvent.TabWithOneStart);
                _pressedTime = Time.time;   // 시간 측정
                _startPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                TouchAction.Invoke(Define.TouchEvent.PressWithOne);
            }
        }
    }
    #endregion

    #region PC
    public Action<Define.MouseEvent> MouseAction = null;
    public Action<Define.KeyEvent, Vector3> KeyAction = null;

    void OnMouseEvent()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // RMB
            MouseAction.Invoke(Define.MouseEvent.LeftClick);
        }

        if (Input.GetMouseButtonDown(1))
        {
            MouseAction.Invoke(Define.MouseEvent.RightStart);
        }
        else if (Input.GetMouseButton(1))
        {
            MouseAction.Invoke(Define.MouseEvent.RightPress);
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            // Scroll Wheel
            MouseAction.Invoke(Define.MouseEvent.ScrollWheel);
        }
    }

    void OnKeyEvent()
    {
        // 방향 벡터 축적
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            dir += new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;
        if (Input.GetKey(KeyCode.S))
            dir += new Vector3(-Camera.main.transform.forward.x, 0, -Camera.main.transform.forward.z).normalized;
        if (Input.GetKey(KeyCode.A))
            dir += new Vector3(-Camera.main.transform.right.x, 0, -Camera.main.transform.right.z).normalized;
        if (Input.GetKey(KeyCode.D))
            dir += new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z).normalized;

        // 결정
        if (dir != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                KeyAction.Invoke(Define.KeyEvent.ShiftWASD, dir);
            else
                KeyAction.Invoke(Define.KeyEvent.WASD, dir);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
            KeyAction.Invoke(Define.KeyEvent.SpaceBar, dir);

    }
    #endregion
}
