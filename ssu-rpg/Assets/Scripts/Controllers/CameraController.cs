using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 _delta = new Vector3(0f, 15f, -10f);
    [SerializeField] GameObject _target = null;
    [SerializeField] float _ratio = 1f;
    Transform _pivot = null;
    float _rotateSpeed = 1f;
    Vector3 _prevPos;
    float _pivotAngleX = 0f;
    float _pivotAngleY = 0f;

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
    float _prevDelta;

    void OnTouchEvent(Define.TouchEvent touchEvent)
    {
        switch (touchEvent)
        {
            case Define.TouchEvent.TabWithOneStart:
                StartRotate(Input.GetTouch(0).position);
                break;
            case Define.TouchEvent.PressWithOne:
                Rotate(Input.GetTouch(0).position);
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
                StartRotate(Input.mousePosition);
                break;
            case Define.MouseEvent.LeftPress:
                Rotate(Input.mousePosition);
                break;
            case Define.MouseEvent.ScrollWheel:
                Zoom(Input.mouseScrollDelta.y * Define.MouseZoomSpeed);
                break;
        }
    }
    #endregion

    void StartRotate(Vector3 point)
    {
        _prevPos = Camera.main.ScreenToViewportPoint(point);

        _pivotAngleX = _pivot.eulerAngles.x >= 320 ? _pivot.eulerAngles.x - 360 : _pivot.eulerAngles.x;
        _pivotAngleY = _pivot.eulerAngles.y;
    }

    void Rotate(Vector3 point)
    {
        Vector3 distPos = Camera.main.ScreenToViewportPoint(point) - _prevPos;

        float xAngle = Mathf.Clamp(_pivotAngleX - distPos.y * 90 * _rotateSpeed, -40, 50);
        float yAngle = _pivotAngleY + distPos.x * 180 * _rotateSpeed;

        _pivot.rotation = Quaternion.Euler(xAngle, yAngle, 0f);
    }

    void Zoom(float delta)
    {
        _ratio += delta;
        _ratio = Mathf.Clamp(_ratio, 0.5f, 3f);
    }

}
