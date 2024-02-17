using System.Collections;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    public float moveSpeed = 2f; // Speed at which the enemy moves
    public float patrolRange = 5f; // Distance the enemy patrols
    public float followRange = 3f; // Distance at which the enemy starts following the player
    public float health = 100f; // Enemy's health
    public float attackRange = 1f;
    public GameObject damageWavePrefab;
    public float damageWaveSpeed = 2f;
    public float damageRate = 2f;
    public float damageDelay = 0.4f;

    private Transform player;
    private Vector3 originalPosition;
    private bool movingRight = true;
    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private float nextDamageTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        originalPosition = transform.position;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float moveDirection = 0;
        float facePlayer = transform.position.x - player.position.x;

        // Check if player is within follow range
        if (Vector3.Distance(transform.position, player.position) < attackRange)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else if(Vector3.Distance(transform.position, player.position) < followRange)
        {
            // Move towards player
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // look towards player
            if (facePlayer > 0)
            {
                sprite.flipX = true; // moving left
            }
            else if (facePlayer < 0)
            {
                sprite.flipX = false; // moving right
            }
        } else {
            // Patrol left and right within patrol range
            if (Mathf.Abs(transform.position.x - originalPosition.x) > patrolRange)
            {
                movingRight = !movingRight;
            }

            moveDirection = movingRight ? 1 : -1;
            transform.Translate(Vector3.right * moveDirection * moveSpeed * Time.deltaTime);
        }

        // configuring animator states
        if (moveDirection < 0)
        {
            sprite.flipX = true; // moving left
        }
        else if(moveDirection > 0)
        {
            sprite.flipX = false; // moving right
        }
        
        //checking if player is in range to attack
        if(Vector3.Distance(transform.position, player.position) < attackRange)
        {
            anim.SetBool("attack", true);
            if(facePlayer> 0)
            {
                if (Time.time >= nextDamageTime)
                {
                    // Fire damage wave
                    StartCoroutine(Attack(-1));

                    // Set the next fire time based on the damage rate
                    nextDamageTime = Time.time + 1f / damageRate;
                }
            }
            else if(facePlayer < 0)
            {
                if (Time.time >= nextDamageTime)
                {
                    // Fire damage wave
                    StartCoroutine(Attack(1));

                    // Set the next fire time based on the damage rate
                    nextDamageTime = Time.time + 1f / damageRate;
                }
            }
        }
        else
        {
            anim.SetBool("attack", false);
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

    IEnumerator Attack(int dir) // input dir as 1 for right, -1 for left.
    {
        yield return new WaitForSeconds(damageDelay);

        GameObject damageWave = Instantiate(damageWavePrefab, transform.position, Quaternion.identity);
        Rigidbody2D damageRB = damageWave.GetComponent<Rigidbody2D>();

        if(damageRB != null)
        {
            Vector2 velocity = new Vector2(damageWaveSpeed * dir, 0f);

            damageRB.velocity = velocity;
        }
        else
        {
            Debug.LogError("Rigidbody2D component not found on the damageWave prefab!");
        }
    }

    void Die()
    {
        // Handle death
        Destroy(gameObject);
    }
}
