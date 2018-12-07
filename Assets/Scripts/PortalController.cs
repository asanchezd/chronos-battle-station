using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour {

    public bool IsStart = false;
    public int ShipsToBuild = 1;
    public GameObject Ship = null;
    public GameObject Sparkle = null;

    private float _lastShipBuiltTime = 0;
    private float _builtShipInterval = 1.1f;
    private int _builtShips = 0;
 
	void Start () {

        _lastShipBuiltTime = Time.time - _builtShipInterval - 0.01f;

    }
	
	void Update () {

        if (IsStart && _builtShips < ShipsToBuild) {
            TryToBuildShip();
        }

	}

    private void TryToBuildShip() {

        if (_lastShipBuiltTime + _builtShipInterval < Time.time) {
            _lastShipBuiltTime = Time.time;
            var newShip = Instantiate(Ship, new Vector3(transform.position.x + 0.5f, 0, transform.position.z + 0.7f), Quaternion.identity);
            GameState.Current.Ships.Add(newShip);
            GameState.Current.CalculateGameStatus();
            var newSparkle = Instantiate(Sparkle, new Vector3(transform.position.x + 0.5f, 0.8f, transform.position.z + 0.4f), Quaternion.identity);
            DestroyObject(newSparkle, 2.5f);
            _builtShips++;
        }

    }

}
