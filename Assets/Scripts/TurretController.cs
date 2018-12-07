using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurretController : MonoBehaviour {

    public float Damage = 0.2f;
    public float ShotInterval = 2f;
    public float Range = 5f;
    public GameObject Rocket;

    private GameObject _currentTarget = null;
    private float _rotationSpeed = 2.6f;
    private float _lastShotTime = 0;

	void Start () {
        _lastShotTime = Time.time;
    }
	
	void Update () {
        CheckTarget();
        if (_currentTarget != null) {
            var timeFactor = GameState.Current.CalculateTimeFactor(transform.position);
            AimToTarget(timeFactor);
            TryShot(timeFactor);
        }
        
	}

    void AimToTarget(float timeFactor) {
        var newDir = Vector3.RotateTowards(transform.forward, transform.position - new Vector3(_currentTarget.transform.position.x, transform.position.y ,  _currentTarget.transform.position.z), _rotationSpeed * Time.deltaTime * timeFactor, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    void TryShot(float timeFactor) {
        if (_lastShotTime + ShotInterval / timeFactor < Time.time)
        {
            _lastShotTime = Time.time;
            var newShoot = Instantiate(Rocket, new Vector3(transform.position.x, transform.position.y + 0.15f, transform.position.z), Quaternion.identity);
            newShoot.GetComponent<RocketController>().Target = _currentTarget;
            newShoot.GetComponent<RocketController>().Damage = Damage;
        }
    }

    void CheckTarget() {
        if (_currentTarget != null) {
            if (Vector3.Distance(transform.position, _currentTarget.transform.position) > Range) {
                _currentTarget = null;
            }
        }

        if (_currentTarget == null) {
            _currentTarget = GameState.Current.Ships.FirstOrDefault(x => x != null && Vector3.Distance(transform.position, x.transform.position) < Range);
        }
    }
}
