using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePad : SceneUI
{
    RectTransform _backgroundRect;
    RectTransform _handleRect;
    float _backgroundRadius;

    public Vector2 Direction { get; private set; } = Vector3.zero;

    #region InputPad
    static GamePad _pad = null;
    public static GamePad Pad
    {
        get
        {
            if (_pad == null)
            {
                GameObject go = GameObject.Find("@GamePad");
                if (go == null)
                {
                    go = Manager.Resource.Instaniate($"UI/Scene/{typeof(GamePad)}");
                    go.name = "@GamePad";
                }

                _pad = go.GetComponent<GamePad>();
                DontDestroyOnLoad(_pad);

                _pad.gameObject.SetActive(false);
            }
            return _pad;
        }
    }

    bool _buttonA = false;
    bool _buttonJ = false;
    bool _buttonR = false;

    public enum PadCode
    {
        ButtonR,
        ButtonA,
        ButtonJ,
    }

    public bool GetPad(PadCode padCode)
    {
        switch (padCode)
        {
            case PadCode.ButtonA:
                return GamePad.Pad._buttonA;
            case PadCode.ButtonJ:
                return GamePad.Pad._buttonJ;
            case PadCode.ButtonR:
                return GamePad.Pad._buttonR;
            default:
                return false;
        }
    }
    #endregion

    enum GameObjects
    {
        Background,
        Handle,
    }

    protected override void OnStart()
    {
        // Set Canvas
        base.OnStart();

        // Binding
        Bind<GameObject>(typeof(GameObjects));

        // Component get
        GameObject background = GetObject((int)GameObjects.Background);
        GameObject handle = GetObject((int)GameObjects.Handle);

        _backgroundRect = background.GetComponent<RectTransform>();
        _handleRect = handle.GetComponent<RectTransform>();
        _backgroundRadius = _backgroundRect.rect.width * 0.5f;

        BindEvent(handle, StartDrag, Define.UIEvent.DragStart);
        BindEvent(handle, Dragging, Define.UIEvent.Dragging);
        BindEvent(handle, EndDrag, Define.UIEvent.DragEnd);
    }

    void StartDrag(PointerEventData eventData)
    {
        Dragging(eventData);
    }

    void Dragging(PointerEventData eventData)
    {
        Vector2 dir = Vector2.ClampMagnitude(eventData.position - (Vector2)_backgroundRect.position, _backgroundRadius);

        _handleRect.localPosition = dir;
        Direction = dir.normalized;
    }

    void EndDrag(PointerEventData eventData)
    {
        Direction = _handleRect.localPosition = Vector3.zero;
    }
}
