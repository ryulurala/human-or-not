using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    Transform _pivot;
    readonly Vector3 _delta = new Vector3(0f, 7.5f, -5f);
    [SerializeField] GameObject _target;

    public float MaxZoomRatio { get; } = 3.0f;
    public float MinZoomRatio { get; } = 1.0f;

    public GameObject Target { get => _target; set => _target = value; }

    void Awake()
    {
        // Create Pivot
        if (_pivot == null)
        {
            _pivot = new GameObject() { name = "Camera Pivot" }.transform;
            transform.parent = _pivot;
        }
    }

    void Start()
    {
        Manager.Input.MouseAction -= OnMouseEvent;
        Manager.Input.MouseAction += OnMouseEvent;

        Manager.Input.PadAction -= OnPadEvent;
        Manager.Input.PadAction += OnPadEvent;

        _zoomRatio = (MinZoomRatio + MaxZoomRatio) / 2;
    }

    void LateUpdate()
    {
        if (!_target.IsValid())
            return;

        _pivot.position = _target.transform.position;
        transform.localPosition = _delta * _zoomRatio;
        transform.LookAt(_pivot.transform.position);
    }

    #region Mobile
    void OnPadEvent(Definition.PadEvent padEvent, Vector3 point)
    {
        switch (padEvent)
        {
            case Definition.PadEvent.BeginRotate:
                StartRotate(point);
                break;
            case Definition.PadEvent.OnRotate:
                Rotate(point);
                break;
            case Definition.PadEvent.OnZoom:
                Zoom(point.x);
                break;
        }
    }
    #endregion

    #region PC
    void OnMouseEvent(Definition.MouseEvent mouseEvent)
    {
        switch (mouseEvent)
        {
            case Definition.MouseEvent.RightDown:
                StartRotate(Input.mousePosition);
                break;
            case Definition.MouseEvent.RightPressed:
                Rotate(Input.mousePosition);
                break;
            case Definition.MouseEvent.ScrollWheel:
                float delta = Mathf.Clamp(_zoomRatio + Input.mouseScrollDelta.y * _zoomSpeed, 1f, 3f);
                Zoom(delta);
                break;
        }
    }
    #endregion

    Vector3 _prevPos;
    float _pivotAngleX, _pivotAngleY;
    const float _rotateSpeed = 1f;

    void StartRotate(Vector3 point)
    {
        _prevPos = Camera.main.ScreenToViewportPoint(point);

        _pivotAngleX = _pivot.eulerAngles.x >= 310 ? _pivot.eulerAngles.x - 360 : _pivot.eulerAngles.x;
        _pivotAngleY = _pivot.eulerAngles.y;
    }

    void Rotate(Vector3 point)
    {
        Vector3 currPos = Camera.main.ScreenToViewportPoint(point) - _prevPos;

        float xAngle = Mathf.Clamp((_pivotAngleX - currPos.y * 90) * _rotateSpeed, -50, 30);
        float yAngle = (_pivotAngleY + currPos.x * 180) * _rotateSpeed;

        _pivot.rotation = Quaternion.Euler(xAngle, yAngle, 0f);
    }

    float _zoomRatio;
    const float _zoomSpeed = 0.1f;

    void Zoom(float delta)
    {
        _zoomRatio = delta;
    }
}