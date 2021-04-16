using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(coru(1));
        StartCoroutine(coru(2));
    }

    IEnumerator coru(int n)
    {
        yield return new WaitForSeconds(3.0f);
        // 3초 후

        Debug.Log($"한 번! {n}");
    }
}
