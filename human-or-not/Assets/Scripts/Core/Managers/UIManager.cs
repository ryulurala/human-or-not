using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager
{
    int _order = 8;
    Stack<PopupUI> _popupStack = new Stack<PopupUI>();
    SceneUI _sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UIs");
            if (root == null)
                root = new GameObject() { name = "@UIs" };

            return root;
        }
    }

    public void SetCanvas(GameObject go, bool isSorting = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Canvas 안에 Canvas가 중첩할 경우 자신만의 sorting order를 가짐.
        canvas.overrideSorting = true;

        if (isSorting)
            canvas.sortingOrder = _order++;     // Popup UI
        else
            canvas.sortingOrder = 0;        // Scene UI
    }

    public T ShowSceneUI<T>(string name = null) where T : SceneUI
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Manager.Resource.Instaniate($"UI/Scene/{name}");
        T sceneUI = Util.GetOrAddComponent<T>(go);
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);

        return sceneUI;
    }

    public T ShowPopupUI<T>(string name = null) where T : PopupUI
    {
        // name이 없을 경우 T의 이름으로
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Manager.Resource.Instaniate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform);

        return popup;
    }

    public void ClosePopupUI(PopupUI popup = null)
    {
        if (_popupStack.Count == 0)
            return;

        if (popup != null && _popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        popup = _popupStack.Pop();
        _order--;

        Manager.Resource.Destroy(popup.gameObject);
        popup = null;
    }

    void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Init()
    {
        if (Util.IsMobile)
            ShowSceneUI<GamePad>();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        _sceneUI = null;
    }

    public void CreateEventSystem()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
        {
            obj = new GameObject();
            ((GameObject)obj).AddComponent<EventSystem>();
            ((GameObject)obj).AddComponent<StandaloneInputModule>();
        }
        obj.name = "@EventSystem";
    }
}
