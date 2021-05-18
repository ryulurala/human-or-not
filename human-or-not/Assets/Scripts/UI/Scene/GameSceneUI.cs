using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameSceneUI : SceneUI
{
    float _remainSeconds;

    enum Texts
    {
        Time,
    }

    enum Buttons
    {
        Settings,
        Map,
    }

    protected override void OnStart()
    {
        base.OnStart();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));

        if (Util.IsMobile)
            Manager.Input.GamePad = Manager.UI.OverrideSceneUI<GamePad>();

        InitSettingsButton();
        InitMapButton();
        InitTimer();
    }

    void InitSettingsButton()
    {
        Button settingsBtn = GetButton((int)Buttons.Settings);

        BindEvent(settingsBtn.gameObject, (PointerEventData) =>
        {
            Debug.Log($"Settings Button Clicked !");
        });
    }

    void InitMapButton()
    {
        Button mapBtn = GetButton((int)Buttons.Map);

        BindEvent(mapBtn.gameObject, (PointerEventData) =>
        {
            Debug.Log($"Map Button Clicked !");
        });
    }

    void InitTimer()
    {
        _remainSeconds = 10f;
        GetText((int)Texts.Time).text = SecondsToTimerStr(_remainSeconds);
        StartCoroutine(FlowTime());
    }

    IEnumerator FlowTime()
    {
        while (_remainSeconds > 0)
        {
            yield return new WaitForSeconds(1f);
            _remainSeconds -= 1f;       // Time.deltaTime? or Time.fixedDeltaTime?
            GetText((int)Texts.Time).text = SecondsToTimerStr(_remainSeconds);
        }

        Manager.Game.EndGame();
    }

    string SecondsToTimerStr(float seconds)
    {
        return String.Format("{0:D2}:{1:D2}", (int)seconds / 60, (int)seconds % 60);
    }

    #region GamePad
    public class GamePad : SceneUI
    {
        public bool Zoomed { get; private set; } = false;
        public float ZoomValue
        {
            get
            {
                Slider slider = GetSlider((int)Sliders.ZoomSlider).GetComponent<Slider>();
                if (slider == null)
                {
                    CameraController cameraController = Camera.main.GetComponent<CameraController>();
                    return (cameraController.MaxZoomRatio + cameraController.MinZoomRatio) / 2f;
                }

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

        enum Images
        {
            RotatePanel,
            Joystick,
            OuterCircle,
            InnerCircle,
            Zoom,
        }

        enum Sliders
        {
            ZoomSlider,
        }

        enum Buttons
        {
            Attack,
            Jump,
        }

        protected override void OnStart()
        {
            // Binding
            Bind<Image>(typeof(Images));
            Bind<Button>(typeof(Buttons));
            Bind<Slider>(typeof(Sliders));

            InitJoystick();
            InitRotatePanel();
            InitTriggerButtons();
            InitZoomSlider();
        }

        void InitJoystick()
        {
            Image joystick = GetImage((int)Images.Joystick);
            Image outerCircle = GetImage((int)Images.OuterCircle);
            Image innerCircle = GetImage((int)Images.InnerCircle);

            float outerCircleRadius = outerCircle.rectTransform.rect.width * 0.5f;
            float boundary = outerCircleRadius * 0.85f;

            BindEvent(joystick.gameObject, (PointerEventData eventData) =>
            {
                outerCircle.gameObject.SetActive(true);
                innerCircle.rectTransform.position = outerCircle.rectTransform.position = eventData.position;
            }, Define.UIEvent.PointerDown);

            BindEvent(joystick.gameObject, (PointerEventData eventData) =>
            {
                Vector2 dir = Vector2.ClampMagnitude(eventData.position - (Vector2)outerCircle.rectTransform.position, outerCircleRadius);
                if (dir.magnitude < boundary)
                {
                    // 걷기
                    if (JoyStickDetected != JoystickDetect.CloseToCenter)
                    {
                        JoyStickDetected = JoystickDetect.CloseToCenter;
                        innerCircle.color = Color.gray;    // 무분별한 색깔 전환 방지
                    }
                }
                else
                {
                    // 뛰기
                    if (JoyStickDetected != JoystickDetect.FarFromCenter)
                    {
                        JoyStickDetected = JoystickDetect.FarFromCenter;
                        innerCircle.color = Color.black;   // 무분별한 색깔 전환 방지
                    }
                }
                innerCircle.rectTransform.localPosition = dir;
                Direction = dir.normalized;
            }, Define.UIEvent.OnDrag);

            BindEvent(joystick.gameObject, (PointerEventData eventData) =>
            {
                Direction = innerCircle.rectTransform.localPosition = Vector3.zero;
                innerCircle.color = Color.white;
                outerCircle.gameObject.SetActive(false);

                JoyStickDetected = JoystickDetect.Center;
            }, Define.UIEvent.PointerUp);

        }

        void InitRotatePanel()
        {
            Image panel = GetImage((int)Images.RotatePanel);

            BindEvent(panel.gameObject, (PointerEventData eventData) =>
            {
                RotatePanelTapped = RotatePanelTap.Begin;

                TouchPoint = eventData.position;
            }, Define.UIEvent.PointerDown);

            BindEvent(panel.gameObject, (PointerEventData eventData) =>
            {
                if (RotatePanelTapped == RotatePanelTap.Begin)
                    RotatePanelTapped = RotatePanelTap.On;

                // 짧은 순간에 거리 제곱 차가 클 경우 무시
                if ((Camera.main.ScreenToViewportPoint(TouchPoint) - Camera.main.ScreenToViewportPoint(eventData.position)).magnitude > 0.1f)
                    return;

                TouchPoint = eventData.position;
            }, Define.UIEvent.OnDrag);

            BindEvent(panel.gameObject, (PointerEventData eventData) => { RotatePanelTapped = RotatePanelTap.None; }, Define.UIEvent.PointerUp);
        }

        void InitZoomSlider()
        {
            Image zoom = GetImage((int)Images.Zoom);
            Slider zoomSlider = GetSlider((int)Sliders.ZoomSlider);

            // 1 ~ 10 단계
            CameraController cameraController = Camera.main.GetComponent<CameraController>();
            zoomSlider.minValue = cameraController.MinZoomRatio;
            zoomSlider.maxValue = cameraController.MaxZoomRatio;
            zoomSlider.value = (cameraController.MaxZoomRatio + cameraController.MinZoomRatio) / 2f;

            BindEvent(zoom.gameObject, (PointerEventData) => { }); // blocking
            BindEvent(zoomSlider.gameObject, (PointerEventData eventData) => { Zoomed = true; }, Define.UIEvent.PointerDown);
            BindEvent(zoomSlider.gameObject, (PointerEventData eventData) => { Zoomed = false; }, Define.UIEvent.PointerUp);
        }

        void InitTriggerButtons()
        {
            Button attackButton = GetButton((int)Buttons.Attack);
            Button jumpButton = GetButton((int)Buttons.Jump);

            BindEvent(attackButton.gameObject, (PointerEventData eventData) => { ButtonClicked = ButtonClick.Attack; }, Define.UIEvent.PointerDown);
            BindEvent(attackButton.gameObject, (PointerEventData eventData) => { ButtonClicked = ButtonClick.None; }, Define.UIEvent.PointerUp);

            BindEvent(jumpButton.gameObject, (PointerEventData eventData) => { ButtonClicked = ButtonClick.Jump; }, Define.UIEvent.PointerDown);
            BindEvent(jumpButton.gameObject, (PointerEventData eventData) => { ButtonClicked = ButtonClick.None; }, Define.UIEvent.PointerUp);
        }
    }
    #endregion
}
