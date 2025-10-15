using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 2;

    public void TakeDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

}
