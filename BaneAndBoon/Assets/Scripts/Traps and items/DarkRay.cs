using UnityEngine;

public class DarkRay : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private Transform[] waypoints;

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 2f;
    private int currentWaypointIndex = 0;

    [Header("Flip")]
    [SerializeField] private float flipAngle = 180f;
    private bool movingRight = true;

    void Update()
    {
        if (waypoints.Length == 0) return;

        Vector2 targetPosition = waypoints[currentWaypointIndex].position;
        targetPosition.y = transform.position.y;

        transform.position = Vector2.MoveTowards((Vector2)transform.position, targetPosition, moveSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Loop back to the first waypoint
            Flip();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player_Manager.instance.player.Death();
        }
    }

    private void Flip()
    {
        movingRight = !movingRight;
        float newZRotation = movingRight ? transform.rotation.eulerAngles.z + flipAngle : transform.rotation.eulerAngles.z - flipAngle;
        transform.rotation = Quaternion.Euler(0, 0, newZRotation);
    }
}
