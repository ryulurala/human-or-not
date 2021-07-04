using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketHandler
{
    public static void S_CreateRoom(Session session, Packet packet)
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

    public static void S_EnterRoom(Session session, Packet packet)
    {
        S_EnterRoom body = packet as S_EnterRoom;

        // 본인 Player 등록
        Manager.Player.Add(new PlayerInfo() { PlayerId = body.user.id, PlayerName = body.user.name }, true);

        // RoomID
        Manager.Game.RoomId = body.roomId;

        // UI
        Manager.UI.CloseAllPopupUI();
        Manager.UI.ShowPopupUI<OnlineSettingsView>();
    }

    public static void S_UserList(Session session, Packet packet)
    {
        S_UserList body = packet as S_UserList;

        // 다른 Player 등록
        foreach (UserInfo user in body.users)
        {
            Manager.Player.Add(new PlayerInfo() { PlayerId = user.id, PlayerName = user.name });
        }

        // Update UI
        OnlineSettingsView view = Manager.UI.CurrentPopupUI as OnlineSettingsView;
        view.UpdateUser(Manager.Player.GetOtherPlayerNames());


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
