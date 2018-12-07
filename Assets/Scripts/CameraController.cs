using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject _targetShip = null;
    private float _cameraSpeed = 2.1f;
    private bool _isStarted = false;
    private bool _isShaking = false;
    private float _shakeDuration = 0.3f;
    private float _shakeTime = 0;
    private float _shakeSize = 0.12f;

    private Vector3 _targetPoint;

    void Start () {
        _targetPoint = transform.position;
    }

    public void Shake() {
        _shakeDuration = 0.32f;
        _shakeSize = 0.12f;
        _shakeTime = Time.time;
        _isShaking = true;
    }

    public void SmallShake()
    {
        if (!_isShaking) {
            _shakeDuration = 0.14f;
            _shakeSize = 0.05f;
            _shakeTime = Time.time;
            _isShaking = true;
        }
    }

    void Update () {

        FindTarget();

        if (_targetShip != null) {
            if (!_isStarted)
            {
                _targetPoint = new Vector3(_targetShip.transform.position.x, 10, _targetShip.transform.position.z - 8);
                _isStarted = true;
            }
            else {
                _targetPoint = Vector3.MoveTowards(transform.position, new Vector3(_targetShip.transform.position.x, 6, _targetShip.transform.position.z - 6), _cameraSpeed * Time.deltaTime);
            }
        }

        if (_isShaking && _shakeTime + _shakeDuration < Time.time) {
            _isShaking = false;
        }

        if (_isShaking) {
            _targetPoint = _targetPoint + Random.onUnitSphere * _shakeSize;
        }

        transform.position = _targetPoint;

    }

    private void FindTarget() {
        if(_targetShip == null){
            _targetShip = GameState.Current.Ships.FirstOrDefault( x => x != null);
        }
    }

}
