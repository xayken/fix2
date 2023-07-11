using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public PlayerShooting gun;
    public Rigidbody2D rb;
    public BoxCollider2D coll;
    public Transform player, gunContainer, Cam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Awake()
    {
        gun = GetComponent<PlayerShooting>();

        // Setup
        if (!equipped)
        {
            DisableWeapon();
        }
        if (equipped)
        {
            EnableWeapon();
        }
    }

    private void Update()
    {
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull)
        {
            PickUp();
        }

        if (equipped && Input.GetKeyDown(KeyCode.G))
        {
            Drop();
        }
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
        slotFull = true;
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        rb.isKinematic = true;
        coll.isTrigger = true;

        // Enable shoot script
        gun.enabled = true;

        // Make weapon a child of the camera and move it to the default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        rb.isKinematic = false;
        coll.isTrigger = false;

        // Set parent to null
        transform.SetParent(null);

        // Gun carries momentum of the player
        rb.velocity = player.GetComponentInParent<Rigidbody2D>().velocity;

        // Add force
        rb.AddForce(Cam.forward * dropForwardForce, ForceMode2D.Impulse);
        rb.AddForce(Cam.up * dropUpwardForce, ForceMode2D.Impulse);

        // Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(random * 10f);

        // Disable shoot script
        gun.enabled = false;
    }
}