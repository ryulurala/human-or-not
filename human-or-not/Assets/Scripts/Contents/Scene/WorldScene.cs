using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScene : BaseScene
{

    protected override void OnAwake()
    {
        base.OnAwake();

        Camera.main.GetComponent<CameraController>().Target = GameObject.FindGameObjectWithTag("Player");

        if (Util.IsMobile)
            GamePad.Pad.gameObject.SetActive(true);
    }

    public override void Clear()
    {
        // Debug.Log("WorldScene Clear");
    }
}
