using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopupUI : BaseUI
{
    void Awake()
    {
        Manager.UI.SetCanvas(gameObject, true);

        OnAwake();
    }

    protected virtual void ClosePopupUI()
    {
        Manager.UI.ClosePopupUI(this);
    }

    protected abstract void OnAwake();
}
