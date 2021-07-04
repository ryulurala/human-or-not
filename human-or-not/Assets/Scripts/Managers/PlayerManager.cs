using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    Dictionary<string, PlayerInfo> _players = new Dictionary<string, PlayerInfo>();
    public PlayerInfo MyPlayer { get; set; }

    public Define.State GetPlayerState(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.State.Unknown;

        return bc.State;
    }

    #region Network

    public void Add(PlayerInfo info, bool myPlayer = false)
    {
        if (myPlayer)
            MyPlayer = info;    // 본인

        _players.Add(info.PlayerId, info);
    }

    public void Remove(string id)
    {
        _players.Remove(id);
    }

    public void RemoveMyPlayer()
    {
        if (MyPlayer == null)
            return;

        Remove(MyPlayer.PlayerId);
        MyPlayer = null;
    }

    // public void Move(S_BroadcaseMove packet) { }

    // public void EnterGame(S_BroadcastEnterGame packet) { }

    // public void LeaveGame(S_BroadcastLeaveGame packet) { }
    #endregion
}
