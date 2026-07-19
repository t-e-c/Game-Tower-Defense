using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int health = 2;
    [SerializeField] private int points = 10;

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            GameManager.Instance.AddPoints(points);

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Base"))
        {
            Base baseScript = other.GetComponent<Base>();

            if (baseScript != null)
            {
                baseScript.TakeDamage(1);
            }

            Destroy(gameObject);
        }
    }
}