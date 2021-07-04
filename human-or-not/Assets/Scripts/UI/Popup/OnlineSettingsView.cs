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

    enum Images
    {
        Map_Image,
        Character_Image,
    }

    enum Texts
    {
        Room_Id,
        My_Name,
        Map_Name,
        Bot_Count,
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
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        InitTabs();
        InitSettings();

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

    void InitSettings()
    {
        Text roomId = GetText((int)Texts.Room_Id);
        roomId.text = Manager.Game.RoomId;

        Text myName = GetText((int)Texts.My_Name);
        myName.text = Manager.Player.MyPlayer.PlayerName;
    }

    void InitMapSelection()
    {

        Text mapName = GetText((int)Texts.Map_Name);
        mapName.text = "SSU";       // temp

        Image mapImange = GetImage((int)Images.Map_Image);


        // Host일 때
        GameObject map = GetObject((int)GameObjects.Map);

        BindEvent(map.gameObject, (PointerEventData) =>
        {
            Debug.Log("Map Click!!");
        });
    }

    void InitCharacterSelection()
    {
        GameObject character = GetObject((int)GameObjects.Character);

        // Host일 때
        BindEvent(character.gameObject, (PointerEventData) =>
        {
            Debug.Log("Character Click!!");
        });
    }

    void InitBotsSelection()
    {
        GameObject bots = GetObject((int)GameObjects.Bots);

        // Host일 때
        BindEvent(bots.gameObject, (PointerEventData) =>
        {
            Debug.Log("Bot Count Click!!");
        });
    }
}
