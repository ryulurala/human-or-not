using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingMessage : PopupUI
{
    enum Buttons
    {
        Cancel,
    }

    protected override void OnAwake()
    {
        Bind<Button>(typeof(Buttons));
    }

    void Start()
    {
        InitButtons();
    }

    void InitButtons()
    {
        Button cancelBtn = GetButton((int)Buttons.Cancel);

        BindEvent(cancelBtn.gameObject, (PointerEventData) =>
        {
            Manager.Network.Close();
            ClosePopupUI();
        });
    }
}
