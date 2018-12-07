using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState {

    private static GameState _current = null;

    public List<GameObject> PathPoints = new List<GameObject>();
    public List<GameObject> Ships = new List<GameObject>();
    public List<GameObject> Fields = new List<GameObject>();

    public UIController GameUIController;

    public string CurrentMissionName = "";
    public string CurrentMissionStatus = "";

    public int MissionShips = 0;
    public int MissionCompletedShips = 0;
    public int MissionDestroyedShips = 0;

    public void CalculateGameStatus() {

        var percent = Mathf.CeilToInt((float)(Ships.Count(x => x != null) + MissionCompletedShips) / MissionShips * 100f);
        CurrentMissionStatus = Mathf.Max(0, percent) + " %";

        if (MissionShips > 0 && MissionDestroyedShips == MissionShips)
        {
            GameObject.Find("Canvas").transform.Find("MissionFailedPanel").gameObject.SetActive(true);
            Cursor.visible = true;
        }
        else
        {
            GameObject.Find("Canvas").transform.Find("MissionFailedPanel").gameObject.SetActive(false);
        }

        if (MissionShips > 0 && MissionCompletedShips > 0  && Ships.Count(x => x != null) == 0)
        {
            if (CurrentMissionIndex < TotalMissionsCount - 1)
            {
                GameObject.Find("Canvas").transform.Find("MissionCompletedPanel").gameObject.SetActive(true);
                Cursor.visible = true;
            }
            else
            {
                GameObject.Find("Canvas").transform.Find("GameCompletedPanel").gameObject.SetActive(true);
                Cursor.visible = true;
            }
        }
        else
        {
            GameObject.Find("Canvas").transform.Find("MissionCompletedPanel").gameObject.SetActive(false);
        }

        if (GameUIController != null) {
            GameUIController.UpdateUI(CurrentMissionName, CurrentMissionStatus);
        }
    }

    public float CalculateTimeFactor(Vector3 position) {

        float result = 1;

        foreach (var f in Fields)
        {
            if (f != null && Vector3.Distance(f.transform.position, position) < 1.5f) {
                result *= f.GetComponent<FieldController>().TimeFactor;
            }
        }

        return result;
    }

    public static GameState Current {
        get {
            if (_current == null) {
                _current = new GameState();
            }
            return _current;
        }
    }

    public int CurrentMissionIndex = 0;
    public int TotalMissionsCount = 0;

    public void StartGame() {

        CurrentMissionIndex = 0;
        StartMission();

    }

    private void ClearAll() {
        Ships.Clear();
        PathPoints.Clear();
        Fields.Clear();
        MissionShips = 0;
        MissionCompletedShips = 0;
        MissionDestroyedShips = 0;
        CalculateGameStatus();
    }

    public void StartMission() {
        ClearAll();
        SceneManager.LoadScene("Game");
    }

    public void NextMission()
    {
        ClearAll();
        if (CurrentMissionIndex < TotalMissionsCount - 1) {
            CurrentMissionIndex += 1;
            SceneManager.LoadScene("Game");
        }
    }

}

public enum GameMode {
    Idle,
    Playing,
    Over,
    Completed
}
