using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    #region MouserEvent
    public const float MousePressedTime = 0.5f;
    public const float MouseZoomSpeed = 0.01f;
    public enum MouseEvent
    {
        LeftStart,
        LeftPress,
        LeftClick,
        RightClick,
        ScrollWheel,
    }
    #endregion

    #region TouchEvent
    public const float TouchPressedTime = 0.5f;
    public const float TouchMaxDeltaPos = 100f;
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
