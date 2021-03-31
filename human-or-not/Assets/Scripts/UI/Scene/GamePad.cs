using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePad : SceneUI
{
    RectTransform _backgroundRect;
    RectTransform _handleRect;
    Image _handleImage;
    float _backgroundRadius;
    float _walkLimits;

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
    bool _runningSensor = false;
    bool _buttonA = false;
    bool _buttonJ = false;

    public enum PadCode
    {
        RunningSensor,
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
            case PadCode.RunningSensor:
                return GamePad.Pad._runningSensor;
            case PadCode.ButtonA:
                return GamePad.Pad._buttonA;
            case PadCode.ButtonJ:
                return GamePad.Pad._buttonJ;
            default:
                return false;
        }
    }
    #endregion

    enum GameObjects
    {
        Panel,
        Joystick,
        Background,
        Handle,
        Buttons,
        Attack,
        Jump,
    }

    protected override void OnStart()
    {
        // Set Canvas
        base.OnStart();

        // Binding
        Bind<GameObject>(typeof(GameObjects));

        // Get GameObject
        GameObject panel = GetObject((int)GameObjects.Panel);

        GameObject joystick = GetObject((int)GameObjects.Joystick);
        GameObject background = GetObject((int)GameObjects.Background);
        GameObject handle = GetObject((int)GameObjects.Handle);

        GameObject buttons = GetObject((int)GameObjects.Buttons);
        GameObject attackButton = GetObject((int)GameObjects.Attack);
        GameObject jumpButton = GetObject((int)GameObjects.Jump);

        // Get Component & Init values
        _backgroundRect = background.GetComponent<RectTransform>();
        _handleImage = handle.GetComponent<Image>();
        _handleRect = handle.GetComponent<RectTransform>();
        _backgroundRadius = _backgroundRect.rect.width * 0.5f;
        _walkLimits = _backgroundRadius * 0.85f;

        // Bind Event
        BindEvent(panel, BeginRotate, Define.UIEvent.StartDrag);
        BindEvent(panel, OnRotate, Define.UIEvent.Dragging);
        BindEvent(panel, EndRotate, Define.UIEvent.EndDrag);

        BindEvent(joystick, Blocking, Define.UIEvent.Click);    // Joystick과 가까운 부분은 Rotate 불가능
        BindEvent(handle, Dragging, Define.UIEvent.Dragging);
        BindEvent(handle, EndDrag, Define.UIEvent.EndDrag);

        BindEvent(attackButton, (PointerEventData eventData) => { Debug.Log("Attack button clicked!"); }, Define.UIEvent.Click);
        BindEvent(jumpButton, (PointerEventData eventData) => { Debug.Log("Jump button clicked!"); }, Define.UIEvent.Click);
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

    void EndRotate(PointerEventData eventData)
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
        if (dir.magnitude >= _walkLimits)
        {
            _runningSensor = true;
            _handleImage.color = Color.black;
        }
        else
        {
            _runningSensor = false;
            _handleImage.color = Color.gray;
        }

        _handleRect.localPosition = dir;
        Direction = dir.normalized;
    }

    void EndDrag(PointerEventData eventData)
    {
        Direction = _handleRect.localPosition = Vector3.zero;
        _handleImage.color = Color.white;
    }
}
