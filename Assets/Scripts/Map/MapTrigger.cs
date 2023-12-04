using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MapMgr.Instance.currentBlock = transform.root.gameObject.GetComponent<MapBlock>();
            MapMgr.Instance.CollectMapBlock();
            MapMgr.Instance.CheckAllDir(transform);
        }
    }
}
