using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the enemy moves
    public float patrolRange = 5f; // Distance the enemy patrols
    public float followRange = 3f; // Distance at which the enemy starts following the player
    public float health = 100f; // Enemy's health

    private Transform player;
    private Vector3 originalPosition;
    private bool movingRight = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalPosition = transform.position;
    }

    void Update()
    {
        // Check if player is within follow range
        if (Vector3.Distance(transform.position, player.position) < followRange)
        {
            // Move towards player
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            // Patrol left and right within patrol range
            if (Mathf.Abs(transform.position.x - originalPosition.x) > patrolRange)
            {
                movingRight = !movingRight;
            }

            float moveDirection = movingRight ? 1 : -1;
            transform.Translate(Vector3.right * moveDirection * moveSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle death
        Destroy(gameObject);
    }
}
