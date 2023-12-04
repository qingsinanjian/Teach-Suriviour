using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Animator animator;
    private PlayerInput input;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(input.inputDir != Vector2.zero)
        {
            PlayWalkAnim();
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }
    }

    private void PlayWalkAnim()
    {
        animator.SetBool("IsMoving", true);
        if(input.inputDir.x <= 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}
