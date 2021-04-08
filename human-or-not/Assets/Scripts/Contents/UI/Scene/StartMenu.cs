using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartMenu : SceneUI
{
    enum GameObjects
    {
        Play,
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
        GameObject play = GetObject((int)GameObjects.Play);
        GameObject credits = GetObject((int)GameObjects.Credits);

        BindEvent(play, (PointerEventData eventData) =>
        {
            Manager.UI.ShowPopupUI<ConfigurePlay>();
        });
        BindEvent(credits, (PointerEventData eventData) =>
        {
            Manager.UI.ShowPopupUI<Credit>();
        });
    }
}
