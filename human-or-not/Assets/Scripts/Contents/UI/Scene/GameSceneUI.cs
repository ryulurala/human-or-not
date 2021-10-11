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

    protected override void OnAwake()
    {
        base.OnAwake();

        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
    }

    void Start()
    {
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
        // StartCoroutine(FlowTime());
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
}
