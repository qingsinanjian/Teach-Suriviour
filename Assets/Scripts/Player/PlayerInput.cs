using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector2 inputDir;
    public float moveSpeed;
    public float lastInputX;
    public float lastInputY;

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        inputDir = new Vector2(h, v).normalized;
    }

    private void Move()
    {
        if(inputDir == Vector2.zero)
        {
            return;
        }
        if(inputDir.x != 0 || inputDir.y != 0)
        {
            transform.Translate(inputDir * Time.deltaTime * moveSpeed);
            lastInputX = inputDir.x;
            lastInputY = inputDir.y;
        }
    }
}
