using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneUI : BaseUI
{
    void Awake()
    {
        Manager.UI.SetCanvas(gameObject, false);
    }
}
