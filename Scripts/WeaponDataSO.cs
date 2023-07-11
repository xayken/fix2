using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Weapon Data")]
public class WeaponDataSO : ScriptableObject
{


    public string weaponName;

    public int damage;

    public int ammoCount;

    public float fireRate;

    public float bulletSpeed;

    public GameObject gunPrefab;
    public GameObject bulletPrefab;

}
