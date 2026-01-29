using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private float health = 5;
    [SerializeField] private float moveSpeed = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
            HandleMovement();
        HandleHealth();
    }

    void HandleMovement()
    {
        Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        transform.LookAt(targetPosition);

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void HandleRotationInput()
    {
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit))
        {
            transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }

    void HandleHealth()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy hit the player!");
            // Here you can add logic to damage the player
        }
        
        if (other.gameObject.CompareTag("Bullet"))
        {
            Projectile projectile = other.gameObject.GetComponent<Projectile>();
            if (projectile != null)
            {
                Debug.Log("Enemy hit by a bullet!");
                TakeDamage(projectile.damage);
                Destroy(other.gameObject); // Destroy the projectile upon impact
            }
        }
    }
}
