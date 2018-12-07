using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour {

    private Vector3? _targetPosition = null;
    private float _delta = 0.1f;
    private float _baseSpeed = 0.6f;
    private float _baseRotationSpeed = 4f;
    private int _targetIndex = -1;
    public GameObject Sparkle = null;
    private float _health = 1;
    private bool _destroyed = false;

    void Start () {
        FindNextTarget();
	}
	
	void Update () {
        if (_targetPosition != null) {

            var timeFactor = GameState.Current.CalculateTimeFactor(transform.position);

            transform.position = Vector3.MoveTowards(transform.position, new Vector3(_targetPosition.Value.x, _targetPosition.Value.y, _targetPosition.Value.z), _baseSpeed * Time.deltaTime * timeFactor);
            if (Vector3.Distance(transform.position, _targetPosition.Value) < _delta) {
                FindNextTarget();
            }
            var newDir = Vector3.RotateTowards(transform.forward, _targetPosition.Value - transform.position, _baseRotationSpeed * Time.deltaTime,0.0f * timeFactor);
            transform.rotation = Quaternion.LookRotation(newDir);

        }
        if (transform.position.z > -0.2f) {
            Complete();
        }
	}

    private void FindNextTarget() {

        _targetIndex += 1;
        if (GameState.Current.PathPoints.Count > _targetIndex) {
            _targetPosition = new Vector3(GameState.Current.PathPoints[_targetIndex].transform.position.x + 0.5f , transform.position.y , GameState.Current.PathPoints[_targetIndex].transform.position.z + 0.5f);
        } 

    }

    public void Complete() {
        GameState.Current.MissionCompletedShips++;
        GameState.Current.Ships.Remove(gameObject);
        GameState.Current.CalculateGameStatus();
        var newSparkle = Instantiate(Sparkle, new Vector3(transform.position.x, 0.8f, transform.position.z - 0.2f), Quaternion.identity);
        DestroyObject(newSparkle, 2.5f);
        Destroy(gameObject);
    }

    public void ApplyDamage(float damage) {

        if (!_destroyed) {
            _health -= damage;
            if (_health <= 0)
            {
                _destroyed = true;
                GameState.Current.MissionDestroyedShips++;
                GameState.Current.Ships.Remove(gameObject);
                GameState.Current.CalculateGameStatus();
                Camera.main.GetComponent<CameraController>().Shake();
                Destroy(gameObject);
            }
        }
        

    }

}
