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

    protected override void OnAwake()
    {
        base.OnAwake();

        Bind<Button>(typeof(Buttons));
    }

    void Start()
    {
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
