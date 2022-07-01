using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _hitVfx;
    private Transform _parent;
    [SerializeField] private int _scorePerHit;

    private ScoreBoard _scoreBoard;
    [SerializeField] private int _hitPoints = 8;

    private void Awake()
    {
        _scoreBoard = FindObjectOfType<ScoreBoard>();

        var rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;


        _parent = GameObject.FindGameObjectWithTag("JunkSpawnParent").transform;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (_hitPoints < 1)
            KillEnemy();
    }

    private void ProcessHit()
    {
        var n = Instantiate(_hitVfx, transform.position, Quaternion.identity);
        n.transform.SetParent(_parent);

        _hitPoints--;
        _scoreBoard.IncreaseScore(_scorePerHit);
    }

    private void KillEnemy()
    {
        var n = Instantiate(_explosion, transform.position, Quaternion.identity);
        n.transform.SetParent(_parent);

        Destroy(gameObject);
    }
}