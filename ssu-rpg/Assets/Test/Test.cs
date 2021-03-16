using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] GameObject _target;
    void Start()
    {
    }

    void Update()
    {
        transform.position = _target.transform.position;
        // transform.RotateAround(transform.position, Vector3.up, 2f);
    }
}
