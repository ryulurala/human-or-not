using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUI : BaseUI
{
    protected override void OnAwake()
    {
        Manager.UI.SetCanvas(gameObject, false);
    }
}
