using UnityEngine;
using UnityEngine.SceneManagement;

public class LeftJumpLoader : MonoBehaviour
{
    public string sceneToLoad = "JumpLeft"; // The name of the scene to load

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Load the scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
