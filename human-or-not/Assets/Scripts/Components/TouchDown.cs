using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDown : MonoBehaviour
{
    PlayerInfo _playerInfo;

    void OnTriggerEnter(Collider other)
    {
        if (Manager.Game.GetWorldObjectType(other.gameObject) == Define.WorldObject.Player)
        {
            // TODO: 해당 캐릭터 저장( ushort id로? )
            PlayerInfo playerInfo = other.GetComponent<PlayerInfo>();
            if (_playerInfo == null || _playerInfo != playerInfo)
            {
                Debug.Log($"TouchDown!");

                _playerInfo = playerInfo;
            }
        }
    }
}
