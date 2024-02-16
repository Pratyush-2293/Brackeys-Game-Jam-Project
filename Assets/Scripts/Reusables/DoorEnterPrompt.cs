using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnterPrompt : MonoBehaviour
{
    public GameObject enterPrompt = null;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enterPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")){
            enterPrompt.SetActive(false);
        }
    }
}
