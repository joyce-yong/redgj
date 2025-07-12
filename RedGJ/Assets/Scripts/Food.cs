using UnityEngine;

public class Food : MonoBehaviour
{
    private bool isStacked = false;
    private static Transform topOfStack = null; // shared among all food
    private static Transform playerTransform;   // cached player position

    void Start()
    { 
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerTransform = playerObj.transform;
        }


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
        if (!isStacked && Timer.gameStarted)
        {
            transform.position -= new Vector3(0f, 0.12f, 0f);

            if (transform.position.y < -6f)
            {
                if (Timer.instance != null)
                {
                    Timer.instance.DecreaseTime(20f);
                }
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isStacked) return;

        // first item stick only, then the following need to be collide the top stack one to stick
        if ((topOfStack == null && collision.CompareTag("Player")) || collision.transform == topOfStack)
        {
            Transform stackTarget = topOfStack == null ? playerTransform : topOfStack;

            StackOn(stackTarget);

            topOfStack = this.transform;
        }
    }


    private void StackOn(Transform target)
    {
        isStacked = true;

        transform.parent = target;

        // food heightttt
        float myHalfHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2f;
        float targetHalfHeight = target.GetComponent<SpriteRenderer>().bounds.size.y / 2f;

        Vector3 newPos = target.position;
        newPos.y += targetHalfHeight + myHalfHeight;
        transform.position = newPos;


        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.isTrigger = false;
        }
    }

    public static void ResetStack()
    {
        topOfStack = null;
    }
}
