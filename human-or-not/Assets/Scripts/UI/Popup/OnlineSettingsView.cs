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
        Map_Name,
        Bot_Count,
        My_Name,
        User1_Name,
        User2_Name,
        User3_Name,
        User4_Name,
        User5_Name,
    }

    enum GameObjects
    {
        Map,
        Character,
        Bots,
    }

    // user1~5 Name
    Text[] userNames = new Text[5];
    public void UpdateUserName(string[] names)
    {
        ClearUserName();

        Debug.Log("Update user name!");

        int i = 0;
        foreach (string userName in names)
        {
            userNames[i].text = userName;
            i++;
        }
    }

    void ClearUserName()
    {
        Debug.Log($"Clear user name!");

        foreach (Text user in userNames)
        {
            user.text = "";
        }
    }

    // map name, image

    // character image

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

        userNames[0] = GetText((int)Texts.User1_Name);
        userNames[1] = GetText((int)Texts.User2_Name);
        userNames[2] = GetText((int)Texts.User3_Name);
        userNames[3] = GetText((int)Texts.User4_Name);
        userNames[4] = GetText((int)Texts.User5_Name);

        ClearUserName();
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
