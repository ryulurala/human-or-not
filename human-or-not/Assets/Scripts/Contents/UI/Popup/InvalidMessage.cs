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

    void Awake()
    {
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
