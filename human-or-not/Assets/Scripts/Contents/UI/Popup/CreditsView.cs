using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CreditsView : PopupUI
{
    enum GameObjects
    {
        Close,
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
        GameObject closeBtn = GetObject((int)GameObjects.Close);

        BindEvent(closeBtn, (PointerEventData) =>
        {
            ClosePopupUI();
        });
    }
}
