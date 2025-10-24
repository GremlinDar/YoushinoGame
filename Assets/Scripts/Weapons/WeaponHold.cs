using UnityEngine;

public class WeaponHold : MonoBehaviour
{
    public bool hold;
    private Rigidbody2D rb;
    public float distance = 2f;
    RaycastHit2D hit;
    public Transform holdPoint;
    private GameObject heldObject;
    public float throwObject = 5f; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!hold)
            {
                TryPickup();
            }
            else
            {
                DropObject();
            }
        }

        if (hold && heldObject != null)
        {
            heldObject.transform.position = holdPoint.position;
        }
    }

    void TryPickup()
    {
        Physics2D.queriesStartInColliders = false;
        hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance);
        
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Weapon"))
            {
                hold = true;
                heldObject = hit.collider.gameObject;

                rb = heldObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.simulated = false;
                    rb.linearVelocity = Vector2.zero;
                }
            
                Debug.Log("Поднял объект: " + heldObject.name);
            }
            else
            {
                Debug.Log("Объект нельзя поднять: " + hit.collider.name);
            }
        }
        else
        {
            Debug.Log("Объект не обнаружен");
        }
    }

    void DropObject()
    {
        if (heldObject != null)
        {
            if (rb != null)
            {
                rb.simulated = true;
                // Vector2 throwDirection = new Vector2(transform.localScale.x, 1);
                // rb.linearVelocity = throwDirection * throwObject;
            }

            heldObject = null;
        }

        hold = false;
    }

    private void OnDrawGizmos() {
        Gizmos.color = hold ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * distance);
    }
}