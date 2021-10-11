using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void OnAwake()
    {
        base.OnAwake();

        Manager.Game.Init();

        GameObject player = Manager.Game.SpawnPlayer(Definition.Character.Dongdong);
        // GameObject spawningPool = Manager.Game.SpawnBots(Definition.Character.Dongdong);
        // GameObject touchDowns = Manager.Game.SpawnTouchDown(count: 5);

        // Camera Settings
        Manager.Game.InitGameCamera(target: player);
    }

    public override void Clear()
    {
        Debug.Log("GameScene Clear !");
    }
}
