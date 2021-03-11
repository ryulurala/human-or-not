using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InputManager manager = Manager.Input;
        if (manager == null)
            Debug.Log("null");
        else
            Debug.Log("not null");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
