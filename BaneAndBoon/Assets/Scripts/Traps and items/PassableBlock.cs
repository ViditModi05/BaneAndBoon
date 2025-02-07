using UnityEngine;

public class PassableBlock : MonoBehaviour
{
    [Header("Refs")]
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(Player_Manager.instance.player.inShadowState)
        {
            boxCollider.enabled = false;
            spriteRenderer.color = Color.black;

        }
        if(!Player_Manager.instance.player.inShadowState)
        {
            boxCollider.enabled = true;
            spriteRenderer.color = Color.white;
        }
    }
}
