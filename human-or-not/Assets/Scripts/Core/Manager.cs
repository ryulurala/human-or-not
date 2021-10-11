using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    static Manager _instance;
    public static Manager Instance { get { if (_instance == null) Init(); return _instance; } }

    #region Contents
    DataManager _data = new DataManager();
    GameManager _game = new GameManager();
    NetworkManager _network = new NetworkManager();
    PacketManager _packet = new PacketManager();
    PlayerManager _player = new PlayerManager();
    PoolManager _pool = new PoolManager();

    public static DataManager Data { get => Instance._data; }
    public static GameManager Game { get => Instance._game; }
    public static NetworkManager Network { get => Instance._network; }
    public static PacketManager Packet { get => Instance._packet; }
    public static PlayerManager Player { get => Instance._player; }
    public static PoolManager Pool { get => Instance._pool; }
    #endregion

    #region Core
    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    UIManager _ui = new UIManager();

    public static InputManager Input { get => Instance._input; }
    public static ResourceManager Resource { get => Instance._resource; }
    public static SceneManagerEx Scene { get => Instance._scene; }
    public static UIManager UI { get => Instance._ui; }
    #endregion

    static void Init()
    {
        GameObject go = GameObject.Find("@Manager");
        if (go == null)
            go = new GameObject() { name = "@Manager" };

        // Scene이 이동해도 삭제 [X]
        DontDestroyOnLoad(go);
        _instance = go.GetOrAddComponent<Manager>();
    }

    void Update()
    {
        // 입력을 누르고
        _input.OnUpdate();
        // 네트워크 통신하고
        _network.OnUpdate();
    }

    // 앱 종료할 때 호출
    void OnApplicationQuit()
    {
        _network.Close();
    }

    public Coroutine OpenCoroutine(IEnumerator coroutineFunc)
    {
        Coroutine coroutine = _instance.StartCoroutine(coroutineFunc);
        return coroutine;
    }

    public void CloseCoroutine(Coroutine coroutine)
    {
        _instance.StopCoroutine(coroutine);
    }
}
