using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class BaseScene : MonoBehaviour
{
    public Definition.Scene SceneType { get; private set; } = Definition.Scene.UnKnown;

    void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneType = Util.GetEnumValue<Definition.Scene>(currentSceneName);

        Manager.UI.CreateEventSystem();
    }

    public abstract void Clear();
}
