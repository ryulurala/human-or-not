using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseUI : MonoBehaviour
{
    // Bind 된 모든 Objects
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    void Start()
    {
        OnStart();
    }

    protected abstract void OnStart();

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
            {
                Debug.Log($"Failed to bind: {names[i]}");
            }
        }
    }

    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected Text GetText(int idx) { return Get<Text>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
    protected Dropdown GetDropdown(int idx) { return Get<Dropdown>(idx); }
    protected Slider GetSlider(int idx) { return Get<Slider>(idx); }

    protected void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UIEventHandler eventHandler = go.GetOrAddComponent<UIEventHandler>();

        switch (type)
        {
            case Define.UIEvent.Click:
                eventHandler.OnClickHandler -= action;
                eventHandler.OnClickHandler += action;
                break;
            case Define.UIEvent.PointerDown:
                eventHandler.OnPointerDownHandler -= action;
                eventHandler.OnPointerDownHandler += action;
                break;
            case Define.UIEvent.PointerUp:
                eventHandler.OnPointerUpHandler -= action;
                eventHandler.OnPointerUpHandler += action;
                break;
            case Define.UIEvent.OnDrag:
                eventHandler.OnDragHandler -= action;
                eventHandler.OnDragHandler += action;
                break;
        }
    }
}
