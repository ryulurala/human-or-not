using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 _delta = new Vector3(0f, 15f, -10f);
    [SerializeField] GameObject _target = null;
    [SerializeField] float _ratio = 1f;
    float _rotateSpeed = 1f;
    float _zoomSpeed = 0.1f;
    Vector3 _prevPos;
    float _pivotAngleX = 0f;
    float _pivotAngleY = 0f;

    public static Transform Pivot { get; private set; } = null;
    public GameObject Target { get { return _target; } set { _target = value; } }

    void Start()
    {
        Pivot = transform.parent;

        Manager.Input.MouseAction -= OnMouseEvent;
        Manager.Input.MouseAction += OnMouseEvent;

        Manager.Input.PadAction -= OnPadEvent;
        Manager.Input.PadAction += OnPadEvent;
    }

    void LateUpdate()
    {
        if (!_target.IsValid())
            return;

        Pivot.position = _target.transform.position;
        transform.localPosition = _delta * _ratio;
        transform.LookAt(Pivot.transform.position);
    }

    #region Mobile
    void OnPadEvent(Define.PadEvent padEvent, Vector3 point)
    {
        switch (padEvent)
        {
            case Define.PadEvent.StartRotate:
                StartRotate(point);
                break;
            case Define.PadEvent.Rotating:
                Rotate(point);
                break;
        }
    }
    #endregion

    #region PC
    void OnMouseEvent(Define.MouseEvent mouseEvent)
    {
        switch (mouseEvent)
        {
            case Define.MouseEvent.RightStart:
                StartRotate(Input.mousePosition);
                break;
            case Define.MouseEvent.RightPress:
                Rotate(Input.mousePosition);
                break;
            case Define.MouseEvent.ScrollWheel:
                Zoom(Input.mouseScrollDelta.y * _zoomSpeed);
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
        _ratio += delta;
        _ratio = Mathf.Clamp(_ratio, 0.5f, 3f);
    }

}