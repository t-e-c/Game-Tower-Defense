using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Estadísticas del arma")]
    [SerializeField] private int projectileDamage = 1;
    [SerializeField] private int maxAmmo = 8;
    [SerializeField] private float reloadTime = 3f;
    [SerializeField] private float fireRate = 0.5f;

    public int ProjectileDamage => projectileDamage;
    public int MaxAmmo => maxAmmo;
    public float ReloadTime => reloadTime;
    public float FireRate => fireRate;
}