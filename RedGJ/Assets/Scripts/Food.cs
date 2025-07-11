using UnityEngine;

public class Food : MonoBehaviour
{
    private Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();

        GameObject[] floorObjects = GameObject.FindGameObjectsWithTag("Floor");
        Collider2D myCollider = GetComponent<Collider2D>();

        foreach (GameObject floor in floorObjects)
        {
            Collider2D floorCollider = floor.GetComponent<Collider2D>();
            if (floorCollider != null && myCollider != null)
            {
                Physics2D.IgnoreCollision(myCollider, floorCollider);
            }
        }
    }

    void FixedUpdate()
    {
        tr.position -= new Vector3(0f, 0.12f, 0f);

        if (tr.position.y < -6f)
            Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
