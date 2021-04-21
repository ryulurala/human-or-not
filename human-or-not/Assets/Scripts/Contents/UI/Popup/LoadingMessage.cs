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

    enum Images
    {
        Fill,
    }

    protected override void OnStart()
    {
        base.OnStart();

        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        InitButtons();
        InitLoadingBar();
    }

    void InitButtons()
    {
        Button cancelBtn = GetButton((int)Buttons.Cancel);

        BindEvent(cancelBtn.gameObject, (PointerEventData) =>
        {
            ClosePopupUI();

            // TODO: 취소
        });
    }

    void InitLoadingBar()
    {
        Image bar = GetImage((int)Images.Fill);
    }
}
