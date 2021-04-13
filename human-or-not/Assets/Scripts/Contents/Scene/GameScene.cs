using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    protected override void OnAwake()
    {
        base.OnAwake();

        Manager.Game.Init();

        GameObject player = Manager.Game.SpawnPlayer(Define.Character.Dongdong);
        GameObject spawningPool = Manager.Game.SpawnBots(Define.Character.Dongdong);

        // Camera Settings
        if (GameObject.Find("CameraPivot") == null)
            Manager.Resource.Instaniate("Camera/CameraPivot");
        Camera.main.GetComponent<CameraController>().Target = player;
    }

    public override void Clear()
    {
        Debug.Log("GameScene Clear");
    }
}
