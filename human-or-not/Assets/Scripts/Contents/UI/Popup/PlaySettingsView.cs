using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaySettingsView : PopupUI
{
    enum Buttons
    {
        Multi_Mode,
        Single_Mode,
        How_To_Play,
        Close,
    }

    enum Tab
    {
        MultiMode,
        SingleMode,
        HowToPlay,
    }

    protected override void OnStart()
    {
        base.OnStart();

        Bind<Button>(typeof(Buttons));

        InitButtons();
    }


    void InitButtons()
    {
        Button multiModeTab = GetButton((int)Buttons.Multi_Mode);
        Button singleModeTab = GetButton((int)Buttons.Single_Mode);
        Button howToPlayTab = GetButton((int)Buttons.How_To_Play);
        Button closeTab = GetButton((int)Buttons.Close);

        Manager.UI.ShowPopupUI<HowToPlayTab>();
        Tab currentTab = Tab.HowToPlay;

        BindEvent(multiModeTab.gameObject, (PointerEventData) =>
        {
            if (currentTab != Tab.MultiMode)
            {
                Manager.UI.ClosePopupUI();
                Manager.UI.ShowPopupUI<MultiModeTab>();

                currentTab = Tab.MultiMode;
            }
        });

        BindEvent(singleModeTab.gameObject, (PointerEventData) =>
        {
            if (currentTab != Tab.SingleMode)
            {
                Manager.UI.ClosePopupUI();
                Manager.UI.ShowPopupUI<SingleModeTab>();

                currentTab = Tab.SingleMode;
            }
        });

        BindEvent(howToPlayTab.gameObject, (PointerEventData) =>
        {
            if (currentTab != Tab.HowToPlay)
            {
                Manager.UI.ClosePopupUI();
                Manager.UI.ShowPopupUI<HowToPlayTab>();

                currentTab = Tab.HowToPlay;
            }
        });

        BindEvent(closeTab.gameObject, (PointerEventData) =>
        {
            Manager.UI.CloseAllPopupUI();
        });
    }
}
