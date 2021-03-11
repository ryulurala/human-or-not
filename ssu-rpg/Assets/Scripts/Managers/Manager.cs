using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager _instance;
    static Manager Instance { get { if (_instance == null) Init(); return _instance; } }

    #region core
    InputManager _input = new InputManager();
    SceneManagerEx _scene = new SceneManagerEx();
    public static InputManager Input { get { return Instance._input; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    #endregion

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        GameObject go = GameObject.Find("@Manager");
        if (go == null)
        {
            go = new GameObject() { name = "@Manager" };
            _instance = go.AddComponent<Manager>();
        }
        // Scene이 이동해도 삭제 [X]
        DontDestroyOnLoad(go);
    }

    public static void Clear()
    {
        Input.Clear();
        Scene.Clear();
    }
}
