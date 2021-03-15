using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 _delta = new Vector3(0, 5f, -10f);  // 거리 차
    [SerializeField] float _ratio = 1f;
    [SerializeField] GameObject _target = null;
    // [SerializeField] float _zoomSpeed = 0.5f;
    // [SerializeField] float _rotateSpeed = 2f;
    Vector3 _startPos;
    Vector3 _pressedPos;

    public GameObject Target { get { return _target; } set { _target = value; } }

    void Start()
    {
        Manager.Input.MouseAction += OnMouseEvent;
        Manager.Input.TouchAction += OnTouchEvent;
    }

    void LateUpdate()
    {
        if (!_target.IsValid())
            return;

        transform.position = _target.transform.position + _delta * _ratio;
        transform.LookAt(_target.transform.position);
    }

    #region Mobile
    void OnTouchEvent(Define.TouchEvent touchEvent)
    {
        switch (touchEvent)
        {
            case Define.TouchEvent.PressWithOne:
                Rotate();
                break;
            case Define.TouchEvent.TabWithOne:
                // 다른 Player 옵션
                break;
            case Define.TouchEvent.PressWithTwo:
                break;
        }
    }
    #endregion

    #region PC
    void OnMouseEvent(Define.MouseEvent mouseEvent)
    {
        switch (mouseEvent)
        {
            case Define.MouseEvent.LeftStart:
                RotateStart();
                break;
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

    void RotateStart()
    {
        _startPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }

    void Rotate()
    {
        _pressedPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
    }

    void Zoom()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            // 축소
            _ratio += Input.mouseScrollDelta.y;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            // 확대
            _ratio += Input.mouseScrollDelta.y;
        }
        _ratio = Mathf.Clamp(_ratio, 1f, 5f);
    }
    #endregion
}
