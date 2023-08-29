using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Weapons : MonoBehaviour
{

    Sprite[] WeaponSprites;
    public string CurrentWeaponName;
    private Dictionary<string, Sprite> dictionary;

    void Start()
    {
        WeaponSprites = Resources.LoadAll<Sprite>("weapons_5.0");
        dictionary = WeaponSprites.ToDictionary(k => k.name, v => v);
    }

    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = dictionary[CurrentWeaponName];
    }
}