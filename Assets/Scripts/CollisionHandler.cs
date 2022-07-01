using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private PlayerController _playerController;
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _shipModel;

    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
    // private void OnCollisionEnter(Collision collision)
    // {
    //     print($"OnCollisionEnter says {gameObject.name} collided with {collision.gameObject.name}");
    // }

    private void OnTriggerEnter(Collider other)
    {
        // print($"OnTriggerEnter says {gameObject.name} collided with {other.gameObject.name}");
        _playerController.DisableControls();
        StartCoroutine(ProcessCollision());
    }


    private IEnumerator ProcessCollision()
    {
        var e = Instantiate(_explosion, transform);
        e.SetActive(true);
        _shipModel.SetActive(false);

        yield return new WaitForSeconds(1f);

        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
    }
}