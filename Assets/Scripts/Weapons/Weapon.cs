using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public float offsetDistance = 0.5f; 
    
    private Camera mainCamera;
    private Vector3 baseOffset;

    void Start()
    {
        mainCamera = Camera.main;
        baseOffset = firePoint.localPosition;
    }

    void Awake()
    {
        Cursor.visible = false;
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
        float playerDirection;
        Vector2 shootDirection = GetShootDirection();
        if (transform.localScale.x > 0)
        {
            playerDirection = 1;
        }
        else
        {
            playerDirection = -1;
        }

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
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
        
        Vector2 direction = (mousePosition - transform.position).normalized;
        
        return direction;
    }
}