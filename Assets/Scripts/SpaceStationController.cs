using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStationController : MonoBehaviour
{

    public GameObject TimeBomb;

    public Material PointerActiveM;
    public Material PointerInactiveM;

    public Material PointerCenterActiveM;
    public Material PointerCenterInactiveM;

    public GameObject Target;
    public GameObject Pointer;
    public GameObject PointerCenter;


    private float _shotInterval = 3;
    private float _lastShotTime = 0;

    private bool _isActive = true;

    void Start()
    {
        _lastShotTime = -10;
    }

    private void ToggleActivation(bool active)
    {
        Pointer.GetComponent<Renderer>().material = active ? PointerActiveM : PointerInactiveM;
        PointerCenter.GetComponent<Renderer>().material = active ? PointerCenterActiveM : PointerCenterInactiveM;
    }

    void Update()
    {

        CheckActive();

        if (Input.GetMouseButtonDown(0))
        {
            TryShoot(true);
        }
        if (Input.GetMouseButtonDown(1))
        {
            TryShoot(false);
        }
    }

    private void CheckActive()
    {

        if (_lastShotTime + _shotInterval < Time.time)
        {
            if (!_isActive) {
                _isActive = true;
                ToggleActivation(true);
            }
        }

    }

    private void TryShoot(bool s)
    {

        if (_isActive)
        {
            _isActive = false;
            ToggleActivation(false);
            _lastShotTime = Time.time;
            var tb = Instantiate(TimeBomb, new Vector3(Target.transform.position.x, 8, Target.transform.position.z), Quaternion.identity);
            tb.GetComponent<TimeBombController>().TimeFactor = (s) ? 0.25f : 4f;
        }

    }

}
