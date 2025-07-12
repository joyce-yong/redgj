using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Rigidbody2D rb;


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

            Collider2D hit = Physics2D.OverlapPoint(mouseWorldPos);
            if (hit != null && hit.gameObject == gameObject)
            {
                isDragging = true;
                offset = transform.position - mouseWorldPos;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            rb.linearVelocity = Vector2.zero;
        }
    }

}
