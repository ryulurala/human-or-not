using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartScene : BaseScene
{
    protected override void OnAwake()
    {
        base.OnAwake();

        StartCoroutine(threeSeconds());
    }

    public override void Clear()
    {
        // Debug.Log("StartScene Clear");
    }

    IEnumerator threeSeconds()
    {
        int seconds = 3;
        while (seconds > 0)
        {
            yield return new WaitForSeconds(1.0f);
            seconds--;
            Text waitText = GameObject.FindObjectOfType(typeof(Text)) as Text;
            waitText.text = $"Wait for {seconds} seconds...";
        }
        // 3초 후
        Manager.Scene.LoadScene(Define.Scene.Game);
    }
}
