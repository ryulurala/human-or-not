using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    #region PC
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
    #endregion

    #region Mobile

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
    #endregion

    #region UIEvent
    public enum UIEvent
    {
        Click,
        PointerDown,
        PointerUp,
        OnDrag,
    }
    #endregion

    #region State
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
    #endregion

    #region WorldObjectType
    public enum WorldObject
    {
        Unknown,
        Player,
        Bot,
    }
    #endregion

    #region  Scene
    public enum Scene
    {
        UnKnown,
        Start,
        Game,
    }
    #endregion
}
