using UnityEngine;

public class MovingBlocks : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private float moveSpeed = .1f;
    private BoxCollider2D boxCollider;
    private SpriteRenderer sr;
    private float moveDirection = -1;
    private bool onBlock;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();    
    }

    private void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime,0,0);

        if(Player_Manager.instance.player.inShadowState)
        {
            Color color = sr.color;
            color.a = 0;
        }
        else
        {
            Color color = sr.color;
            color.a = 100;
        }
        if(onBlock)
        {
            Player_Manager.instance.player.MoveWithBlock(moveDirection * moveSpeed);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if(collision.gameObject.CompareTag("Ground"))
        {
            moveDirection *= -1;
        }
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
