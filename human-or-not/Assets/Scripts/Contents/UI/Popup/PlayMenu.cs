using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayMenu : PopupUI
{
    enum GameObjects
    {
        OnlineTab,
        OnlineContents,
        LocalTab,
        LocalContents,
        TutorialTab,
        TutorialContents,
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
        GameObject onlineTabBtn = GetObject((int)GameObjects.OnlineTab);
        GameObject onlineContents = GetObject((int)GameObjects.OnlineContents);
        GameObject localTabBtn = GetObject((int)GameObjects.LocalTab);
        GameObject localContents = GetObject((int)GameObjects.LocalContents);
        GameObject tutorialTabBtn = GetObject((int)GameObjects.TutorialTab);
        GameObject tutorialContents = GetObject((int)GameObjects.TutorialContents);
        GameObject closeBtn = GetObject((int)GameObjects.Close);

        onlineContents.SetActive(false);
        localContents.SetActive(false);
        tutorialContents.SetActive(false);

        // Temp
        GameObject playBtn = GetObject((int)GameObjects.Play);
        BindEvent(playBtn, (PointerEventData)=>
        {
            Manager.Scene.LoadScene(Define.Scene.Game, true);
        });

        BindEvent(onlineTabBtn, (PointerEventData) =>
        {
            onlineContents.SetActive(true);
            localContents.SetActive(false);
            tutorialContents.SetActive(false);
        });
        BindEvent(localTabBtn, (PointerEventData) =>
        {
            onlineContents.SetActive(false);
            localContents.SetActive(true);
            tutorialContents.SetActive(false);
        });
        BindEvent(tutorialTabBtn, (PointerEventData) =>
        {
            onlineContents.SetActive(false);
            localContents.SetActive(false);
            tutorialContents.SetActive(true);
        });

        BindEvent(closeBtn, (PointerEventData) =>
        {
            ClosePopupUI();
        });
    }
}
