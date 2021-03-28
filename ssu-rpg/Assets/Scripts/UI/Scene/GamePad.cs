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
    public static GamePad Pad { get { return _pad; } set { _pad = value; } }

    bool _joystick = false;
    bool _buttonA = false;
    bool _buttonJ = false;
    bool _buttonR = false;

    public enum PadCode
    {
        Joystick,
        ButtonA,
        ButtonJ,
        ButtonR,
    }

    public bool GetPad(PadCode padCode)
    {
        switch (padCode)
        {
            case PadCode.Joystick:
                return GamePad.Pad._joystick;
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
        // Debug.Log("Drag 시작!");
        Dragging(eventData);
        _joystick = true;
    }

    void Dragging(PointerEventData eventData)
    {
        // Debug.Log($"Drag 중!: {_joystick}");
        Vector2 dir = Vector2.ClampMagnitude(eventData.position - (Vector2)_backgroundRect.position, _backgroundRadius);

        _handleRect.localPosition = dir;
        Direction = dir.normalized * Vector2.Distance(_backgroundRect.position, _handleRect.position) / _backgroundRadius;
        // Direction = dir.normalized;
    }

    void EndDrag(PointerEventData eventData)
    {
        // Debug.Log("Drag 끝!");
        Direction = _handleRect.localPosition = Vector3.zero;
        _joystick = false;
    }
}
