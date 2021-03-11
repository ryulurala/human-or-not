using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.UnKnown;

    void Awake()
    {
        OnAwake();
    }

    protected abstract void OnAwake();
    public abstract void Clear();
}
