using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeftLoad : MonoBehaviour
{
   // Specify the level name or index you want to load
    public string levelToLoad;

    // Update is called once per frame
    void Update()
    {
        // Check if the "E" key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Call the LoadLevel function to load the specified level
            LoadLevel();
        }
    }

    // Function to load the specified level
    void LoadLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
