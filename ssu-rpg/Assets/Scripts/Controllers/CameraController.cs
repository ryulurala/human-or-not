using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 _delta = new Vector3(0, 5f, -10f);  // 거리 차
    [SerializeField] GameObject _player = null;

    public GameObject Player { get { return _player; } set { _player = value; } }

    void Start()
    {

    }

    void LateUpdate()
    {
        if (!_player.IsValid())
            return;

        transform.position = _player.transform.position + _delta;
        transform.LookAt(_player.transform);
    }
}
