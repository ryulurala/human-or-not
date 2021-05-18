using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    ushort _myPlayerId;
    HashSet<ushort> _playerIds = new HashSet<ushort>();

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
    public void Add(S_PlayerList packet)
    {
        ushort[] playerIds = packet.players;
        int idx = 0;
        while (idx < playerIds.Length - 1)
        {
            _players.Add(playerIds[idx], null);
            idx++;
        }
        _myPlayerId = playerIds[idx];
    }

    // public void Move(S_BroadcaseMove packet) { }

    // public void EnterGame(S_BroadcastEnterGame packet) { }

    // public void LeaveGame(S_BroadcastLeaveGame packet) { }
    #endregion
}
