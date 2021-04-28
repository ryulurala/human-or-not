using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager _instance;
    static Manager Instance { get { if (_instance == null) Init(); return _instance; } }

    #region Contents
    DataManager _data = new DataManager();
    GameManager _game = new GameManager();
    NetworkManager _network = new NetworkManager();
    PacketManager _packet = new PacketManager();
    PlayerManager _player = new PlayerManager();
    PoolManager _pool = new PoolManager();

    public static DataManager Data { get { return Instance._data; } }
    public static GameManager Game { get { return Instance._game; } }
    public static NetworkManager Network { get { return Instance._network; } }
    public static PacketManager Packet { get { return Instance._packet; } }
    public static PlayerManager Player { get { return Instance._player; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    #endregion

    #region Core
    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    UIManager _ui = new UIManager();

    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static UIManager UI { get { return Instance._ui; } }
    #endregion

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        GameObject go = GameObject.Find("@Manager");
        if (go == null)
            go = new GameObject() { name = "@Manager" };

        // Scene이 이동해도 삭제 [X]
        DontDestroyOnLoad(go);
        _instance = go.GetOrAddComponent<Manager>();
    }

    public static void Clear()
    {
        Scene.Clear();
        Input.Clear();
        UI.Clear();
    }

    public static Coroutine OpenCoroutine(IEnumerator coroutineFunc)
    {
        Coroutine coroutine = _instance.StartCoroutine(coroutineFunc);
        return coroutine;
    }

    public static void CloseCoroutine(Coroutine coroutine)
    {
        _instance.StopCoroutine(coroutine);
    }

    void OnApplicationQuit()
    {
        _network.Close();
    }
}
