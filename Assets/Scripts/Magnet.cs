using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * 50;
    }

    private void Update()
    {
        //Destroy(gameObject, 50 * Time.deltaTime)
        //;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.parent = collision.transform;
        rb.simulated = false;
    }
}
