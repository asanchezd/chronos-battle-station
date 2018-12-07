using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

    public GameObject Pointer;

    private float _rotationSpeed = 400f;

	void Start () {
        Cursor.visible = false;
    }
	
	void Update () {
        var plane = new Plane(Vector3.up ,transform.position );
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            var hitPoint = ray.GetPoint(distance);
            transform.position = new Vector3(hitPoint.x, 2.85f, hitPoint.z);
        }
        Pointer.transform.Rotate(new Vector3(0, _rotationSpeed * Time.deltaTime, 0));
    }

}
