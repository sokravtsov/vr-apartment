using UnityEngine;
using System.Collections;

/// <summary>
/// The Teleporter class moves the player between a predetermined set of waypoints whenever the Cardboard button is pressed (or the screen touched).
/// </summary>
public class Teleporter : MonoBehaviour
{
	[Tooltip ("How tall is the player, in meters?")]
	public float height = 1.75f;
	[Tooltip ("How fast to move to new location?")]
	public float speed = 10.0f;

	private Transform[] waypoints;
	// Cached transforms for all waypoints
	private int currentWaypointIndex = -1;
	// Which waypoint is active?
	private bool shouldMoveToWaypoint = false;
	// Only move to the first waypoint after the player pressed the cardboard button (or touched the screen)

	private GameObject cameraRig;
	// Reference to the camera rig representing the player

	void Start ()
	{
		// Assigne the camera rig reference
		cameraRig = Camera.main.transform.parent.gameObject;

		// Initialize the waypoints
		waypoints = new Transform[transform.childCount];
		for (int i = 0; i < transform.childCount; i++) {
			waypoints [i] = transform.GetChild (i);
		}
	}

	void Update ()
	{
		// If the player pressed the cardboard button (or touched the screen), then go to the next waypoint
		if (Input.GetMouseButtonDown (0)) {
			if (!shouldMoveToWaypoint) {
				shouldMoveToWaypoint = true;
			}
			currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
		}

		// Smoothly move the player towards the active waypoint
		if (shouldMoveToWaypoint) {
			Vector3 destPos = waypoints [currentWaypointIndex].transform.position + Vector3.up * height;
			cameraRig.transform.position = Vector3.Lerp (cameraRig.transform.position, destPos, Time.deltaTime * speed);
		}
	}
}
