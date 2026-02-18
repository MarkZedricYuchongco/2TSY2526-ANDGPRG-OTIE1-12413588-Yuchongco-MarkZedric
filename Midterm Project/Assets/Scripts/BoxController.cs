using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxController : MonoBehaviour
{
    public LayerMask blockingLayer;
    public bool OnGoal => goalCount > 0;

    [SerializeField] private Material boxMaterial;
    [SerializeField] private Material highlightMaterial;
    private int goalCount = 0;
    private bool isMoving = false;

    private void Start()
    {
        boxMaterial = GetComponent<Renderer>().material;
    }
    public bool TryToPushBox(Vector3 dir, float moveSpeed)
    {
        if (isMoving) return false;
        var targetPosition = transform.position + dir;

        if (!Physics.Raycast(transform.position, dir, out RaycastHit hit, 1f, blockingLayer))
        {
            StartCoroutine(MoveToPosition(targetPosition, moveSpeed));
            return true;
        }

        return false;
    }

    private IEnumerator MoveToPosition(Vector3 target, float moveSpeed)
    {
        isMoving = true;

        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime * 5f);
            yield return null;
        }
        transform.position = target;
        isMoving = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Goal"))
        {
            goalCount++;
            gameObject.GetComponent<Renderer>().material = highlightMaterial;
            GameManager.instance.sfxSource.PlayOneShot(GameManager.instance.goalSound);
            GameManager.instance.CheckWin();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Goal"))
        {
            goalCount--;
            if (goalCount <= 0)
            {
                goalCount = 0;
                gameObject.GetComponent<Renderer>().material = boxMaterial;
            }
            else
            {
                gameObject.GetComponent<Renderer>().material = highlightMaterial;
            }
        }
    }
}
