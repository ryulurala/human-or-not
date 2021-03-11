using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    #region MouserEvent
    public const float MousePressedTime = 0.5f;
    public enum MouseEvent
    {
        Down,
        Press,
        Up,
        Click,
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
}
