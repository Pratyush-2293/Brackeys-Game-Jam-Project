using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageWaveKiller : MonoBehaviour
{
    public float duration = 0.1f;
    void Start()
    {
        StartCoroutine(DeleteSelf(duration));
    }

    IEnumerator DeleteSelf(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
