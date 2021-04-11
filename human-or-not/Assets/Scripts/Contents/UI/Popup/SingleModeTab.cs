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
        BotCount_Text,
    }

    enum Sliders
    {
        BotCount_Slider,
    }

    protected override void OnStart()
    {
        base.OnStart();

        Bind<Button>(typeof(Buttons));
        Bind<Dropdown>(typeof(Dropdowns));
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        Bind<Slider>(typeof(Sliders));

        InitList();     // 나중에 Serializable

        InitCharacterSettings();
        InitBotCountSettings();
        InitMapSettings();
        InitPlaySettings();
    }

    void InitPlaySettings()
    {
        Button playBtn = GetButton((int)Buttons.Play);
        Slider botCountSlider = GetSlider((int)Sliders.BotCount_Slider);

        BindEvent(playBtn.gameObject, (PointerEventData) =>
        {
            Manager.Game.BotCount = (ushort)botCountSlider.value;

            // Loading
            Manager.Scene.LoadScene(Define.Scene.Game);
        });
    }

    void InitBotCountSettings()
    {
        Slider botCountSlider = GetSlider((int)Sliders.BotCount_Slider);
        Text botCountText = GetText((int)Texts.BotCount_Text);

        // 0 ~ 100
        botCountSlider.minValue = 0;
        botCountSlider.maxValue = 100;
        botCountSlider.wholeNumbers = true;     // int

        BindEvent(botCountSlider.gameObject, (PointerEventData) =>
        {
            botCountText.text = botCountSlider.value.ToString();
        }, Define.UIEvent.OnDrag);

        BindEvent(botCountSlider.gameObject, (PointerEventData) =>
        {
            botCountText.text = botCountSlider.value.ToString();
        });
    }

    void InitCharacterSettings()
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
        // 나중에 Serializable로 Json data로 Update
        _characterList.Add("Dongdong");

        _mapList.Add("SSU");
        _mapList.Add("HUFS");
        _mapList.Add("KKU");
    }
}
