using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBombController : MonoBehaviour {

    public float TimeFactor = 0.3f;
    public GameObject SField;
    public GameObject FField;
    private float _speed = 5;
    private Vector3 _targetPosition;

    void Start () {
        _targetPosition = new Vector3(transform.position.x, 0, transform.position.z);
	}
	
	void Update () {

        if (transform.position.y < 0.1f)
        {
            GameObject f = null;
            if (TimeFactor > 1)
            {
                f = Instantiate(FField, transform.position, Quaternion.identity);
            }
            else
            {
                f = Instantiate(SField, transform.position, Quaternion.identity);
            }

            f.GetComponent<FieldController>().TimeFactor = TimeFactor;
            Destroy(gameObject);

        }
        else {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _speed * Time.deltaTime);
        }

	}
}
