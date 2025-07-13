using UnityEngine;
using System.Collections.Generic;

public class Food : MonoBehaviour
{
    private bool isStacked = false;
    private static Transform topOfStack = null;
    private static Transform playerTransform;

    public AudioClip stackSound;
    private AudioSource audioSource;

    private static List<Transform> stackedFoods = new List<Transform>();

    void Start()
    {
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerTransform = playerObj.transform;
        }

        audioSource = GetComponent<AudioSource>();

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
            // Falling movement
            transform.position -= new Vector3(0f, 0.12f, 0f);

            // Wriggle/rotate a little bit as it falls
            transform.Rotate(0f, 0f, Random.Range(-7f, 7f));

            if (transform.position.y < -6f)
            {
                if (Timer.instance != null &&
                   (OguFeverManager.instance == null || !OguFeverManager.instance.IsFeverActive()))
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

        if ((topOfStack == null && collision.CompareTag("Player")) || collision.transform == topOfStack)
        {
            Transform stackTarget = topOfStack == null ? playerTransform : topOfStack;

            StackOn(stackTarget);

            topOfStack = this.transform;
            stackedFoods.Add(this.transform);
        }
    }

    private void StackOn(Transform target)
    {
        isStacked = true;

        if (stackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(stackSound);
        }

        transform.parent = target;

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

        // Reset rotation when stacked
        transform.rotation = Quaternion.identity;
    }

    public static void ResetStack()
    {
        topOfStack = null;
        stackedFoods.Clear();
    }

    public static void RemoveTopStackedFoods(int count)
    {
        for (int i = 0; i < count && stackedFoods.Count > 0; i++)
        {
            Transform top = stackedFoods[stackedFoods.Count - 1];
            stackedFoods.RemoveAt(stackedFoods.Count - 1);
            if (top != null)
            {
                Object.Destroy(top.gameObject);
            }
        }

        if (stackedFoods.Count > 0)
        {
            topOfStack = stackedFoods[stackedFoods.Count - 1];
        }
        else
        {
            topOfStack = null;
        }
    }

    public static Transform GetTopOfStack()
    {
        return topOfStack;
    }
}
