using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePad : SceneUI
{
    RectTransform _backgroundRect;
    RectTransform _handleRect;
    float _backgroundRadius;

    public Vector2 Direction { get; private set; } = Vector2.zero;
    public Vector2 Point { get; private set; } = Vector2.zero;

    #region InputPad
    static GamePad _pad = null;
    public static GamePad Pad
    {
        get
        {
            if (_pad == null)
            {
                GameObject go = GameObject.Find("@GamePad");
                if (go == null)
                {
                    go = Manager.Resource.Instaniate($"UI/Scene/{typeof(GamePad)}");
                    go.name = "@GamePad";
                }

                _pad = go.GetComponent<GamePad>();
                DontDestroyOnLoad(_pad);

                _pad.gameObject.SetActive(false);
            }
            return _pad;
        }
    }

    bool _backgroundTab = false;
    bool _buttonA = false;
    bool _buttonJ = false;
    bool _buttonR = false;

    public enum PadCode
    {
        ButtonR,
        ButtonA,
        ButtonJ,
        BackGroundTab,
    }

    public bool GetPad(PadCode padCode)
    {
        switch (padCode)
        {
            case PadCode.BackGroundTab:
                return _backgroundTab;
            case PadCode.ButtonA:
                return GamePad.Pad._buttonA;
            case PadCode.ButtonJ:
                return GamePad.Pad._buttonJ;
            case PadCode.ButtonR:
                return GamePad.Pad._buttonR;
            default:
                return false;
        }
    }
    #endregion

    enum GameObjects
    {
        Joystick,
        Background,
        Handle,
        RotatePanel,
    }

    protected override void OnStart()
    {
        // Set Canvas
        base.OnStart();

        // Binding
        Bind<GameObject>(typeof(GameObjects));

        // Component get
        GameObject joystick = GetObject((int)GameObjects.Joystick);
        GameObject background = GetObject((int)GameObjects.Background);
        GameObject handle = GetObject((int)GameObjects.Handle);
        GameObject rotatePanel = GetObject((int)GameObjects.RotatePanel);

        _backgroundRect = background.GetComponent<RectTransform>();
        _handleRect = handle.GetComponent<RectTransform>();
        _backgroundRadius = _backgroundRect.rect.width * 0.5f;

        BindEvent(joystick, Blocking, Define.UIEvent.Click);

        BindEvent(handle, Dragging, Define.UIEvent.Dragging);
        BindEvent(handle, EndDrag, Define.UIEvent.DragEnd);

        BindEvent(rotatePanel, BeginRotate, Define.UIEvent.DragStart);
        BindEvent(rotatePanel, OnRotate, Define.UIEvent.Dragging);
        BindEvent(rotatePanel, RotateEnd, Define.UIEvent.DragEnd);
    }

    void BeginRotate(PointerEventData eventData)
    {
        // 첫 시작 Invoke
        Manager.Input.PadAction.Invoke(Define.PadEvent.StartRotate, eventData.position);
        Point = eventData.position;

        _backgroundTab = true;
    }

    void OnRotate(PointerEventData eventData)
    {
        // 짧은 순간에 거리 제곱 차가 클 경우 무시
        if ((Camera.main.ScreenToViewportPoint(Point) - Camera.main.ScreenToViewportPoint(eventData.position)).magnitude > 0.1f)
            return;

        Point = eventData.position;
    }

    void RotateEnd(PointerEventData eventData)
    {
        _backgroundTab = false;
    }

    void Blocking(PointerEventData eventData)
    {
        Debug.Log($"Blocking!");
    }

    void Dragging(PointerEventData eventData)
    {
        Vector2 dir = Vector2.ClampMagnitude(eventData.position - (Vector2)_backgroundRect.position, _backgroundRadius);

        _handleRect.localPosition = dir;
        Direction = dir.normalized;
    }

    void EndDrag(PointerEventData eventData)
    {
        Direction = _handleRect.localPosition = Vector3.zero;
    }
}
