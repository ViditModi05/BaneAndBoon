using UnityEngine;

public class MovingBlocks : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private float moveSpeed = .1f;
    [SerializeField] private float moveDistance = 5f;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform targetPos;
    private Transform targetPoint;
    private BoxCollider2D boxCollider;
    private SpriteRenderer sr;
    private float moveDirection = -1;
    private bool onBlock;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        targetPoint = targetPos;
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, moveSpeed * Time.deltaTime);

        // If the block reaches the target, switch direction
        if (Vector2.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            moveDirection *= -1;
            targetPoint = (targetPoint == targetPos) ? startPos : targetPos;
        }

        if (onBlock)
        {
            float direction = (targetPoint.position.x > transform.position.x) ? 1 : -1;
            Player_Manager.instance.player.MoveWithBlock(moveSpeed * direction);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision"); 
        if(collision.gameObject.CompareTag("Player"))
        {
            onBlock = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            onBlock = false;
        }
    }
}
