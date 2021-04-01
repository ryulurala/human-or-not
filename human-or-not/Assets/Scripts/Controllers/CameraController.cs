using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject _target = null;
    readonly Vector3 _delta = new Vector3(0f, 7.5f, -5f);
    Vector3 _playerBellyPos;
    Vector3 _prevPos;
    float _pivotAngleX;
    float _pivotAngleY;
    float _zoomRatio;
    const float _rotateSpeed = 1f;
    const float _zoomSpeed = 0.1f;

    public float MaxZoomRatio { get; } = 3.0f;
    public float MinZoomRatio { get; } = 1.0f;
    public Transform Pivot { get; private set; } = null;
    public GameObject Target { get { return _target; } set { _target = value; } }

    void Start()
    {
        Pivot = transform.parent;

        Manager.Input.MouseAction -= OnMouseEvent;
        Manager.Input.MouseAction += OnMouseEvent;

        Manager.Input.PadAction -= OnPadEvent;
        Manager.Input.PadAction += OnPadEvent;

        _zoomRatio = (MinZoomRatio + MaxZoomRatio) / 2;
        // Scene이 Awake에서 Player 미리 설정
        _playerBellyPos = new Vector3(0, 0, _target.GetComponent<CharacterController>().height / 2);
    }

    void LateUpdate()
    {
        if (!_target.IsValid())
            return;

        Pivot.position = _target.transform.position;
        transform.localPosition = _delta * _zoomRatio;
        transform.LookAt(Pivot.transform.position + _playerBellyPos);
    }

    #region Mobile
    void OnPadEvent(Define.PadEvent padEvent, Vector3 point)
    {
        switch (padEvent)
        {
            case Define.PadEvent.BeginRotate:
                StartRotate(point);
                break;
            case Define.PadEvent.OnRotate:
                Rotate(point);
                break;
            case Define.PadEvent.OnZoom:
                Zoom(point.x);
                break;
        }
    }
    #endregion

    #region PC
    void OnMouseEvent(Define.MouseEvent mouseEvent)
    {
        switch (mouseEvent)
        {
            case Define.MouseEvent.RightDown:
                StartRotate(Input.mousePosition);
                break;
            case Define.MouseEvent.RightPressed:
                Rotate(Input.mousePosition);
                break;
            case Define.MouseEvent.ScrollWheel:
                float delta = Mathf.Clamp(_zoomRatio + Input.mouseScrollDelta.y * _zoomSpeed, 1f, 3f);
                Zoom(delta);
                break;
        }
    }
    #endregion

    void StartRotate(Vector3 point)
    {
        _prevPos = Camera.main.ScreenToViewportPoint(point);

        _pivotAngleX = Pivot.eulerAngles.x >= 310 ? Pivot.eulerAngles.x - 360 : Pivot.eulerAngles.x;
        _pivotAngleY = Pivot.eulerAngles.y;
    }

    void Rotate(Vector3 point)
    {
        Vector3 currPos = Camera.main.ScreenToViewportPoint(point) - _prevPos;

        float xAngle = Mathf.Clamp((_pivotAngleX - currPos.y * 90) * _rotateSpeed, -50, 30);
        float yAngle = (_pivotAngleY + currPos.x * 180) * _rotateSpeed;

        Pivot.rotation = Quaternion.Euler(xAngle, yAngle, 0f);
    }

    void Zoom(float delta)
    {
        _zoomRatio = delta;
    }
}