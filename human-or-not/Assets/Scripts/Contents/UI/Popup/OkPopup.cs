using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OkPopup : PopupUI
{
    enum Buttons
    {
        OK,
    }

    protected override void OnStart()
    {
        base.OnStart();

        Bind<Button>(typeof(Buttons));

        InitButtons();
    }

    void InitButtons()
    {
        Button okBtn = GetButton((int)Buttons.OK);

        BindEvent(okBtn.gameObject, (PointerEventData) =>
        {
            ClosePopupUI();

            Manager.Scene.LoadScene(Define.Scene.Start);
        });
    }
}
