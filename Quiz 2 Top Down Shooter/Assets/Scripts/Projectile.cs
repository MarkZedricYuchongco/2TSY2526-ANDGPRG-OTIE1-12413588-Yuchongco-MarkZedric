using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 firingPoint;

    [SerializeField] private float projectileSpeed;
    [SerializeField] private float maxProjectileLifespan;
    [SerializeField] public float damage;
    private float timeSpawned = 0;

    void Start()
    {
        firingPoint = transform.position;
        timeSpawned = Time.time;
    }

    void Update()
    {
        MoveProjectile();
    }

    void MoveProjectile()
    {
        if (timeSpawned + maxProjectileLifespan <= Time.time)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
        }
    }
}
