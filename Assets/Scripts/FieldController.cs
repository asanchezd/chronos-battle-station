using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour {

    public float TimeFactor = 0.3f;
    private float _duration = 15;
    private float _startTime = 0;
    private float _rotationSpeed = 60f;
    private bool _creating = true;
    private bool _destroying = false;
    private float _scaleSpeed = 5;
    private float _maxScale = 3;
    private float _minScale = 0.1f;
    private float _currentScale = 0.1f;
    void Start () {
        _startTime = Time.time;
        GameState.Current.Fields.Add(gameObject);
        transform.localScale = new Vector3(_minScale, _minScale, _minScale);

    }

    void Update () {

        Rotate();

        if (_creating) {
            if (_currentScale < _maxScale)
            {
                _currentScale += _scaleSpeed * Time.deltaTime;
                transform.localScale = new Vector3(_currentScale, _currentScale, _currentScale);
            }
            else {
                _creating = false;
            }
        }

        if (!_destroying && Time.time > _startTime + _duration) {
            _destroying = true;
            GameState.Current.Fields.Remove(gameObject);
        }


        if (_destroying) {
            if (_currentScale > _minScale)
            {
                _currentScale -= _scaleSpeed * Time.deltaTime;
                transform.localScale = new Vector3(_currentScale, _currentScale, _currentScale);
            }
            else
            {
                Destroy(gameObject);
            }
        }

	}

    private void Rotate() {

        transform.Rotate(new Vector3(0, _rotationSpeed * Time.deltaTime, 0));

    }
}
