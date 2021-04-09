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

    protected override void OnStart()
    {
        base.OnStart();

        Bind<GameObject>(typeof(GameObjects));

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
