using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private SpriteRenderer Ded;
    private float rotation;

    private void Start()
    {
        Ded = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        rotation = Input.GetAxis("Horizontal");
        if (rotation < 0) Ded.flipX = true;
        else if (rotation > 0) Ded.flipX = false;
    }
}
