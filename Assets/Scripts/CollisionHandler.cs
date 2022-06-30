using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        print($"OnCollisionEnter says {gameObject.name} collided with {collision.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other)
    {
        print($"OnTriggerEnter says {gameObject.name} collided with {other.gameObject.name}");
        Reload();
    }


    private void Reload()
    {
        SceneManager.LoadScene(0);
    }
}