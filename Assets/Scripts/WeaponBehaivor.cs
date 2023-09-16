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

    [SerializeField] private AudioClip swap;

    [SerializeField] private float betweenShotsCooldown;
    private float bubilda;
    private bool gunState;
    private float CDR = 0;

    [SerializeField] private int maxShotgunAmmo;
    private int curentShotgunAmmo;
    [SerializeField] private int maxSssr2Ammo;
    private int curentSssr2Ammo;
    [SerializeField] private int maxRifleAmmo;
    private int curentRifleAmmo;
    [SerializeField] private float reloadTime;
    void Start()
    {
        curentShotgunAmmo = maxShotgunAmmo;
        curentSssr2Ammo = maxSssr2Ammo;
        curentRifleAmmo = maxRifleAmmo;

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
        if (Input.GetKeyDown(KeyCode.Alpha1) && weapons.CurrentWeaponName != "sssr2")
        {
            audio.PlayOneShot(swap);
            projectile = projectilePrefab.GetComponent<Projectile>();
            weapons.CurrentWeaponName = "sssr2";
            projectile.damage = sssr2Dmage;
            projectile.speed = sssr2Speed;
            projectile.range = sssr2Range;
            projectile.magnetic = false;
            projectile.GunSpread = new int[] { 0, 0 };
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.CurrentWeaponName != "rifle")
        {
            audio.PlayOneShot(swap);
            projectile = riflePojectilePrefab.GetComponent<Projectile>();
            weapons.CurrentWeaponName = "rifle";
            projectile.damage = rifleDmage;
            projectile.speed = rifleSpeed;
            projectile.range = rifleRange;
            projectile.magnetic = true;
            projectile.GunSpread = new int[] {-5, 5};
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.CurrentWeaponName != "shotgun")
        {
            audio.PlayOneShot(swap);
            projectile = projectilePrefab.GetComponent<Projectile>();
            weapons.CurrentWeaponName = "shotgun";
            projectile.damage = shotgunDmage;
            projectile.speed = shotgunSpeed;
            projectile.range = shotgunRange;
            projectile.magnetic = false;
            projectile.GunSpread = new int[] { -6, 6 };
        }

        if (Input.GetMouseButtonDown(1) && GameObject.Find("MagnetPrefab(Clone)") == null && weapons.CurrentWeaponName == "rifle")
        {
            Instantiate(magnetPrefab, Gun.position, Gun.rotation);
            Destroy(GameObject.Find("MagnetPrefab(Clone)"), 700 * Time.deltaTime);
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

        if (Input.GetKeyDown(KeyCode.R))  StartCoroutine(Reload());

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
        Debug.Log("started");
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

    IEnumerator Reload()
    {
        if (weapons.CurrentWeaponName == "sssr2")
        {
            weapons.CurrentWeaponName = "sssr2reload";
            yield return new WaitForSeconds(reloadTime);
            curentSssr2Ammo = maxSssr2Ammo;
            weapons.CurrentWeaponName = "sssr2";
        }
        else if (weapons.CurrentWeaponName == "rifle")
        {
            weapons.CurrentWeaponName = "riflereload";
            yield return new WaitForSeconds(reloadTime);
            curentRifleAmmo = maxRifleAmmo;
            weapons.CurrentWeaponName = "rifle";
        }
        else if (weapons.CurrentWeaponName == "shotgun")
        {
            yield return new WaitForSeconds(reloadTime);
            curentShotgunAmmo = maxShotgunAmmo;
        }
        else yield return null;
    }

    void Shot() 
    {
        if (weapons.CurrentWeaponName != "None")
        {
            if (weapons.CurrentWeaponName == "shotgun" && curentShotgunAmmo > 0)
            {
                curentShotgunAmmo--;
                audio.PlayOneShot(shotgunShotAudio, 0.1f);
                for (int i = 0; i < 10; i++)
                {
                    Instantiate(projectilePrefab, shotpoint.position, Gun.rotation);
                }
            }
            else if (weapons.CurrentWeaponName == "rifle" && curentRifleAmmo > 0)
            {
                curentRifleAmmo--;
                audio.PlayOneShot(rifleShotAudio, 0.1f);
                Instantiate(riflePojectilePrefab, Gun.position, Gun.rotation);
            }
            else if (weapons.CurrentWeaponName == "sssr2" && curentSssr2Ammo > 0)
            {
                curentSssr2Ammo--;
                audio.PlayOneShot(sssr2ShotAudio, 0.1f);
                Instantiate(laserPrefab, shotpoint.position, Gun.rotation);
            }
        }
    }
}