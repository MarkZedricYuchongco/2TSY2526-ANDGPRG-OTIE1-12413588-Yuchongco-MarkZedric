using TMPro;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] Transform firingPoint;
    [SerializeField] GameObject type1Projectile;
    [SerializeField] float type1FiringSpeed;
    [SerializeField] GameObject type2Projectile;
    [SerializeField] float type2FiringSpeed;
    [SerializeField] GameObject type3Projectile;
    [SerializeField] float type3FiringSpeed;
    [SerializeField] GameObject type4Projectile;
    [SerializeField] float type4FiringSpeed;

    public static PlayerGun Instance;

    private float lastTimeShot = 0;

    void Awake()
    {
        Instance = GetComponent<PlayerGun>();
    }

    void Update()
    {
        
    }

    public void Shoot1()
    {
        if (lastTimeShot + type1FiringSpeed <= Time.time)
        {
            lastTimeShot = Time.time;
            Instantiate(type1Projectile, firingPoint.position, firingPoint.rotation);
        }
    }

    public void Shoot2()
    {
        if (lastTimeShot + type2FiringSpeed <= Time.time)
        {
            lastTimeShot = Time.time;
            Instantiate(type2Projectile, firingPoint.position, firingPoint.rotation);
        }
    }

    public void Shoot3()
    {
        if (lastTimeShot + type3FiringSpeed <= Time.time)
        {
            lastTimeShot = Time.time;
            Instantiate(type3Projectile, firingPoint.position, firingPoint.rotation);
        }
    }

    public void Shoot4()
    {
        if (lastTimeShot + type4FiringSpeed <= Time.time)
        {
            lastTimeShot = Time.time;
            for (int i = -2; i <= 2; i++)
            {
                Quaternion _spreadRotation = firingPoint.rotation * (Quaternion.Euler(0, i * 15, 0));
                Instantiate(type4Projectile, firingPoint.position, _spreadRotation);
            }
        }
    }
}
