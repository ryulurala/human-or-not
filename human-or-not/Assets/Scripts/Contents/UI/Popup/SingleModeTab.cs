using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SingleModeTab : PopupUI
{
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

        InitCharacterSettings();
        InitBotCountSettings();
        InitMapSettings();
        InitPlaySettings();
    }

    void InitPlaySettings()
    {
        Button playBtn = GetButton((int)Buttons.Play);
        Slider botCountSlider = GetSlider((int)Sliders.BotCount_Slider);
        Text mapText = GetText((int)Texts.Map_Text);

        BindEvent(playBtn.gameObject, (PointerEventData) =>
        {
            Manager.Game.BotCount = (ushort)botCountSlider.value;

            string text = mapText.text;
            Debug.Log($"text: {text}");
            Define.Scene type = Util.GetEnumValue<Define.Scene>(text);

            // Loading
            if (type != default(Define.Scene))
                Manager.Scene.LoadScene(type);
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

        string[] characters = Enum.GetNames(typeof(Define.Character));

        characterDropdown.AddOptions(new List<string>(characters));
    }

    void InitMapSettings()
    {
        Button leftBtn = GetButton((int)Buttons.Map_Left);
        Button rightBtn = GetButton((int)Buttons.Map_Right);
        Text mapText = GetText((int)Texts.Map_Text);

        string[] maps = Enum.GetNames(typeof(Define.Map));
        int idx = 0;
        mapText.text = maps[idx];
        leftBtn.gameObject.SetActive(false);

        BindEvent(leftBtn.gameObject, (PointerEventData) =>
        {
            idx = idx < 1 ? idx : idx - 1;
            mapText.text = maps[idx];

            if (idx == 0)
                leftBtn.gameObject.SetActive(false);
            if (rightBtn.gameObject.activeSelf == false)
                rightBtn.gameObject.SetActive(true);
        });


        BindEvent(rightBtn.gameObject, (PointerEventData) =>
        {
            idx = idx < maps.Length - 1 ? idx + 1 : idx;
            mapText.text = maps[idx];

            if (idx == maps.Length - 1)
                rightBtn.gameObject.SetActive(false);
            if (leftBtn.gameObject.activeSelf == false)
                leftBtn.gameObject.SetActive(true);
        });
    }
}
