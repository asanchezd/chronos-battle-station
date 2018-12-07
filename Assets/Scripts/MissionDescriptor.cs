using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MissionDescriptor {

    public string Name = "";
    public int Ships = 1;
    public List<ListWrapper> Map = new List<ListWrapper>();
    public List<Vector2> Path = new List<Vector2>();

}

[Serializable]
public class ListWrapper
{
    public List<int> D = new List<int>();

}
