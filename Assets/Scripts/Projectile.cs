using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Weapons weapons;

    public float speed;
    public float damage;
    public float range;
    public bool magnetic;

    public int[] GunSpread;

    void Start()
    {
        
        transform.localEulerAngles += new Vector3(0, 0, UnityEngine.Random.Range(GunSpread[0], GunSpread[1]));
        speed += UnityEngine.Random.Range(GunSpread[0], GunSpread[1]);
    }

    void FixedUpdate()
    {
        weapons = GetComponent<Weapons>();
        rb.velocity = transform.right * speed;
        //Destroy(gameObject, range * Time.deltaTime);
        GameObject magnet = (GameObject.Find("MagnetPrefab(Clone)"));
        if (magnet != null && magnet.transform.parent != null && magnetic == true)
        {
            Vector3 dir = (Vector2)magnet.transform.position - rb.position;
            dir.Normalize();
            float rotateAmount = Vector3.Cross(dir, transform.right).z;
            rb.angularVelocity = -rotateAmount * 1000;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Vector3 reflected = Vector3.Reflect(rb.velocity.normalized, collision.GetContact(0).normal);
        //rb.velocity = reflected * speed;
        Destroy (gameObject);
    }
}