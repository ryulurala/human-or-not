using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvalidMessage : PopupUI
{
    enum Buttons
    {
        Close,
    }

    protected override void OnStart()
    {
        base.OnStart();

        Bind<Button>(typeof(Buttons));

        InitButtons();
    }

    void InitButtons()
    {
        Button closeBtn = GetButton((int)Buttons.Close);

        BindEvent(closeBtn.gameObject, (PointerEventData) =>
        {
            ClosePopupUI();
        });
    }
}
