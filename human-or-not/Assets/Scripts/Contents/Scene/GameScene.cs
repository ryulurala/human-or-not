using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    protected override void OnAwake()
    {
        base.OnAwake();

        Manager.Game.Init();

        if (GameObject.Find("CameraPivot") == null)
            Manager.Resource.Instaniate("Camera/CameraPivot");

        Camera.main.GetComponent<CameraController>().Target = Manager.Game.Spawn(Define.WorldObject.Player, "Character/Dongdong/Player");

        Manager.Game.SpawnBots();
    }

    public override void Clear()
    {
        Debug.Log("GameScene Clear");
    }
}
