using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConfigurePlay : PopupUI
{
    enum GameObjects
    {
        Online_Tab,
        Online_Contents,
        Single_Tab,
        Single_Contents,
        HTP_Tab,
        HTP_Contents,
        Play,
        Close,
    }

    protected override void OnStart()
    {
        base.OnStart();

        Bind<GameObject>(typeof(GameObjects));

        InitButtons();
    }

    void InitButtons()
    {
        GameObject onlineTabBtn = GetObject((int)GameObjects.Online_Tab);
        GameObject onlineContents = GetObject((int)GameObjects.Online_Contents);
        GameObject singleTabBtn = GetObject((int)GameObjects.Single_Tab);
        GameObject singleContents = GetObject((int)GameObjects.Single_Contents);
        GameObject htpTabBtn = GetObject((int)GameObjects.HTP_Tab);
        GameObject htpContents = GetObject((int)GameObjects.HTP_Contents);
        GameObject closeBtn = GetObject((int)GameObjects.Close);

        onlineContents.SetActive(true);
        singleContents.SetActive(false);
        htpContents.SetActive(false);

        // Temp
        GameObject playBtn = GetObject((int)GameObjects.Play);
        BindEvent(playBtn, (PointerEventData) =>
        {
            Manager.Scene.LoadScene(Define.Scene.Game, true);
        });

        BindEvent(onlineTabBtn, (PointerEventData) =>
        {
            onlineContents.SetActive(true);
            singleContents.SetActive(false);
            htpContents.SetActive(false);
        });
        BindEvent(singleTabBtn, (PointerEventData) =>
        {
            onlineContents.SetActive(false);
            singleContents.SetActive(true);
            htpContents.SetActive(false);
        });
        BindEvent(htpTabBtn, (PointerEventData) =>
        {
            onlineContents.SetActive(false);
            singleContents.SetActive(false);
            htpContents.SetActive(true);
        });

        BindEvent(closeBtn, (PointerEventData) =>
        {
            ClosePopupUI();
        });
    }
}
