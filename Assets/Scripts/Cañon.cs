using System.Collections;
using TMPro;
using UnityEngine;

public class Cañon : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform shootPosition;

    [Header("UI")]
    [SerializeField] private GameObject reloadPanel;
    [SerializeField] private TMP_Text ammoText;

    [Header("Rotación")]
    [SerializeField, Range(30f, 720f)]
    private float rotationSpeed = 180f;

    [Header("Arma")]
    [SerializeField] private int maxAmmo = 8;
    [SerializeField] private float fireRate = 3f;
    [SerializeField] private float reloadTime = 2f;

    private int currentAmmo;
    private float nextFireTime;
    private bool isReloading;

    private Camera cam;

    // Referencia a las estadísticas del jugador
    private PlayerStats playerStats;

    private void Start()
    {
        cam = Camera.main;

        // Buscar el objeto PlayerStats en la escena
        playerStats = FindFirstObjectByType<PlayerStats>();

        currentAmmo = maxAmmo;

        reloadPanel.SetActive(false);

        UpdateAmmoUI();
    }

    private void Update()
    {
        if (GameManager.IsGameOver)
            return;

        RotateToMouse();

        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isReloading && currentAmmo < maxAmmo)
            {
                StartCoroutine(Reload());
            }
        }
    }

    private void RotateToMouse()
    {
        Vector2 mouseWorldPoint =
            cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction =
            mouseWorldPoint - (Vector2)transform.position;

        float angle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Quaternion targetRotation =
            Quaternion.Euler(0, 0, angle);

        transform.rotation =
            Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        if (isReloading)
            return;

        if (Time.time < nextFireTime)
            return;

        if (currentAmmo <= 0)
        {
            reloadPanel.SetActive(true);
            return;
        }

        Projectile projectile =
            Instantiate(
                projectilePrefab,
                shootPosition.position,
                shootPosition.rotation);

        // Le damos al proyectil el daño actual del jugador
        projectile.SetDamage(playerStats.ProjectileDamage);

        projectile.LaunchProjectile(shootPosition.right);

        currentAmmo--;

        UpdateAmmoUI();

        if (currentAmmo <= 0)
        {
            reloadPanel.SetActive(true);
        }

        nextFireTime =
            Time.time + (1f / fireRate);
    }

    private IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;

        UpdateAmmoUI();

        reloadPanel.SetActive(false);

        isReloading = false;
    }

    private void UpdateAmmoUI()
    {
        ammoText.text = currentAmmo + " / " + maxAmmo;
    }

    //========================
    // MEJORAS
    //========================

    public void IncreaseAmmo(int amount)
    {
        maxAmmo += amount;

        currentAmmo = maxAmmo;

        UpdateAmmoUI();
    }

    public void ReduceReloadTime(float amount)
    {
        reloadTime -= amount;

        if (reloadTime < 0.2f)
            reloadTime = 0.2f;
    }

    public void IncreaseFireRate(float amount)
    {
        fireRate += amount;
    }
}