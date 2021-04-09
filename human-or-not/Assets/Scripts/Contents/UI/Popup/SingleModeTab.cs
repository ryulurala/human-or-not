using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleModeTab : PopupUI
{
    List<string> _characterList = new List<string>();
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
        Character_Dropdown,
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
        Dropdown characterDropdown = Get<Dropdown>((int)Dropdowns.Character_Dropdown);
        characterDropdown.ClearOptions();

        characterDropdown.AddOptions(_characterList);
    }

    void InitMapSettings()
    {
        Button leftBtn = GetButton((int)Buttons.Map_Left);
        Button rightBtn = GetButton((int)Buttons.Map_Right);

        Text mapText = GetText((int)Texts.Map_Text);
        int idx = 0;
        mapText.text = _mapList[idx];
        leftBtn.gameObject.SetActive(false);

        BindEvent(leftBtn.gameObject, (PointerEventData) =>
        {
            idx = idx < 1 ? idx : idx - 1;
            mapText.text = _mapList[idx];

            if (idx == 0)
                leftBtn.gameObject.SetActive(false);
            if (rightBtn.gameObject.activeSelf == false)
                rightBtn.gameObject.SetActive(true);
        });


        BindEvent(rightBtn.gameObject, (PointerEventData) =>
        {
            idx = idx < _mapList.Count - 1 ? idx + 1 : idx;
            mapText.text = _mapList[idx];

            if (idx == _mapList.Count - 1)
                rightBtn.gameObject.SetActive(false);
            if (leftBtn.gameObject.activeSelf == false)
                leftBtn.gameObject.SetActive(true);
        });
    }

    void InitList()
    {
        // 나중에 Serializable로 Json data 빌려오기
        _characterList.Add("Dongdong");
        _characterList.Add("???");
        _characterList.Add("!!!");

        _mapList.Add("SSU");
        _mapList.Add("HUFS");
        _mapList.Add("KKU");
    }
}
