using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private bool isMoving = false;
    [SerializeField] private LayerMask blockingLayer;
    [SerializeField] public float moveSpeed = 5f;

    [SerializeField] public TextMeshProUGUI moveCount;
    private int moveCounter = 0;
    
    void Start()
    {
        UpdateHUD(moveCounter);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovementInput();
        UpdateHUD(moveCounter);
    }

    void HandleMovementInput()
    {
        if (isMoving) return;

        var _movement = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.W))
        {
            _movement += Vector3.forward;
        } else if (Input.GetKeyDown(KeyCode.S))
        {
            _movement += Vector3.back;
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            _movement += Vector3.left;
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            _movement += Vector3.right;
        }
        
        if (_movement != Vector3.zero)
        {
            moveCounter++;
            GameManager.instance.sfxSource.PlayOneShot(GameManager.instance.moveSound);
            HandleMovement(_movement);
        }
    }

    void HandleMovement(Vector3 dir)
    {
        var targetPosition = transform.position + dir;

        if(!Physics.Raycast(transform.position, dir, out RaycastHit hit, 1f, blockingLayer))
        {
            StartCoroutine(MoveToPosition(targetPosition));    
        }
        else if (hit.collider.CompareTag("Box"))
        {
            var box = hit.collider.GetComponent<BoxController>();
            if (box != null && box.TryToPushBox(dir, moveSpeed))
            {
                GameManager.instance.sfxSource.PlayOneShot(GameManager.instance.pushSound);
                StartCoroutine(MoveToPosition(targetPosition));
            }
        }
    }

    private IEnumerator MoveToPosition(Vector3 target)
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

    private void UpdateHUD(int moveCounter)
    {
        if (moveCount != null)
        {
            moveCount.text = $"Moves: {moveCounter}";
        }
    }
}
