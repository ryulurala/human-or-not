using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SingleModeTab : PopupUI
{
    List<string> _skinList = new List<string>();
    List<string> _mapList = new List<string>();

    enum Buttons
    {
        Map_Left,
        Map_Right,
        Play,
    }

    enum Images
    {
        Map_Image,
    }

    enum Dropdowns
    {
        Skin_Dropdown,
    }

    enum Texts
    {
        Map_Text,
    }

    protected override void OnStart()
    {
        base.OnStart();

        Bind<Button>(typeof(Buttons));
        Bind<Dropdown>(typeof(Dropdowns));
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        InitList();

        InitDropDown();
        InitMapSettings();

        Button playBtn = GetButton((int)Buttons.Play);
        BindEvent(playBtn.gameObject, (PointerEventData) => { Manager.Scene.LoadScene(Define.Scene.Game); });
    }

    void InitDropDown()
    {
        Dropdown skinInput = Get<Dropdown>((int)Dropdowns.Skin_Dropdown);
        skinInput.ClearOptions();

        skinInput.AddOptions(_skinList);
    }

    void InitMapSettings()
    {
        Button leftBtn = GetButton((int)Buttons.Map_Left);
        Button rightBtn = GetButton((int)Buttons.Map_Right);

        Text mapText = GetText((int)Texts.Map_Text);
        mapText.text = _mapList[0];

        BindEvent(leftBtn.gameObject, (PointerEventData) =>
        {

            Debug.Log($"Map_LeftBtn Clicked !");
        });

        BindEvent(rightBtn.gameObject, (PointerEventData) =>
        {

            Debug.Log($"Map_RightBtn Clicked !");
        });
    }

    void InitList()
    {
        _skinList.Add("Orange");
        _skinList.Add("Yellow");
        _skinList.Add("Green");
        _skinList.Add("Blue");
        _skinList.Add("Purple");

        _mapList.Add("Seoul-University");
        _mapList.Add("Soongsil-University");
    }
}
