using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public Definition.Scene SceneType { get; private set; } = Definition.Scene.UnKnown;

    void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        Manager.UI.CreateEventSystem();

        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        SceneType = Util.GetEnumValue<Definition.Scene>(currentSceneName);
    }
    public abstract void Clear();
}
