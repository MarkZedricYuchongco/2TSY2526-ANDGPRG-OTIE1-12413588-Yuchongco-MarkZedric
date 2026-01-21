using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textObject;
    [SerializeField] float movementSpeed;
    [SerializeField] Transform shipModel;
    [SerializeField] float zoomSpeed;

    Transform camTransform;
    Vector3 cameraCurrentPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camTransform = Camera.main.transform;
        cameraCurrentPos = camTransform.localPosition;

        Camera.main.farClipPlane = 20000f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0) * Time.deltaTime * movementSpeed * (cameraCurrentPos.y / 10);
            shipModel.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * movementSpeed * (cameraCurrentPos.y / 10);
            shipModel.rotation = Quaternion.Euler(0, 270, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, 1) * Time.deltaTime * movementSpeed * (cameraCurrentPos.y / 10);
            shipModel.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, 0, -1) * Time.deltaTime * movementSpeed * (cameraCurrentPos.y / 10);
            shipModel.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            cameraCurrentPos.y -= zoomSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            cameraCurrentPos.y += zoomSpeed * Time.deltaTime;
        }

        cameraCurrentPos.y = Mathf.Clamp(cameraCurrentPos.y, 20f, 10000f);

        camTransform.localPosition = cameraCurrentPos;


    }
    
    private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Collision with: " + other.gameObject.name);
            textObject.text = other.gameObject.name;
        }

    private void OnTriggerExit(Collider other)
    {
        textObject.text = "Empty Space";
    }
}
