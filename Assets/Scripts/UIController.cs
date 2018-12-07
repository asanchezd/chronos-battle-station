using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    private bool _isRestarting = false;

	void Start () {
        GameState.Current.GameUIController = this;
	}
	
	void Update () {

        if (Input.GetKeyDown("s"))
        {
            if (!_isRestarting)
            {
                _isRestarting = true;
                RestartMission();
            }
        }

	}

    public void RestartMission() {
        GameState.Current.StartMission();
    }

    public void NextMission()
    {
        GameState.Current.NextMission();
    }

    public void UpdateUI( string missionName , string gameStatusData) {
        GameObject.Find("Percent").GetComponent<Text>().text = gameStatusData;
        GameObject.Find("Mission").GetComponent<Text>().text = "Mission " + missionName;
    }
}
