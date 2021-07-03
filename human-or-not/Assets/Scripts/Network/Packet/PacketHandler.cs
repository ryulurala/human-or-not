using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketHandler
{

    public void S_CreateRoom(Session session, Packet packet)
    {
        S_CreateRoom body = packet as S_CreateRoom;

        // TODO: 본인 Player 등록

        // UI
        Manager.UI.CloseAllPopupUI();
        Manager.UI.ShowPopupUI<OnlineSettingsView>();
    }

    public void S_EnterRoom(Session session, Packet packet)
    {
        S_EnterRoom body = packet as S_EnterRoom;

        // TODO: 본인 Player 등록

        // UI
        Manager.UI.CloseAllPopupUI();
        Manager.UI.ShowPopupUI<OnlineSettingsView>();
    }

    public void S_Spawn(Session session, Packet packet)
    {
        // TODO: 다른 Player 등록

    }
}
