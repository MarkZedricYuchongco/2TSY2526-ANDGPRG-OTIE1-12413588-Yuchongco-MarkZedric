using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    GameObject goal;

    NavMeshAgent agent;

    float arrivalThreshold = 1f;

    public int damageToBase = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goal = GameObject.FindGameObjectWithTag("Goal");
        agent = this.GetComponent<NavMeshAgent>();
        agent.SetDestination(goal.transform.position);
        agent.stoppingDistance = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToGoal = Vector3.Distance(transform.position, goal.transform.position);

        if (distanceToGoal <= arrivalThreshold)
        {
            ReachedGoal();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            ReachedGoal();
        }
    }

    void ReachedGoal()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.TakeDamage(damageToBase);
        }

        Debug.Log("Monster deleted at goal.");
        Destroy(gameObject);
    }
}
