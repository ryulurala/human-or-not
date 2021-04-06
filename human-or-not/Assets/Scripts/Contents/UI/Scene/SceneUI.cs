using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUI : BaseUI
{
    protected override void OnStart()
    {
        Manager.UI.SetCanvas(gameObject, false);
    }
}
