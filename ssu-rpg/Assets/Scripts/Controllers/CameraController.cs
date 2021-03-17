using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 _delta = new Vector3(0f, 5f, -10f);
    [SerializeField] float _ratio = 1f;
    [SerializeField] float _rotateSpeed = 1f;
    [SerializeField] GameObject _target = null;
    [SerializeField] Transform _pivot = null;
    Vector3 _prevMousePos;
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
            case Define.TouchEvent.TabWithOne:
                // 다른 Player 옵션
                break;
            case Define.TouchEvent.PressWithTwo:
                float delta = GetDelta() * Define.TouchZoomSpeed;
                Zoom(delta);
                break;
        }
    }

    float GetDelta()
    {
        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        Vector2 touchZeroPrev = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrev = touchOne.position - touchOne.deltaPosition;

        float prevTouchDelta = (touchZeroPrev - touchOnePrev).magnitude;
        float touchDelta = (touchZero.position - touchOne.position).magnitude;

        return touchDelta - prevTouchDelta;
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
            case Define.MouseEvent.LeftClick:
                // 다른 Player 옵션
                break;
            case Define.MouseEvent.ScrollWheel:
                Zoom(Input.mouseScrollDelta.y * Define.MouseZoomSpeed);
                break;
        }
    }
    #endregion

    void StartRotate(Vector2 point)
    {
        _prevMousePos = Camera.main.ScreenToViewportPoint(point);

        _pivotAngleX = _pivot.eulerAngles.x >= 340 ? _pivot.eulerAngles.x - 360 : _pivot.eulerAngles.x;
        _pivotAngleY = _pivot.eulerAngles.y;
    }

    void Rotate(Vector2 point)
    {
        Vector2 distPos = Camera.main.ScreenToViewportPoint(point) - _prevMousePos;

        float xAngle = Mathf.Clamp(_pivotAngleX - distPos.y * 90 * _rotateSpeed, -20, 50);
        float yAngle = _pivotAngleY + distPos.x * 180 * _rotateSpeed;

        _pivot.rotation = Quaternion.Euler(xAngle, yAngle, 0f);
    }

    void Zoom(float delta)
    {
        _ratio += delta;
        _ratio = Mathf.Clamp(_ratio, 0.5f, 5f);
    }

}
