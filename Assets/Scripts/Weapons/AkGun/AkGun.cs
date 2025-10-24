using UnityEngine;
using TMPro;

public class AkGun : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public float barrelLength = 0.1f;

    private Camera mainCamera;

    private PlayerMovement playerMovement;
    
    [SerializeField] public GameObject ammoShotgun;

    public Shotgun shotgun;

    public GameObject ammoPistol;
    public Pistol pistol;

    public int currentAmmo = 30;
    public int allAmmo = 0;
    public int fullAmmo = 60;

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
            allAmmo += 30;
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
            pistol.allAmmo += 15;
            Destroy(other.gameObject);
            UpdateUI();
        }
        
    }

    void UpdateUI()
    {
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateAmmoCountPistol(pistol.currentAmmo, pistol.allAmmo);
        }
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateAmmoCountShotgun(shotgun.currentAmmo, shotgun.allAmmo);
        }
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateAmmoCountAk(currentAmmo, allAmmo);
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
        int reason = 30 - currentAmmo;

        if (allAmmo >= reason)
        {
            allAmmo -= reason;
            currentAmmo = 30;
        }
        else
        {
            currentAmmo = currentAmmo + allAmmo;
            allAmmo = 0;
        }

        UpdateUI();
        
    }
}