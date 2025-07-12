using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    private Rigidbody2D rb;
    private bool isDragging = false;
    private Vector3 offset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;

            // Check if clicked on self using OverlapPoint
            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);
            if (hit != null && hit.gameObject == gameObject)
            {
                isDragging = true;
                offset = transform.position - mouseWorldPos;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    void FixedUpdate()
    {
        if (isDragging)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f;

            Vector3 targetPosition = mouseWorldPos + offset;
            Vector2 smoothedPosition = Vector2.Lerp(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);

            rb.MovePosition(smoothedPosition);
        }
    }
}
