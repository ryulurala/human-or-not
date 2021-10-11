using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : BaseScene
{
    protected override void OnAwake()
    {
        base.OnAwake();

        Manager.UI.ShowSceneUI<StartSceneUI>();
    }

    public override void Clear()
    {
        Debug.Log("StartScene Clear");
    }
}
