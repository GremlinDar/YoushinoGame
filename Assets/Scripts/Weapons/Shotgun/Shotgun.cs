using UnityEngine;
using TMPro;
using System.Collections;

public class Shotgun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public float barrelLength = 0.1f;

    private Camera mainCamera;
    
    private PlayerMovement playerMovement;

    public GameObject ammoPistol;

    public Pistol pistol;

    public GameObject ammoAk;

    public AkGun akGun;
    public int currentAmmo = 8;
    public int allAmmo = 0;
    public int fullAmmo = 24;

    void Start()
    {
        mainCamera = Camera.main;
        playerMovement = GetComponentInParent<PlayerMovement>();
        Cursor.visible = false;
        
        if (firePoint != null)
        {
            firePoint.localPosition = new Vector3(barrelLength, 0f, 0f);
        }
        
        UpdateUI(); 
    }

    void Update()
    {
        AimAtMouse();

        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            Shoot();
            currentAmmo--;
            UpdateUI();
        }

        if (Input.GetKeyDown(KeyCode.R) && allAmmo > 0)
        {
            StartCoroutine(Reload());
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<AkClip>())
        {
            akGun.allAmmo += 30;
            Destroy(other.gameObject);
            UpdateUI();
        }
        else if (other.GetComponent<ShotgunClip>())
        {
            allAmmo += 8;
            Destroy(other.gameObject);
            UpdateUI();
        }
        else if (other.GetComponent<PistolClip>())
        {
            pistol.allAmmo += 15;
            Destroy(other.gameObject);
            UpdateUI();
        }


    }
    
    void UpdateUI()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateAmmoCountShotgun(currentAmmo, allAmmo);
        }

        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateAmmoCountPistol(pistol.currentAmmo, pistol.allAmmo);
        }
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateAmmoCountAk(akGun.currentAmmo, akGun.allAmmo);
        }
    }
    
    void AimAtMouse()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - transform.position).normalized;

        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        
        firePoint.localPosition = new Vector3(barrelLength, 0f, 0f);
    }

public void Shoot()
{
    if (currentAmmo <= 0 || bullet == null || firePoint == null) return;

    Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    mouseWorldPos.z = 0f;
    Vector2 direction = (mouseWorldPos - firePoint.position).normalized;

    float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    int bulletCount = 10;
    float spreadAngle = 30f;

    Vector3 originalScale = bullet.transform.localScale;

    for (int i = 0; i < bulletCount; i++)
    {
        float spreadOffset = spreadAngle * ((float)i / (bulletCount - 1) - 0.5f);
        float finalAngle = baseAngle + spreadOffset;
        Quaternion bulletRotation = Quaternion.Euler(0, 0, finalAngle);

        GameObject newBullet = Instantiate(bullet, firePoint.position, bulletRotation);
        
        newBullet.transform.localScale = originalScale;
    }
}
    
    public IEnumerator Reload()
    {
        int reason = 8 - currentAmmo;

        if (allAmmo >= reason)
        {
            for (int i = 0; i < reason; i++)
            {
                yield return new WaitForSeconds(1);
                currentAmmo += 1;
                allAmmo -= 1;
                UpdateUI();
            }
        }
        else
        {
            for (int i = 0; i < allAmmo; i++)
            {
                yield return new WaitForSeconds(1);
                currentAmmo += 1;
                allAmmo -= 1;
                UpdateUI();
            }
        }
        

       
        
    }
}