using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    PlayerInfo _myPlayer;
    Dictionary<ushort, PlayerInfo> _players = new Dictionary<ushort, PlayerInfo>();

    public Define.State GetPlayerState(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.State.Unknown;

        return bc.State;
    }

    #region Network
    // public void Add(S_PlayerList packet) { }

    // public void Move(S_BroadcaseMove packet) { }

    // public void EnterGame(S_BroadcastEnterGame packet) { }

    // public void LeaveGame(S_BroadcastLeaveGame packet) { }
    #endregion
}
