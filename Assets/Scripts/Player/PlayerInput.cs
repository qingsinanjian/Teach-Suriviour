using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerMoveDir
{
    Up, Down, Left, Right,
    LeftUp, RightUp,
    LeftDown, RightDown,
}

public class PlayerInput : MonoBehaviour
{
    public Vector2 inputDir;
    public float moveSpeed;
    public float lastInputX;
    public float lastInputY;
    public PlayerMoveDir moveDir;

    private void Update()
    {
        GetInput();
        GetDirBaseInput();
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

    private void GetDirBaseInput()
    {
        if (inputDir == Vector2.zero) return;

        if(inputDir.x == 0 &&  inputDir.y > 0)
        {
            moveDir = PlayerMoveDir.Up;
        }
        else if (inputDir.x == 0 && inputDir.y < 0)
        {
            moveDir = PlayerMoveDir.Down;
        }
        else if (inputDir.x < 0 && inputDir.y == 0)
        {
            moveDir = PlayerMoveDir.Left;
        }
        else if (inputDir.x > 0 && inputDir.y == 0)
        {
            moveDir = PlayerMoveDir.Right;
        }
        if (inputDir.x < 0 &&  inputDir.y < 0)
        {
            moveDir = PlayerMoveDir.LeftDown;
        }
        else if(inputDir.x < 0 && inputDir.y > 0)
        {
            moveDir = PlayerMoveDir.LeftUp;
        }
        else if (inputDir.x > 0 && inputDir.y < 0)
        {
            moveDir = PlayerMoveDir.RightDown;
        }
        else if (inputDir.x > 0 && inputDir.y > 0)
        {
            moveDir = PlayerMoveDir.RightUp;
        }
    }
}
