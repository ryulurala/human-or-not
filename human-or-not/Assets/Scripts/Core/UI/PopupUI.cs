using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUI : BaseUI
{
    void Awake()
    {
        Manager.UI.SetCanvas(gameObject, true);
    }

    protected virtual void ClosePopupUI()
    {
        Manager.UI.ClosePopupUI(this);
    }
}
