using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaySettingsView : PopupUI
{
    enum GameObjects
    {
        Multi_Mode_Tab,
        Single_Mode_Tab,
        How_To_Play_Tab,
        Close_Tab,
    }

    protected override void OnStart()
    {
        base.OnStart();

        Bind<GameObject>(typeof(GameObjects));

        InitButtons();
    }

    void InitButtons()
    {
        GameObject multiModeTab = GetObject((int)GameObjects.Multi_Mode_Tab);
        GameObject singleModeTab = GetObject((int)GameObjects.Single_Mode_Tab);
        GameObject howToPlayTab = GetObject((int)GameObjects.How_To_Play_Tab);
        GameObject closeTab = GetObject((int)GameObjects.Close_Tab);

        Manager.UI.ShowPopupUI<HowToPlayTab>();

        BindEvent(multiModeTab, (PointerEventData) =>
        {
            Manager.UI.ClosePopupUI();
            Manager.UI.ShowPopupUI<MultiModeTab>();
        });

        BindEvent(singleModeTab, (PointerEventData) =>
        {
            Manager.UI.ClosePopupUI();
            Manager.UI.ShowPopupUI<SingleModeTab>();
        });

        BindEvent(howToPlayTab, (PointerEventData) =>
        {
            Manager.UI.ClosePopupUI();
            Manager.UI.ShowPopupUI<HowToPlayTab>();
        });

        BindEvent(closeTab, (PointerEventData) =>
        {
            Manager.UI.CloseAllPopupUI();
        });
    }
}
