using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 offset;
    public GameObject target;

    private void Start()
    {
        offset = new Vector3(0, 0, -10);
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void LateUpdate()
    {
        if(target != null)
        {
            transform.position = target.transform.position + offset;
        }
    }
}
