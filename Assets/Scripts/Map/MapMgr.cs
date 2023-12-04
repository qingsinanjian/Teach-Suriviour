using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapMgr : MonoBehaviour
{
    private static MapMgr instance;
    public static MapMgr Instance {  get { return instance; } }

    public GameObject mapBlockPrefab;
    private List<GameObject> maps = new List<GameObject>();
    public Vector3 BlockOffset = new Vector3(6, 6, 0);
    public MapBlock currentBlock;
    public List<GameObject> hasCreateBlocks = new List<GameObject>();
    public float CollectDis = 12f;
    public GameObject player;

    private void Awake()
    {
        instance = this;
        CreatMapBlocks();
    }

    private void CreatMapBlocks()
    {
        for (int i = 0; i < 24; i++)
        {
            GameObject map = Instantiate(mapBlockPrefab);
            map.transform.parent = transform;
            map.SetActive(false);
            maps.Add(map);
        }
    }

    private bool CheckNeedCreatMap(Vector3 target, float radius = 0.5f)
    {
        if(!Physics2D.OverlapCircle(target, radius, LayerMask.GetMask("MapBlock")))
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 在某一个地图块的触发器处检测某方向的地图块
    /// </summary>
    /// <param name="triggerPos">触发器位置</param>
    /// <param name="dir">玩家移动方向</param>
    /// <param name="offset">偏移值</param>
    private void GetTargetBlockFromDir(Vector3 triggerPos, PlayerMoveDir dir, Vector3 offset)
    {
        Vector3 targetPos = Vector3.zero;
        switch (dir)
        {
            case PlayerMoveDir.Up:
                targetPos = triggerPos + new Vector3(0, offset.y, 0);                
                break;
            case PlayerMoveDir.Down:
                targetPos = triggerPos + new Vector3(0, -offset.y, 0);
                break;
            case PlayerMoveDir.Left:
                targetPos = triggerPos + new Vector3(-offset.x, 0, 0);
                break;
            case PlayerMoveDir.Right:
                targetPos = triggerPos + new Vector3(offset.x, 0, 0);
                break;
            case PlayerMoveDir.LeftUp:
                targetPos = triggerPos + new Vector3(-offset.x, offset.y, 0);
                break;
            case PlayerMoveDir.LeftDown:
                targetPos = triggerPos + new Vector3(-offset.x, -offset.y, 0);
                break;
            case PlayerMoveDir.RightUp:
                targetPos = triggerPos + new Vector3(offset.x, offset.y, 0);
                break;
            case PlayerMoveDir.RightDown:
                targetPos = triggerPos + new Vector3(offset.x, -offset.y, 0);
                break;
        }
        if (CheckNeedCreatMap(targetPos))
        {
            SetMapBlockPosition(dir, BlockOffset);
        }
    }

    public GameObject TryGetBlock()
    {
        GameObject go = null;
        if(maps.Count > 0)
        {
            go = maps.Find(a => !a.activeInHierarchy);
            go.SetActive(true);
            go.transform.parent = null;

            maps.Remove(go);
            return go;
        }
        Debug.Log("当前获取的地图块为空，请创建新的地图块");
        return null;
    }

    public void SetMapBlockPosition(PlayerMoveDir dir, Vector3 offset)
    {
        Vector3 nowOffset = Vector3.zero;
        switch(dir)
        {
            case PlayerMoveDir.Up:
                nowOffset = new Vector3(0, offset.y, 0);
                break;
            case PlayerMoveDir.Down:
                nowOffset = new Vector3(0, -offset.y, 0);
                break;
            case PlayerMoveDir.Left:
                nowOffset = new Vector3(-offset.x, 0, 0);
                break;
            case PlayerMoveDir.Right:
                nowOffset = new Vector3(offset.x, 0, 0);
                break;
            case PlayerMoveDir.LeftUp:
                nowOffset = new Vector3(-offset.x, offset.y, 0);
                break;
            case PlayerMoveDir.LeftDown:
                nowOffset = new Vector3(-offset.x, -offset.y, 0);
                break;
            case PlayerMoveDir.RightUp:
                nowOffset = new Vector3(offset.x, offset.y, 0);
                break;
            case PlayerMoveDir.RightDown:
                nowOffset = new Vector3(offset.x, -offset.y, 0);
                break;
        }

        GameObject go = TryGetBlock();
        if(go != null)
        {
            go.transform.position = currentBlock.transform.position + nowOffset;
            hasCreateBlocks.Add(go);
        }
    }

    public void CheckAllDir(Transform tr)
    {
        GetTargetBlockFromDir(tr.position, PlayerMoveDir.Up, BlockOffset);
        GetTargetBlockFromDir(tr.position, PlayerMoveDir.Down, BlockOffset);
        GetTargetBlockFromDir(tr.position, PlayerMoveDir.Left, BlockOffset);
        GetTargetBlockFromDir(tr.position, PlayerMoveDir.Right, BlockOffset);
        GetTargetBlockFromDir(tr.position, PlayerMoveDir.LeftUp, BlockOffset);
        GetTargetBlockFromDir(tr.position, PlayerMoveDir.LeftDown, BlockOffset);
        GetTargetBlockFromDir(tr.position, PlayerMoveDir.RightUp, BlockOffset);
        GetTargetBlockFromDir(tr.position, PlayerMoveDir.RightDown, BlockOffset);
    }

    public void CollectMapBlock()
    {
        List<GameObject> tempList = new List<GameObject>();
        if(hasCreateBlocks.Count > 0)
        {
            for (int i = 0; i < hasCreateBlocks.Count; i++)
            {
                if (Vector3.Distance(hasCreateBlocks[i].transform.position, player.transform.position) >= CollectDis)
                {
                    GameObject temp = hasCreateBlocks[i];
                    tempList.Add(temp);
                }
            }
        }
        for (int i = 0; i< tempList.Count; i++)
        {
            GameObject temp = tempList[i];
            maps.Add(temp);
            temp.transform.parent = transform;
            temp.transform.localPosition = Vector3.zero;
            temp.SetActive(false);
            hasCreateBlocks.Remove(temp);
        }
    }
}
