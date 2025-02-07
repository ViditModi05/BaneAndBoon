using UnityEngine;

public class BlockMovement : MonoBehaviour
{
    [Header("Refs")]
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Player_Manager.instance.player.inShadowState)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
        else if(!Player_Manager.instance.player.inShadowState)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
