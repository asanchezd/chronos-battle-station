using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour {

    public List<GameObject> MapItems = new List<GameObject>();
    public GameObject GroundTile;
    public GameObject PathPoint;
    public GameObject Ship;
    public GameObject Portal;
    public GameObject Turret;

    private MissionsDescriptor _missionsData = null;
    private MissionDescriptor _currentMissionData = null;

    void Start()
    {
        LoadMissionsData();
        LoadCurrentMissionData();
        BuildPath();
        BuildMap();
        UpdateData();
    }

    private void UpdateData() {
        GameState.Current.CurrentMissionName = _currentMissionData.Name;
        GameState.Current.MissionShips = _currentMissionData.Ships;
        GameState.Current.CalculateGameStatus();        
    }

    private void LoadMissionsData()
    {
        TextAsset targetFile = Resources.Load<TextAsset>(@"Missions\missions");
        _missionsData = JsonUtility.FromJson<MissionsDescriptor>(targetFile.text);
        GameState.Current.TotalMissionsCount = _missionsData.Missions.Count;
    }

    private void LoadCurrentMissionData()
    {
        var currentMissionName = _missionsData.Missions[GameState.Current.CurrentMissionIndex];
        TextAsset targetFile = Resources.Load<TextAsset>(@"Missions\mission-" + currentMissionName.ToLower());
        _currentMissionData = JsonUtility.FromJson<MissionDescriptor>(targetFile.text);
    }

    private void BuildMap()
    {
        for (int i = 0; i < _currentMissionData.Map.Count; i++)
        {
            for (int j = 0; j < _currentMissionData.Map[i].D.Count; j++)
            {
               InstanciateFromIndex(_currentMissionData.Map[j].D[i],i,j);
                
            }
        }
    }

    private void BuildPath()
    {
        GameState.Current.PathPoints.Clear();
        for (int i = 0; i < _currentMissionData.Path.Count; i++)
        {
            var newItem = Instantiate(PathPoint, new Vector3(_currentMissionData.Path[i].x, 0, -_currentMissionData.Path[i].y), transform.rotation);
            GameState.Current.PathPoints.Add(newItem);
        }
    }

    private void InstanciateFromIndex(int index, int x , int z) {

        var includeGround = true;
        var applyScale = true;
        var isPortal = false;
        GameObject template = null;

        if (index > 4) {
            template = MapItems[index-5];
        }

        if (index == 2)
        {
            template = Turret;
            applyScale = false;
        }

        if (index == 3)
        {
            isPortal = true;
            template = Portal;
            applyScale = false;
        }

        if (index == 0) {
            includeGround = false;
        }

        if (template != null)
        {
            var newItem = Instantiate(template, new Vector3(x, 0, -z), transform.rotation);
            if (applyScale) {
                newItem.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            }
            if (isPortal)
            {
                if (z > 10)
                {
                    newItem.GetComponent<PortalController>().ShipsToBuild = _currentMissionData.Ships;
                    newItem.GetComponent<PortalController>().IsStart = true;
                    newItem.GetComponent<PortalController>().Ship = Ship;
                }
            }
        }

        if (includeGround) {
            var newItem = Instantiate(GroundTile, new Vector3(x, 0, -z), transform.rotation);
            newItem.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        }

    }

}
