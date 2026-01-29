using UnityEngine;

public class TargetPlaneControl : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Start()
    {

    }

    void Update()
    {
        MovePlane();
    }

    void MovePlane()
    {
        transform.position = target.position;
    }
}
