using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketHandler
{
    public void S_CreateRoom(Session session, Packet packet)
    {
        S_CreateRoom body = packet as S_CreateRoom;

        // 본인 Player 등록
        Manager.Player.Add(new PlayerInfo() { PlayerId = body.user.id, PlayerName = body.user.name }, true);

        // RoomID
        Manager.Game.RoomId = body.roomId;

        // UI
        Manager.UI.CloseAllPopupUI();
        Manager.UI.ShowPopupUI<OnlineSettingsView>();
    }

    public void S_EnterRoom(Session session, Packet packet)
    {
        S_EnterRoom body = packet as S_EnterRoom;

        // 본인 Player 등록
        Manager.Player.Add(new PlayerInfo() { PlayerId = body.user.name, PlayerName = body.user.id }, true);

        // RoomID
        Manager.Game.RoomId = body.roomId;

        // UI
        Manager.UI.CloseAllPopupUI();
        Manager.UI.ShowPopupUI<OnlineSettingsView>();
    }

    public void S_Spawn(Session session, Packet packet)
    {
        // TODO: 다른 Player 등록

        // ushort[] playerIds = packet.players;
        // int idx = 0;
        // while (idx < playerIds.Length - 1)
        // {
        //     _players.Add(playerIds[idx], null);
        //     idx++;
        // }
        // _myPlayerId = playerIds[idx];

    }
}
