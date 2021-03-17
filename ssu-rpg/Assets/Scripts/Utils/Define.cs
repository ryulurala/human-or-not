using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    #region MouserEvent
    public const float MousePressedTime = 0.5f;
    public const float MouseZoomSpeed = 0.1f;
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
    public const float TouchMaxDeltaPos = 10f;
    public const float TouchZoomSpeed = 0.01f;
    public enum TouchEvent
    {
        TabWithOneStart,
        TabWithOne,
        PressWithOne,
        PressWithTwo,
    }
    #endregion

    #region State
    public enum State
    {
        Die,
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
        Monster,
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
