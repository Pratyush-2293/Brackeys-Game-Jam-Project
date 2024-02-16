using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    void Update()
    {
        // Get input from the player
        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(KeyCode.Space)) // Move up
        {
            verticalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.S)) // Move down
        {
            verticalInput = -1f;
        }

        if (Input.GetKey(KeyCode.D)) // Move right
        {
            horizontalInput = 1f;
        }
        else if (Input.GetKey(KeyCode.A)) // Move left
        {
            horizontalInput = -1f;
        }

        // Calculate the movement direction
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.deltaTime;

        // Move the player
        transform.Translate(movement);
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

}
