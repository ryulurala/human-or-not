using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action<Define.MouseEvent> MouseAction = null;
    float _pressedTime = 0f;

    public void OnUpdate()
    {
        // UI Click 상태
        // if (EventSystem.current.IsPointerOverGameObject())
        //     return;

        if (MouseAction != null)
            OnMouseEvent();
    }

    public void Clear()
    {
        MouseAction = null;
    }

    #region Event
    void OnMouseEvent()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
