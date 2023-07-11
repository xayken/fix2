using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Transform weaponSlot;
    private GameObject currentWeapon;
    [SerializeField] private WeaponDataSO equippedWeapon;
    [SerializeField] private float dropForwardForce;
    [SerializeField] private float dropUpwardForce;
    [SerializeField] private Transform player;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    [SerializeField] private float pickUpRange;
    [SerializeField] private PlayerShooting gun;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        gun = GameObject.FindGameObjectWithTag("Aim").GetComponent<PlayerShooting>();
    }

    private void Start()
    {
        if(currentWeapon != null)
        {
            EnableWeapon();
        }

        else
        {
            DisableWeapon();
        }
    }

    private void Update()
    {

        

    }


    public void EquipWeapon(WeaponDataSO weaponData)
    {

        equippedWeapon = weaponData;

        if (currentWeapon != null)
        {
            Destroy(gameObject);
        }



        currentWeapon = Instantiate(weaponData.gunPrefab);
        currentWeapon.transform.SetParent(weaponSlot);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }





   


    private void DisableWeapon()
    {


        gun.enabled = false;



        rb.isKinematic = false;


        coll.isTrigger = false;

    }

    private void EnableWeapon()
    {
        gun.enabled = true;
        rb.isKinematic = true;
        coll.isTrigger = true;
        
    }





}
