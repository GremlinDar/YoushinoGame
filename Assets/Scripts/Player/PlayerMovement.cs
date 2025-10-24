using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    
    private Vector2 moveVelocity;
    private bool facingRight = true;
    private Camera mainCamera;
    
    public bool IsFacingRight { get { return facingRight; } }

    // Добавляем ссылку на контейнер для всех объектов, которые должны поворачиваться
    [SerializeField] private Transform visualsContainer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        
        // Если не назначен вручную, находим автоматически
        if (visualsContainer == null)
        {
            visualsContainer = transform.Find("Visuals");
            if (visualsContainer == null)
            {
                // Создаем контейнер автоматически
                visualsContainer = new GameObject("Visuals").transform;
                visualsContainer.SetParent(transform);
                visualsContainer.localPosition = Vector3.zero;
                
                // Перемещаем все дочерние объекты (кроме камеры) в контейнер
                foreach (Transform child in transform)
                {
                    if (child != visualsContainer && !child.GetComponent<Camera>())
                    {
                        child.SetParent(visualsContainer);
                    }
                }
            }
        }
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        RotateTowardsMouse();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    void RotateTowardsMouse()
    {
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - transform.position).normalized;

        bool shouldFaceRight = direction.x >= 0;

        if (shouldFaceRight != facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        
        // Поворачиваем только контейнер с визуальными элементами, а не всего игрока
        if (visualsContainer != null)
        {
            visualsContainer.Rotate(0f, 180f, 0f);
        }
    }
}