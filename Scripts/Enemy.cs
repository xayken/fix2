using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float detectionRange = 5f;
    public float fireInterval = 2f;
    public int damage = 1;
    public int maxHealth = 10;
    private int currentHealth;


    private Rigidbody2D rb;
    private Transform currentPoint;
    private Transform player;
    private float speed = 2f;
    private float lastFireTime;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;

    }

    private void Update()
    {
        
        if (IsPlayerWithinRange())
        {
            RotateTowardsPlayer();
            if (Time.time - lastFireTime >= fireInterval)
            {
                lastFireTime = Time.time;
                Fire();
            }
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            Flip();
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            Flip();
            rb.velocity = new Vector2(-speed, 0);
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
        {
            currentPoint = pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
        {
            currentPoint = pointB.transform;
        }
    }

    private void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private bool IsPlayerWithinRange()
    {
        return Vector2.Distance(transform.position, player.position) <= detectionRange;
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
    }

    private void Fire()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle death here
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);
    }
}