using UnityEngine;

public class Bullet_Shotgun : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 5;
    public float lifeTime = 0.9f;

    void Start()
    {
        // Используем текущее направление вращения пули
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifeTime);
    }

    // Убираем метод SetDirection, так как он больше не нужен
    // public void SetDirection(Vector2 direction) ...

    private void OnTriggerEnter2D(Collider2D hitInfo) {
        if (hitInfo.CompareTag("Player") || hitInfo.CompareTag("Weapon"))
        {
            return; 
        }   
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        Destroy(gameObject); 
    }
}