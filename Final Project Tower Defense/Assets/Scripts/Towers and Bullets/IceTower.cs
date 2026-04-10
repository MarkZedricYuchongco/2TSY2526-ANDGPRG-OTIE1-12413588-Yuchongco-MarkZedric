using UnityEngine;

public class SlowTower : MonoBehaviour
{
    public float slowAmount = 0.5f;
    public float slowDuration = 1.0f;
    public float range = 5f;

    void Start()
    {
        SphereCollider col = gameObject.GetComponent<SphereCollider>();
        if (col == null) col = gameObject.AddComponent<SphereCollider>();
        col.isTrigger = true;
        col.radius = range;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.ApplySlow(slowAmount, slowDuration);
            }
        }
    }
}