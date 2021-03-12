using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    #region MouserEvent
    public const float MousePressedTime = 0.5f;
    public enum MouseEvent
    {
        LeftPress,
        LeftClick,
        RightClick,
        ScrollWheel,
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
