using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {

    public GameObject Target;
    public GameObject Explode;
    public float Damage;
    private float _speed = 2.5f;
    private float _rotationSpeed = 2;
    private bool isReady = false;
    private float _actionRadius = 0.2f;

    void Start () {
		
	}
	
	void Update () {

        Move();
        Act();

	}

    private void Move()
    {
        if (Target != null) {

            var timeFactor = GameState.Current.CalculateTimeFactor(transform.position);

            var targetPosition = new Vector3(Target.transform.position.x, 0.65f, Target.transform.position.z);

            if (!isReady)
            {
                isReady = true;
                var newDir = Vector3.RotateTowards(transform.forward, targetPosition - transform.position, 50, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
            else {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime * timeFactor);
                var newDir = Vector3.RotateTowards(transform.forward, targetPosition - transform.position, _rotationSpeed * Time.deltaTime * timeFactor, 0.0f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
        }
    }

    private void Act()
    {

        if (isReady) {

            if (Target == null)
            {
                var exp = Instantiate(Explode, transform.position, Quaternion.identity);
                Destroy(exp, 2f);
                Destroy(gameObject);
            }
            else {

                if (Vector3.Distance(transform.position, new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z)) < _actionRadius) {

                    Target.GetComponent<ShipController>().ApplyDamage(Damage);
                    var exp = Instantiate(Explode, transform.position, Quaternion.identity);
                    Destroy(exp, 2f);
                    Camera.main.GetComponent<CameraController>().SmallShake();
                    Destroy(gameObject);
                }

            }

        }

    }

}
