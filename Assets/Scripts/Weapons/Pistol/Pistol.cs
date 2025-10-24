using UnityEngine;
using TMPro;

public class Pistol : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public float barrelLength = 0.1f;

    private Camera mainCamera;
    private PlayerMovement playerMovement;
    
    public GameObject ammoShotgun;

    public Shotgun shotgun;

    public GameObject ammoAk;

    public AkGun akGun;

    public int currentAmmo = 15;
    public int allAmmo = 0;
    public int fullAmmo = 45;

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
            Invoke("Reload", 2f);
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
            shotgun.allAmmo += 8;
            Destroy(other.gameObject);
            UpdateUI();
        }
        else if (other.GetComponent<PistolClip>())
        {
            allAmmo += 15;
            Destroy(other.gameObject);
            UpdateUI();
        }
    }
    
    void UpdateUI()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateAmmoCountPistol(currentAmmo, allAmmo);
        }
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateAmmoCountShotgun(shotgun.currentAmmo, shotgun.allAmmo);
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

    void Shoot()
    {
        if (bullet != null && firePoint != null && currentAmmo > 0)
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
    }
    
    public void Reload()
    {
        int reason = 15 - currentAmmo;

        if (allAmmo >= reason)
        {
            allAmmo -= reason;
            currentAmmo = 15;
        }
        else
        {
            currentAmmo = currentAmmo + allAmmo;
            allAmmo = 0;
        }

        UpdateUI();
        
    }
}