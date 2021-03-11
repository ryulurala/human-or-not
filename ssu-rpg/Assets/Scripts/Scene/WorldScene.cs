using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScene : BaseScene
{

    protected override void OnAwake()
    {
        Camera.main.GetComponent<CameraController>().Player = GameObject.FindGameObjectWithTag("Player");
    }

    public override void Clear()
    {
        Debug.Log("WorldScene Clear");
    }

}
