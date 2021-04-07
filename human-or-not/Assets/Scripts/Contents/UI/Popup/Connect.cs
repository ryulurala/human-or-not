using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Connect : PopupUI
{
    enum GameObjects
    {
        OK,
    }

    protected override void OnStart()
    {
        base.OnStart();

        Bind<GameObject>(typeof(GameObjects));

        InitButtons();
    }

    void InitButtons()
    {
        GameObject okBtn = GetObject((int)GameObjects.OK);

        BindEvent(okBtn, (PointerEventData) =>
        {
            ClosePopupUI();
        });
    }
}
