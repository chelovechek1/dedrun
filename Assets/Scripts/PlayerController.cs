using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static System.Runtime.CompilerServices.RuntimeHelpers;

public class PlayerController : MonoBehaviour
{
    private PlayerAnimator animator;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float speed;
    [SerializeField] private float jumpForse;
    [SerializeField] private float fallForse;

    private float maxFallingSpeed = -20f;
    private float dashForse = 1f;
    private bool dashReady = true;
    [SerializeField] private bool onGround = true;
    private void Update()
    {
        animator = GetComponent<PlayerAnimator>();
        if (animator.canRun)
        {
            rb.velocity = new Vector2(speed * dashForse * Input.GetAxis("Horizontal"), rb.velocity.y);
        }

        if (rb.velocity.y < maxFallingSpeed) rb.velocity = new Vector2(rb.velocity.x, maxFallingSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && onGround == true) rb.AddForce(new Vector2(0, jumpForse), ForceMode2D.Impulse);

        if (Input.GetKeyDown(KeyCode.S))
        {
            maxFallingSpeed = -100;
            rb.AddForce(new Vector2(0, -fallForse), ForceMode2D.Impulse);
            StartCoroutine(ForsedFallCooldown());
        }

        if (Input.GetKey(KeyCode.LeftShift) && dashReady == true)
        {
            dashReady = false;
            dashForse = 10f;
            StartCoroutine(StopDash());
            StartCoroutine(DashCooldown());
        }
    }
    IEnumerator StopDash()
    {
        yield return new WaitForSeconds(0.05f);
        dashForse = 1f;
    }
    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(2f);
        dashReady = true;
    }

    IEnumerator ForsedFallCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        maxFallingSpeed = -20f;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        onGround = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;   
    }
}