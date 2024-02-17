using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int damage = 10;

    private Transform player;
    private bool isDestroyed = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!isDestroyed)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerControls playerControls = collision.gameObject.GetComponent<PlayerControls>();
            if (playerControls != null)
            {
                playerControls.TakeDamage(damage);
            }
            Destroy(gameObject); // Destroy the enemy game object on impact with the player
        }
    }

    private void OnMouseDown()
    {
        Destroy(gameObject); // Destroy the enemy game object when clicked
    }
}
