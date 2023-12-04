using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    public List<GameObject> obsList = new List<GameObject>();
    public List<Transform> obsPointList = new List<Transform>();

    public List<MapTrigger> mapTriggers = new List<MapTrigger>();

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < obsPointList.Count; i++)
        {
            GameObject temp = obsList[Random.Range(0, obsList.Count)];
            GameObject go = Instantiate(temp);

            go.transform.parent = obsPointList[i].transform;
            go.transform.localPosition = Vector3.zero;
        }
    }
}
