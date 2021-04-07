using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartMenu : SceneUI
{
    enum GameObjects
    {
        Background,
        Online,
        Local,
        HowToPlay,
        Credits,
    }

    protected override void OnStart()
    {
        base.OnStart();

        Bind<GameObject>(typeof(GameObjects));

        InitButtons();
    }

    void InitButtons()
    {
        GameObject online = GetObject((int)GameObjects.Online);
        GameObject local = GetObject((int)GameObjects.Local);
        GameObject howToPlay = GetObject((int)GameObjects.HowToPlay);
        GameObject credits = GetObject((int)GameObjects.Credits);

        BindEvent(online, (PointerEventData eventData) =>
        {
            Manager.UI.ShowPopupUI<Connect>();
        });
        BindEvent(local, (PointerEventData eventData) =>
        {
            Manager.UI.ShowPopupUI<Connect>();
        });
        BindEvent(howToPlay, (PointerEventData eventData) =>
        {
            Manager.Scene.LoadScene(Define.Scene.Game, async: true);
        });
        BindEvent(credits, (PointerEventData eventData) =>
        {
            Manager.UI.ShowPopupUI<Connect>();
        });
    }
}
