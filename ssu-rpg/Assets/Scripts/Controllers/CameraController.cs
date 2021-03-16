using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 _delta = new Vector3(0f, 5f, -10f);
    [SerializeField] float _ratio = 1f;
    [SerializeField] GameObject _target = null;
    [SerializeField] Transform _pivot = null;
    [SerializeField] float _zoomSpeed = 0.5f;
    [SerializeField] float _rotateSpeed = 2f;
    Vector3 _startPos;
    float _xAngleStart;
    float _yAngleStart;

    public GameObject Target { get { return _target; } set { _target = value; } }

    void Start()
    {
        _pivot = transform.parent;

        Manager.Input.MouseAction += OnMouseEvent;
        Manager.Input.TouchAction += OnTouchEvent;
    }

    void LateUpdate()
    {
        if (!_target.IsValid())
            return;

        _pivot.position = _target.transform.position;
        transform.localPosition = _delta * _ratio;
        transform.LookAt(_pivot.transform.position);
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

        _xAngleStart = _pivot.eulerAngles.x >= 340 ? _pivot.eulerAngles.x - 360 : _pivot.eulerAngles.x;
        _yAngleStart = _pivot.eulerAngles.y;
    }

    void Rotate()
    {
        Vector3 distPos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - _startPos;

        float xAngle = Mathf.Clamp(_xAngleStart - distPos.y * 90 * _rotateSpeed, -20, 50);
        float yAngle = _yAngleStart + (distPos).x * 180 * _rotateSpeed;

        _pivot.rotation = Quaternion.Euler(xAngle, yAngle, 0f);
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
        _ratio = Mathf.Clamp(_ratio, 0.5f, 5f);
    }
    #endregion
}
