using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;

    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("La base recibió " + damage + " de daño.");
        Debug.Log("Vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }

    }
}