using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketHandler
{
    public void S_BroadcastEnterRoom(Session session, Packet packet)
    {

    }

    public void S_BroadcastLeaveRoom(Session session, Packet packet)
    {

    }

    public void S_PlayerListHandler(Session session, Packet packet)
    {
        S_PlayerList body = packet as S_PlayerList;

        // Player 추가
        Manager.Player.Add(body);

        // UI
        Manager.UI.CloseAllPopupUI();
        if (body.players.Length == 1)
            Manager.UI.ShowPopupUI<HostSettingsView>();     // I'm host
        else
            Manager.UI.ShowPopupUI<ClientSettingsView>();   // I'm client
    }
}
