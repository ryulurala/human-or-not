using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    protected override void OnAwake()
    {
        base.OnAwake();

        if (Util.IsMobile)
            GamePad.Pad.gameObject.SetActive(true);

        Camera.main.GetComponent<CameraController>().Target = Manager.Game.Spawn(Define.WorldObject.Player, "Character/Dongdong/Player");

        Manager.Game.SpawnNonPlayer(10);
    }

    public override void Clear()
    {
        // Debug.Log("WorldScene Clear");
    }
}
