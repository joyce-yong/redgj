using UnityEngine;

public class skillTriggerPointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float waitDurationAtSecondPoint = 5f;

    private int currentWaypointIndex = 0;
    private bool hasReachedEnd = false;
    private bool isWaiting = false;
    private float waitTimer = 0f;

    private void Update()
    {
        if (hasReachedEnd || isWaiting) return;

        if (Vector2.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < 0.1f)
        {
    
            if (currentWaypointIndex == 1)
            {
                isWaiting = true;
                waitTimer = 0f;
                return;
            }

            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length)
            {
                hasReachedEnd = true;
                return;
            }
        }

        transform.position = Vector2.MoveTowards(
            transform.position,
            waypoints[currentWaypointIndex].transform.position,
            Time.deltaTime * speed
        );
    }

    private void LateUpdate()
    {
        if (isWaiting)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitDurationAtSecondPoint)
            {
                isWaiting = false;
                currentWaypointIndex++;
            }
        }
    }
}
