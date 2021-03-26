using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    #region MouseEvent
    public const float MouseZoomSpeed = 0.01f;
    public enum MouseEvent
    {
        LeftClick,
        RightStart,
        RightPress,
        ScrollWheel,
    }
    #endregion

    #region KeyEvent
    public enum KeyEvent
    {
        WASD,
        ShiftWASD,
        SpaceBar,
    }
    #endregion

    #region TouchEvent
    public enum TouchEvent
    {
        TabWithOneStart,
        PressWithOne,
        TabWithOne,
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

    #region UIEvent
    public enum UIEvent
    {
        Click,
        Drag,
    }
    #endregion
}
