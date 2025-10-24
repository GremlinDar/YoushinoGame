using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private TMP_Text ammoCountPistolText;
    [SerializeField] private TMP_Text ammoCountShotgunText;

    [SerializeField] private TMP_Text ammoCountAkText;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateAmmoCountPistol(int currentAmmo, int allAmmo)
    {
        if (ammoCountPistolText != null)
        {
            ammoCountPistolText.text = currentAmmo + " / " + allAmmo;
        }
    }

    public void UpdateAmmoCountShotgun(int currentAmmo, int allAmmo)
    {
        if (ammoCountShotgunText != null)
        {
            ammoCountShotgunText.text = currentAmmo + " / " + allAmmo;
        }
    }
    
    public void UpdateAmmoCountAk(int currentAmmo, int allAmmo)
    {
        if (ammoCountAkText != null)
        {
            ammoCountAkText.text = currentAmmo + " / " + allAmmo;
        }
    }

}