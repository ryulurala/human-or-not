using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaySettingsView : PopupUI
{
    enum Buttons
    {
        HostMode,
        ClientMode,
        TrainingMode,
        Close,
    }

    enum Tab
    {
        HostMode,
        ClientMode,
        TraningMode,
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
        Button hostTab = GetButton((int)Buttons.HostMode);
        Button clientTab = GetButton((int)Buttons.ClientMode);
        Button trainingTab = GetButton((int)Buttons.TrainingMode);
        Button closeTab = GetButton((int)Buttons.Close);

        Manager.UI.ShowPopupUI<PlayHostTab>();
        Tab currentTab = Tab.HostMode;

        BindEvent(hostTab.gameObject, (PointerEventData) =>
        {
            if (currentTab != Tab.HostMode)
            {
                Manager.UI.ClosePopupUI();
                Manager.UI.ShowPopupUI<PlayHostTab>();

                currentTab = Tab.HostMode;
            }
        });

        BindEvent(clientTab.gameObject, (PointerEventData) =>
        {
            if (currentTab != Tab.ClientMode)
            {
                Manager.UI.ClosePopupUI();
                Manager.UI.ShowPopupUI<PlayClientTab>();

                currentTab = Tab.ClientMode;
            }
        });

        BindEvent(trainingTab.gameObject, (PointerEventData) =>
        {
            if (currentTab != Tab.TraningMode)
            {
                Manager.UI.ClosePopupUI();
                Manager.UI.ShowPopupUI<PlayTrainingTab>();

                currentTab = Tab.TraningMode;
            }
        });

        BindEvent(closeTab.gameObject, (PointerEventData) =>
        {
            Manager.UI.CloseAllPopupUI();
        });
    }
}
