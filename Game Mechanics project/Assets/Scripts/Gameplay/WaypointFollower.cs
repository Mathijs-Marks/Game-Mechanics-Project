using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private Lever lever;
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    private bool isSwitched;

    [SerializeField] private float speed = 2f;

    private void OnEnable()
    {
        lever.leverSwitched.AddListener(OnSwitch);
    }

    private void Start()
    {
        isSwitched = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isSwitched)
        {
        FollowWaypoint();
        }
    }

    private void FollowWaypoint()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < .1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }

    private void OnSwitch()
    {
        isSwitched = true;
    }
}
