using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Page{
    public string name;
    public GameObject o;
}

[System.Serializable]
public struct Row{
    public List<Page> r;
}


public class MoveScreen : MonoBehaviour
{
    public List<Row> grid;
    public Dictionary<string, Page> pageLU;

    void Start()
    {
        foreach(Row r in grid){
            foreach(Page p in r.r){
                pageLU.Add(p.name, p);
            }
        }
    }
}
