using UnityEngine;

public class AimScreept : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Avake()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }
}
