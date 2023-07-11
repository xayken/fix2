 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponDataSO weaponData;

    private void OnTriggerEnter2D(Collider2D other)
    {

        WeaponManager weaponManager = other.GetComponent<WeaponManager>();
        if (weaponManager != null)
        {
            
            weaponManager.EquipWeapon(weaponData);
            Destroy(gameObject);

        }
    }

}
