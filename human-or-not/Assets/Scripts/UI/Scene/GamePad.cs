using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePad : SceneUI
{
    RectTransform _outerCircleRect;
    RectTransform _innerCircleRect;
    Image _innerCircleImage;
    float _outerCircleRadius;
    float _boundary;

    public Vector2 Direction { get; private set; } = Vector2.zero;
    public Vector2 Point { get; private set; } = Vector2.zero;

    public BackgroundTap BackgroundTapped { get; set; } = BackgroundTap.None;
    public enum BackgroundTap
    {
        None,
        Begin,
        On,
    }
    public bool RunningSensorDeteted { get; private set; } = false;
    public ButtonClick ButtonClicked { get; set; } = ButtonClick.None;
    public enum ButtonClick
    {
        None,
        Attack,
        Jump,
    }

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

    enum GameObjects
    {
        Panel,
        Joystick,
        OuterCircle,
        InnerCircle,
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
        GameObject outerCircle = GetObject((int)GameObjects.OuterCircle);
        GameObject innerCircle = GetObject((int)GameObjects.InnerCircle);

        GameObject attackButton = GetObject((int)GameObjects.Attack);
        GameObject jumpButton = GetObject((int)GameObjects.Jump);

        // Get Component & Init values
        _outerCircleRect = outerCircle.GetComponent<RectTransform>();
        _innerCircleImage = innerCircle.GetComponent<Image>();
        _innerCircleRect = innerCircle.GetComponent<RectTransform>();
        _outerCircleRadius = _outerCircleRect.rect.width * 0.5f;
        _boundary = _outerCircleRadius * 0.85f;

        // Bind Event
        BindEvent(panel, BeginRotate, Define.UIEvent.StartDrag);
        BindEvent(panel, OnRotate, Define.UIEvent.Dragging);
        BindEvent(panel, EndRotate, Define.UIEvent.EndDrag);

        BindEvent(joystick, Blocking, Define.UIEvent.Click);    // Joystick과 가까운 부분은 Rotate 불가능
        BindEvent(innerCircle, Dragging, Define.UIEvent.Dragging);
        BindEvent(innerCircle, EndDrag, Define.UIEvent.EndDrag);

        BindEvent(attackButton, (PointerEventData eventData) => { ButtonClicked = ButtonClick.Attack; }, Define.UIEvent.PointerDown);
        BindEvent(jumpButton, (PointerEventData eventData) => { ButtonClicked = ButtonClick.Jump; }, Define.UIEvent.PointerDown);
    }

    void BeginRotate(PointerEventData eventData)
    {
        // 첫 시작 Invoke
        BackgroundTapped = BackgroundTap.Begin;
        Point = eventData.position;
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
        BackgroundTapped = BackgroundTap.None;
    }

    void Blocking(PointerEventData eventData)
    {
        Debug.Log($"Blocking!");
    }

    void Dragging(PointerEventData eventData)
    {
        Vector2 dir = Vector2.ClampMagnitude(eventData.position - (Vector2)_outerCircleRect.position, _outerCircleRadius);
        if (dir.magnitude >= _boundary)
        {
            RunningSensorDeteted = true;
            _innerCircleImage.color = Color.black;
        }
        else
        {
            RunningSensorDeteted = false;
            _innerCircleImage.color = Color.gray;
        }

        _innerCircleRect.localPosition = dir;
        Direction = dir.normalized;
    }

    void EndDrag(PointerEventData eventData)
    {
        Direction = _innerCircleRect.localPosition = Vector3.zero;
        _innerCircleImage.color = Color.white;
    }
}
