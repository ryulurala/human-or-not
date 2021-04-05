using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePad : SceneUI
{
    public bool Zoomed { get; private set; } = false;
    public float ZoomValue
    {
        get
        {
            Slider slider = GetObject((int)GameObjects.Slider).GetComponent<Slider>();
            if (slider == null)
                return 2f;

            return slider.value;
        }
    }
    public Vector2 Direction { get; private set; } = Vector2.zero;
    public JoystickDetect JoyStickDetected { get; private set; } = JoystickDetect.Center;
    public enum JoystickDetect
    {
        Center,
        CloseToCenter,
        FarFromCenter,
    }

    public Vector2 TouchPoint { get; private set; } = Vector2.zero;
    public RotatePanelTap RotatePanelTapped { get; private set; } = RotatePanelTap.None;
    public enum RotatePanelTap
    {
        None,
        Begin,
        On,
    }
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
        Zoom,
        Slider,
    }

    protected override void OnStart()
    {
        // Set Canvas
        base.OnStart();

        // Binding
        Bind<GameObject>(typeof(GameObjects));

        InitJoystick();
        InitRotatePanel();
        InitTriggerButtons();
        InitZoomSlider();
    }

    void InitJoystick()
    {
        GameObject joystick = GetObject((int)GameObjects.Joystick);
        GameObject outerCircle = GetObject((int)GameObjects.OuterCircle);
        GameObject innerCircle = GetObject((int)GameObjects.InnerCircle);

        RectTransform outerCircleRect = outerCircle.GetComponent<RectTransform>();
        RectTransform innerCircleRect = innerCircle.GetComponent<RectTransform>();
        Image innerCircleImage = innerCircle.GetComponent<Image>();

        float outerCircleRadius = outerCircleRect.rect.width * 0.5f;
        float boundary = outerCircleRadius * 0.85f;

        BindEvent(joystick, (PointerEventData eventData) =>
        {
            outerCircle.SetActive(true);
            innerCircleRect.position = outerCircleRect.position = eventData.position;
        }, Define.UIEvent.PointerDown);

        BindEvent(joystick, (PointerEventData eventData) =>
        {
            Vector2 dir = Vector2.ClampMagnitude(eventData.position - (Vector2)outerCircleRect.position, outerCircleRadius);
            if (dir.magnitude < boundary)
            {
                // 걷기
                if (JoyStickDetected != JoystickDetect.CloseToCenter)
                {
                    JoyStickDetected = JoystickDetect.CloseToCenter;
                    innerCircleImage.color = Color.gray;    // 무분별한 색깔 전환 방지
                }
            }
            else
            {
                // 뛰기
                if (JoyStickDetected != JoystickDetect.FarFromCenter)
                {
                    JoyStickDetected = JoystickDetect.FarFromCenter;
                    innerCircleImage.color = Color.black;   // 무분별한 색깔 전환 방지
                }
            }
            innerCircleRect.localPosition = dir;
            Direction = dir.normalized;
        }, Define.UIEvent.OnDrag);

        BindEvent(joystick, (PointerEventData eventData) =>
        {
            Direction = innerCircleRect.localPosition = Vector3.zero;
            innerCircleImage.color = Color.white;
            outerCircle.SetActive(false);

            JoyStickDetected = JoystickDetect.Center;
        }, Define.UIEvent.PointerUp);

    }

    void InitRotatePanel()
    {
        GameObject panel = GetObject((int)GameObjects.Panel);

        BindEvent(panel, (PointerEventData eventData) =>
        {
            RotatePanelTapped = RotatePanelTap.Begin;

            TouchPoint = eventData.position;
        }, Define.UIEvent.PointerDown);

        BindEvent(panel, (PointerEventData eventData) =>
        {
            if (RotatePanelTapped == RotatePanelTap.Begin)
                RotatePanelTapped = RotatePanelTap.On;

            // 짧은 순간에 거리 제곱 차가 클 경우 무시
            if ((Camera.main.ScreenToViewportPoint(TouchPoint) - Camera.main.ScreenToViewportPoint(eventData.position)).magnitude > 0.1f)
                return;

            TouchPoint = eventData.position;
        }, Define.UIEvent.OnDrag);

        BindEvent(panel, (PointerEventData eventData) => { RotatePanelTapped = RotatePanelTap.None; }, Define.UIEvent.PointerUp);
    }

    void InitZoomSlider()
    {
        GameObject zoom = GetObject((int)GameObjects.Zoom);
        GameObject slider = GetObject((int)GameObjects.Slider);
        Slider zoomSlider = slider.GetComponent<Slider>();

        // 1 ~ 10 단계
        CameraController cameraController = Camera.main.GetComponent<CameraController>();
        zoomSlider.minValue = cameraController.MinZoomRatio;
        zoomSlider.maxValue = cameraController.MaxZoomRatio;
        zoomSlider.value = (cameraController.MaxZoomRatio + cameraController.MinZoomRatio) / 2f;

        BindEvent(zoom, (PointerEventData) => { }); // blocking
        BindEvent(slider, (PointerEventData eventData) => { Zoomed = true; }, Define.UIEvent.PointerDown);
        BindEvent(slider, (PointerEventData eventData) => { Zoomed = false; }, Define.UIEvent.PointerUp);
    }

    void InitTriggerButtons()
    {
        GameObject attackButton = GetObject((int)GameObjects.Attack);
        GameObject jumpButton = GetObject((int)GameObjects.Jump);

        BindEvent(attackButton, (PointerEventData eventData) => { ButtonClicked = ButtonClick.Attack; }, Define.UIEvent.PointerDown);
        // BindEvent(attackButton, (PointerEventData eventData) => { ButtonClicked = ButtonClick.None; }, Define.UIEvent.PointerUp);

        BindEvent(jumpButton, (PointerEventData eventData) => { ButtonClicked = ButtonClick.Jump; }, Define.UIEvent.PointerDown);
        // BindEvent(jumpButton, (PointerEventData eventData) => { ButtonClicked = ButtonClick.None; }, Define.UIEvent.PointerUp);
    }
}
