using UnityEngine;

public class WayPointFollower : MonoBehaviour 
{ 
    [SerializeField] private GameObject[] waypoints; 
    private int currentWaypointIndex = 0; 
    [SerializeField] private float speed = 1f; 
    private bool hasReachedEnd = false;

    private void Update() 
    { 
        if (hasReachedEnd) return;

        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position,  
                             transform.position) < 0.1f) 
        { 
            currentWaypointIndex++; 

            if (currentWaypointIndex >= waypoints.Length) 
            { 
                hasReachedEnd = true;
                return;
            } 
        } 

        transform.position = Vector2.MoveTowards(transform.position,  
            waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed); 
    } 
}

