using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponBehaivor : MonoBehaviour
{
    [SerializeField] private Transform Gun;
    [SerializeField] private Transform shotpoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject riflePojectilePrefab;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject magnetPrefab;
    private Projectile projectile;
    private PlayerController movement;
    private Weapons weapons;
    AudioSource audio;

    [Header ("ссср2")]
    [SerializeField] private float sssr2Dmage;
    [SerializeField] private float sssr2Speed;
    [SerializeField] private float sssr2Range;
    [SerializeField] private AudioClip sssr2ShotAudio;

    [Header("дробовик")]
    [SerializeField] private float shotgunDmage;
    [SerializeField] private float shotgunSpeed;
    [SerializeField] private float shotgunRange;
    [SerializeField] private AudioClip shotgunShotAudio;

    [Header("автомат")]
    [SerializeField] private float rifleDmage;
    [SerializeField] private float rifleSpeed;
    [SerializeField] private float rifleRange;
    [SerializeField] private AudioClip rifleShotAudio;

    [SerializeField] private float betweenShotsCooldown;
    private float bubilda;
    private bool gunState;
    private float CDR = 0;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        movement = GetComponent<PlayerController>();
        weapons = GetComponent<Weapons>();
        weapons.CurrentWeaponName = "None";
    }
    void Update()
    {
        Vector2 Direction = Input.mousePosition - Camera.main.WorldToScreenPoint(Gun.position);
        Gun.rotation = Quaternion.AngleAxis(Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg, Vector3.forward);

        if (Input.mousePosition.x - Camera.main.WorldToScreenPoint(Gun.position).x < 0)
        {
            Gun.GetComponent<SpriteRenderer>().flipY = true;
        }
        else
        {
            Gun.GetComponent<SpriteRenderer>().flipY = false;
        }

        if (Input.GetKey(KeyCode.Alpha0)) weapons.CurrentWeaponName = "None";
        if (Input.GetKey(KeyCode.Alpha1))
        {
            projectile = projectilePrefab.GetComponent<Projectile>();
            weapons.CurrentWeaponName = "sssr2";
            projectile.damage = sssr2Dmage;
            projectile.speed = sssr2Speed;
            projectile.range = sssr2Range;
            projectile.GunSpread = new int[] { 0, 0 };
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            projectile = riflePojectilePrefab.GetComponent<Projectile>();
            weapons.CurrentWeaponName = "rifle";
            projectile.damage = rifleDmage;
            projectile.speed = rifleSpeed;
            projectile.range = rifleRange;
            projectile.GunSpread = new int[] {-5, 5};
        }

        if (Input.GetKey(KeyCode.Alpha3))
        {
            projectile = projectilePrefab.GetComponent<Projectile>();
            weapons.CurrentWeaponName = "shotgun";
            projectile.damage = shotgunDmage;
            projectile.speed = shotgunSpeed;
            projectile.range = shotgunRange;
            projectile.GunSpread = new int[] { -6, 6 };
        }
        if (Input.GetMouseButtonDown(1) && GameObject.Find("MagnetPrefab(Clone)") == null && weapons.CurrentWeaponName == "rifle")
        {
            Instantiate(magnetPrefab, Gun.position, Gun.rotation);
        }

        if (Input.GetMouseButtonDown(0) && bubilda > betweenShotsCooldown)
        {
            bubilda = 0;
            if (weapons.CurrentWeaponName == "rifle")
            {
                //StopCoroutine(Rifleing());
                gunState = true;
                StartCoroutine(Rifleing());
            }
            else if (weapons.CurrentWeaponName == "shotgun")
            {
                //StopCoroutine(ShotGuning());
                gunState = true;
                StartCoroutine(ShotGuning());
            }
            Shot();

        }

        if (Input.GetMouseButtonUp(0))
        {
            StopAllCoroutines();
            gunState = false;
            CDR = 0.40f;
        }
        bubilda += Time.deltaTime;
    }
    IEnumerator Rifleing()
    {
        while (weapons.CurrentWeaponName == "rifle" && gunState == true)
        {
            yield return new WaitForSeconds(0.13f);
            Shot();
        }
        yield return null;
    }

    IEnumerator ShotGuning()
    {
        while (weapons.CurrentWeaponName == "shotgun" && gunState == true)
        {
            yield return new WaitForSeconds(CDR);
            if (CDR > 0.20) CDR -= 0.02f;
            Shot();
        }
        yield return null;
    }

    void Shot() 
    {
        
        if (weapons.CurrentWeaponName != "None")
        {
            if (weapons.CurrentWeaponName == "shotgun")
            {
                audio.PlayOneShot(shotgunShotAudio, 0.1f);
                for (int i = 0; i < 10; i++)
                {
                    Instantiate(projectilePrefab, shotpoint.position, Gun.rotation);
                }
            }
            else if (weapons.CurrentWeaponName == "rifle")
            {
                audio.PlayOneShot(rifleShotAudio, 0.1f);
                Instantiate(riflePojectilePrefab, Gun.position, Gun.rotation);
            }
            else
            {
                audio.PlayOneShot(sssr2ShotAudio, 0.1f);
                Instantiate(laserPrefab, shotpoint.position, Gun.rotation);
            }
        }
    }
}