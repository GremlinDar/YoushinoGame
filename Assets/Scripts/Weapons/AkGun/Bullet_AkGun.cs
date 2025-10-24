using UnityEngine;

public class Bullet_AkGun : MonoBehaviour
{

    public float speed = 30f;
    public Rigidbody2D rb;

    public int damage = 10;

    public float lifeTime = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifeTime);

    }

    public void SetDirection(Vector2 direction)
{
    rb.linearVelocity = direction * speed;
}

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
