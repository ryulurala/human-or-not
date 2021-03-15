using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action<Define.MouseEvent> MouseAction = null;
    public Action<Define.TouchEvent> TouchAction = null;
    float _pressedTime = 0f;

    public void OnUpdate()
    {
        // UI Click 상태
        // if (EventSystem.current.IsPointerOverGameObject())
        //     return;

        if (Util.IsMobile)
        {
            // Mobile일 때
            if (TouchAction != null)
                OnTouchEvent();
        }
        else
        {
            // PC일 때
            if (MouseAction != null)
                OnMouseEvent();
        }
    }

    public void Clear()
    {
        MouseAction = null;
    }

    #region Mobile
    void OnTouchEvent()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _pressedTime = Time.time;   // 시간 측정
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                TouchAction.Invoke(Define.TouchEvent.PressWithOne);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (Time.time - _pressedTime < Define.touchPressedTime)
                    TouchAction.Invoke(Define.TouchEvent.TabWithOne);
            }
        }
        else if (Input.touchCount == 2)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                TouchAction.Invoke(Define.TouchEvent.PressWithTwo);
            }
        }
    }
    #endregion

    #region PC
    void OnMouseEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseAction.Invoke(Define.MouseEvent.LeftStart);
            // LMB
            _pressedTime = Time.time;   // 시간 측정
        }
        else if (Input.GetMouseButton(0))
        {
            MouseAction.Invoke(Define.MouseEvent.LeftPress);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - _pressedTime < Define.MousePressedTime)
                MouseAction.Invoke(Define.MouseEvent.LeftClick);
        }

        if (Input.GetMouseButtonUp(1))
        {
            // RMB
            MouseAction.Invoke(Define.MouseEvent.RightClick);
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            // Scroll Wheel
            MouseAction.Invoke(Define.MouseEvent.ScrollWheel);
        }
    }
    #endregion
}
