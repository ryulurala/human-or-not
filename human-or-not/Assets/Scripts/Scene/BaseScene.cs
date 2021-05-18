using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; private set; } = Define.Scene.UnKnown;

    void Awake()
    {
        OnAwake();
    }

    protected virtual void OnAwake()
    {
        Manager.UI.CreateEventSystem();

        string currentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        SceneType = Util.GetEnumValue<Define.Scene>(currentSceneName);
    }
    public abstract void Clear();
}
