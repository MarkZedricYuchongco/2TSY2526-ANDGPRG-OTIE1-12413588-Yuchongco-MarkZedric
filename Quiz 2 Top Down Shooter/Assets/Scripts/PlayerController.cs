using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    //[SerializeField] private float health = 10f;
    private int shootMode = 1;

    void Start()
    {
        shootMode = 1;
    }

    void Update()
    {
        HandleShootModeInput();
        HandleMovementInput();
        HandleRotationInput();
        HandleShootInput();
        //HandleHealth();
    }

    void HandleMovementInput()
    {
        float _horizontalInput = Input.GetAxis("Horizontal");
        float _verticalInput = Input.GetAxis("Vertical");

        Vector3 _movement = new Vector3(_horizontalInput, 0, _verticalInput);
        transform.Translate(_movement * movementSpeed * Time.deltaTime, Space.World);
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

    void HandleShootModeInput()
    {
        if (Input.GetKey(KeyCode.Alpha1) && shootMode != 1)
        {
            shootMode = 1;
            Debug.Log("Switched Shoot Mode to First!");
        }
        else if (Input.GetKey(KeyCode.Alpha2) && shootMode != 2)
        {
            shootMode = 2;
            Debug.Log("Switched Shoot Mode to Second!");
        }
        else if (Input.GetKey(KeyCode.Alpha3) && shootMode != 3)
        {
            shootMode = 3;
            Debug.Log("Switched Shoot Mode to Third!");
        }
        else if (Input.GetKey(KeyCode.Alpha4) && shootMode != 4)
        {
            shootMode = 4;
            Debug.Log("Switched Shoot Mode to Fourth!");
        }
    }

    void HandleShootInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            switch (shootMode)
            {
                case 1:
                    PlayerGun.Instance.Shoot1();
                    break;
                case 2:
                    PlayerGun.Instance.Shoot2();
                    break;
                case 3:
                    PlayerGun.Instance.Shoot3();
                    break;
                case 4:
                    PlayerGun.Instance.Shoot4();
                    break;
                default:
                    PlayerGun.Instance.Shoot1();
                    break;
            }
        }
    }

    //for future health management
    //when player dies, causes several errors because there is no game end management
    //public void TakeDamage(float damage)
    //{
    //    health -= damage;
    //    Debug.Log("Player took damage! Current health: " + health);
    //}

    //void HandleHealth()
    //{
    //    if (health <= 0)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
    //        if (enemy != null)
    //        {
    //            Debug.Log("Player hit by an Enemy!");
    //            TakeDamage(1);
    //            Destroy(other.gameObject); // Destroy the projectile upon impact
    //        }
    //    }
    //}
}
