using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : SceneUI
{
    enum GameObjects
    {
        Play,
        Credits,
    }

    void Awake()
    {
        Bind<GameObject>(typeof(GameObjects));
    }

    void Start()
    {
        InitButtons();
    }

    void InitButtons()
    {
        GameObject play = GetObject((int)GameObjects.Play);
        GameObject credits = GetObject((int)GameObjects.Credits);

        BindEvent(play, (PointerEventData eventData) =>
        {
            Manager.UI.ShowPopupUI<PlaySettingsView>();
        });
        BindEvent(credits, (PointerEventData eventData) =>
        {
            Manager.UI.ShowPopupUI<CreditsView>();
        });
    }
}
