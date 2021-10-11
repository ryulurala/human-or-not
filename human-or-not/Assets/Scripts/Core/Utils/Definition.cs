using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Definition
{
    public enum MouseEvent
    {
        LeftClick,
        RightDown,
        RightPressed,
        ScrollWheel,
    }

    public enum KeyEvent
    {
        None,
        WASD,
        ShiftWASD,
        SpaceBar,
    }

    public enum PadEvent
    {
        OnIdle,
        OnWalk,
        OnRun,
        OnAttack,
        OnJump,
        BeginRotate,
        OnRotate,
        OnZoom
    }

    public enum UIEvent
    {
        Click,
        PointerDown,
        PointerUp,
        OnDrag,
    }

    public enum State
    {
        Unknown,
        Died,
        Attack,
        Jump,
        Idle,
        Walking,
        Running,
    }

    public enum Scene
    {
        UnKnown,
        Start,
        SSU,
        HUFS,
    }

    public enum Character
    {
        Dongdong,
    }

    public enum Map
    {
        Start,
        SSU,
        HUFS,
    }
}
