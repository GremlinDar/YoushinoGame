using UnityEngine;

public class Wapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public float offsetDistance = 0.5f; // Дистанция от центра игрока
    
    private Vector3 baseOffset;

    void Start()
    {
        
        baseOffset = firePoint.localPosition;
    }

    void Update()
    {
        UpdateFirePointPosition();
        
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    
    void UpdateFirePointPosition()
    {
        Vector2 shootDirection = GetShootDirection();
        

        float playerDirection = transform.localScale.x > 0 ? 1f : -1f;
        
        Vector3 newPosition = Vector3.zero;
        
        if (shootDirection != Vector2.zero)
        {
            newPosition = new Vector3(
                shootDirection.x * offsetDistance * playerDirection,
                shootDirection.y * offsetDistance,
                0
            );
        }
        else
        {

            newPosition = new Vector3(
                offsetDistance * playerDirection,
                0,
                0
            );
        }
        
        firePoint.localPosition = newPosition;
    }
    
    void Shoot()
    {
        Vector2 shootDirection = GetShootDirection();
        float angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        Quaternion bulletRotation = Quaternion.Euler(0f, 0f, angle);

        Instantiate(bullet, firePoint.position, bulletRotation); 
    }
    
    Vector2 GetShootDirection()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
    
        if (horizontal == 0 && vertical == 0)
        {
            return Vector2.right * (transform.localScale.x > 0 ? 1 : -1);
        }
        
        return new Vector2(horizontal, vertical).normalized;
    }
}