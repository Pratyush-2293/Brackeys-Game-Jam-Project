using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpStrength = 5f;
    public float attackStrength = 20f;
    // public float knockBack = 2f;
    public float health = 100f;
    private Rigidbody2D charBody;
    private BoxCollider2D charCollider;
    private SpriteRenderer charSprite;
    [SerializeField] private LayerMask jumpableGround;
    private Animator charAnim;
    private enum MovementState { idle, running,jumping,falling,attack1,attack2,attack3,hurt,die};
    private float dirX;
    // private bool facingLeft = false; // used in knockback logic

    // Damage Dealing
    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    public GameObject damageWavePrefab;
    public float damageWaveSpeed = 2f;
    public bool facingLeft = false;

    private void Start()
    {
        charBody = GetComponent<Rigidbody2D>();
        charCollider = GetComponent<BoxCollider2D>();
        charSprite = GetComponent<SpriteRenderer>(); 
        charAnim = GetComponent<Animator>();
    }



    void Update()
    {
        // Get input from the player
        dirX = Input.GetAxisRaw("Horizontal");
        charBody.velocity = new Vector2(dirX * moveSpeed, charBody.velocity.y);

        if(charBody.velocity.x < 0)
        {
            facingLeft = true;
        } 
        else if(charBody.velocity.x > 0)
        {
            facingLeft = false;
        }

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            charBody.velocity = new Vector2(charBody.velocity.x, jumpStrength);
        }

        // Attack Logic
        if(Time.time >= nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (facingLeft == false)
                {
                    Attack(1);
                }
                else
                {
                    Attack(-1);
                }
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
        
        UpdateAnimation();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Loading left jump scene
        if (collision.gameObject.CompareTag("LeftJump"))
        {
            SceneManager.LoadScene("JumpLeft");
        }

        // loading right jump scene
        if (collision.gameObject.CompareTag("RightJump"))
        {
            SceneManager.LoadScene("JumpRight");
        }

        if (collision.gameObject.CompareTag("MushroomDamage"))
        {
            TakeDamage(20f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("HostileEnvironment"))
        {
            TakeDamage(30f);
        }
    }

    public void TakeDamage(float damage)
    {
        charAnim.SetTrigger("hurt");
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        charAnim.SetTrigger("die");
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(charCollider.bounds.center, charCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }


    void Attack(int dir)
    {
        //play attack animation
        float randNum = 0f;
        randNum = Random.Range(1, 4);
        if(randNum == 1)
        {
            charAnim.SetTrigger("attack1");
        }
        else if (randNum == 2)
        {
            charAnim.SetTrigger("attack2");
        }
        else
        {
            charAnim.SetTrigger("attack3");
        }
        

        // Launch Damage Wave
        GameObject damageWave = Instantiate(damageWavePrefab, transform.position, Quaternion.identity);
        Rigidbody2D damageRB = damageWave.GetComponent<Rigidbody2D>();

        if (damageRB != null)
        {
            Vector2 velocity = new Vector2(damageWaveSpeed * dir, 0f);

            damageRB.velocity = velocity;
        }
        else
        {
            Debug.LogError("Rigidbody2D component not found on the damageWave prefab!");
        }
    }

    void UpdateAnimation()
    {
        MovementState moveState;

        if (dirX > 0f)
        {
            moveState = MovementState.running;
            charSprite.flipX = false;
        } else if (dirX < 0f)
        {
            moveState = MovementState.running;
            charSprite.flipX = true;
        }
        else
        {
            moveState = MovementState.idle;
        }

        if(charBody.velocity.y > 0.1f)
        {
            moveState = MovementState.jumping;
        } else if(charBody.velocity.y < -0.1f)
        {
            moveState = MovementState.falling;
        }

        charAnim.SetInteger("animState", (int)moveState);
    }

}