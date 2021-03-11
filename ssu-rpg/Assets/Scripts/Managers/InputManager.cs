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
            OnMouse();
    }

    public void Clear()
    {
        MouseAction = null;
    }


    #region Event
    void OnMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseAction.Invoke(Define.MouseEvent.Down);

            // 시간 측정
            _pressedTime = Time.time;
        }
        else if (Input.GetMouseButton(0))
        {
            MouseAction.Invoke(Define.MouseEvent.Press);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (Time.time - _pressedTime < Define.MousePressedTime)
                MouseAction.Invoke(Define.MouseEvent.Click);
            else
                MouseAction.Invoke(Define.MouseEvent.Up);
        }
    }
    #endregion
}
