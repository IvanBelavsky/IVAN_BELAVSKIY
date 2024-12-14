using System;
using System.Collections;
using System.Collections.Generic;
using SplineMesh;
using UnityEngine;
using UnityEngine.Serialization;

public class SplineFollower : MonoBehaviour
{
    [SerializeField] private Spline _spline;
    [SerializeField] private float _speed;
    [SerializeField] private float _sensitivity;

    private float _currentSplinePosition = 0f;
    private float _input = 0f;
    private float _lastPositionMouse;

    private void Start()
    {
       Disable();
       Place();
    }

    private void Update()
    {
        _input += (_lastPositionMouse - Input.mousePosition.x) * _sensitivity;
        _lastPositionMouse = Input.mousePosition.x;
        _input = Mathf.Clamp(_input, -1, 1);
        
        _currentSplinePosition += _speed * Time.deltaTime;

        if (_currentSplinePosition <= _spline.nodes.Count - 1)
            Place();
    }

    public void Eneble()
    {
        enabled = true;
        _lastPositionMouse = Input.mousePosition.x;
    }

    public void Disable()
    {
        enabled = false;
    }

    private void Place()
    {
        CurveSample sample = _spline.GetSample(_currentSplinePosition);

        transform.localPosition = sample.location + Vector3.right * _input;
        transform.localRotation = sample.Rotation;
    }
}