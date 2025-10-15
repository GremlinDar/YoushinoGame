using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    public float speed = 2f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            transform.position += (Vector3)dir * speed * Time.deltaTime;
        }
    }
}
