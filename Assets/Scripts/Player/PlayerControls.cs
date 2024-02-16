using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpStrength = 5f;
    private Rigidbody2D charBody;
    private BoxCollider2D charCollider;
    [SerializeField] private LayerMask jumpableGround;

    private void Start()
    {
        charBody = GetComponent<Rigidbody2D>();
        charCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Get input from the player
        float dirX = Input.GetAxisRaw("Horizontal");
        charBody.velocity = new Vector2(dirX * moveSpeed, charBody.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            charBody.velocity = new Vector2(charBody.velocity.x, jumpStrength);
        }

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

    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(charCollider.bounds.center, charCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
