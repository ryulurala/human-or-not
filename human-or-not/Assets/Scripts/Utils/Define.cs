using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    #region PC
    public enum MouseEvent
    {
        LeftClick,
        RightStart,
        RightPress,
        ScrollWheel,
    }

    public enum KeyEvent
    {
        WASD,
        ShiftWASD,
        SpaceBar,
    }
    #endregion

    #region Mobile

    public enum PadEvent
    {
        Dragging,
        RunButton,
        AttackButton,
        JumpButton,
        StartRotate,
        Rotating,
    }
    #endregion

    #region UIEvent
    public enum UIEvent
    {
        Click,
        PointerDown,
        StartDrag,
        Dragging,
        EndDrag,
    }
    #endregion

    #region State
    public enum State
    {
        Die,
        Idle,
        Walking,
        Running,
        Attack,
    }
    #endregion

    #region WorldObjectType
    public enum WorldObject
    {
        Unknown,
        Player,
        NonPlayer,
    }
    #endregion

    #region  Scene
    public enum Scene
    {
        UnKnown,
        Start,
        World,
    }
    #endregion
}
