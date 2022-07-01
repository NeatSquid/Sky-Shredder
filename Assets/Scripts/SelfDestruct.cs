using System.Collections;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DelayedSelfDestruct());
    }

    private IEnumerator DelayedSelfDestruct()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}