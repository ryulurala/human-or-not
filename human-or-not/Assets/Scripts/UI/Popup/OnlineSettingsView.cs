using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineSettingsView : PopupUI
{
    enum Buttons
    {
        Close,
    }

    enum GameObjects
    {
        Map,
        Character,
        Bots,
    }


    protected override void OnStart()
    {
        base.OnStart();

        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        InitTabs();
        InitMapSelection();
        InitCharacterSelection();
        InitBotsSelection();
    }

    void InitTabs()
    {
        Button closeTab = GetButton((int)Buttons.Close);

        BindEvent(closeTab.gameObject, (PointerEventData) =>
        {
            Manager.Network.Close();
            Manager.UI.CloseAllPopupUI();
            Manager.UI.ShowPopupUI<PlaySettingsView>();
        });
    }

    void InitMapSelection()
    {
        GameObject map = GetObject((int)GameObjects.Map);

        BindEvent(map.gameObject, (PointerEventData) =>
        {
            Debug.Log("Map Click!!");
        });
    }

    void InitCharacterSelection()
    {
        GameObject character = GetObject((int)GameObjects.Character);

        BindEvent(character.gameObject, (PointerEventData) =>
        {
            Debug.Log("Character Click!!");
        });
    }

    void InitBotsSelection()
    {
        GameObject bots = GetObject((int)GameObjects.Bots);

        BindEvent(bots.gameObject, (PointerEventData) =>
        {
            Debug.Log("Bot Count Click!!");
        });
    }
}
