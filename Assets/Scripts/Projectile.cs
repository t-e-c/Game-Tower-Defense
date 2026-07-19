using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;

    private float destroyDelay = 2f;
    private Rigidbody2D projectileRb;

    private int damage;

    private void Awake()
    {
        projectileRb = GetComponent<Rigidbody2D>();
    }

    public void LaunchProjectile(Vector2 direction)
    {
        projectileRb.linearVelocity = direction.normalized * speed;

        Destroy(gameObject, destroyDelay);
    }

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}