using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 _delta = new Vector3(0, 5f, -10f);  // 거리 차
    [SerializeField] GameObject _target = null;
    // [SerializeField] float _zoomSpeed = 0.5f;
    // [SerializeField] float _rotateSpeed = 2f;

    public GameObject Target { get { return _target; } set { _target = value; } }

    void Start()
    {
        Manager.Input.MouseAction -= OnMouseEvent;  // Pooling으로 인해 두 번 등록 방지
        Manager.Input.MouseAction += OnMouseEvent;
    }

    void LateUpdate()
    {
        if (!_target.IsValid())
            return;

        transform.position = _target.transform.position + _delta;
        transform.LookAt(_target.transform);
    }

    void OnMouseEvent(Define.MouseEvent mouseEvent)
    {
        switch (mouseEvent)
        {
            case Define.MouseEvent.LeftPress:
                Rotate();
                break;
            case Define.MouseEvent.LeftClick:
                // 다른 Player 옵션
                break;
            case Define.MouseEvent.ScrollWheel:
                Zoom();
                break;
        }
    }

    void Zoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            // 축소
            Debug.Log("축소");
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            // 확대
            Debug.Log("확대");
        }
    }

    void Rotate()
    {

    }
}
