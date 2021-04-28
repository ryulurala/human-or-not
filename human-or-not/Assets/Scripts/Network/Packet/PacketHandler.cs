using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketHandler
{
    public void S_PlayerOrderHandler(Session session, Packet packet)
    {
        S_PlayerOrder body = packet as S_PlayerOrder;

        Debug.Log($"S_ConnectedHandler: {body.playerId}, {body.order}");

        Manager.UI.CloseAllPopupUI();
        if (body.order == 0)
            Manager.UI.ShowPopupUI<HostSettingsView>();     // I'm host
        else
            Manager.UI.ShowPopupUI<ClientSettingsView>();   // I'm client
    }
}
