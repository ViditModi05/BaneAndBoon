using UnityEngine;

public class InvisbleBlock : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private GameObject block;


    private void Update()
    {
        if(Player_Manager.instance.player.inShadowState)
        {
            block.SetActive(false);
            BoxCollider2D boxCollider = block.GetComponent<BoxCollider2D>();
            boxCollider.enabled = false;
        }
        else
        {
            block.SetActive(true);
            BoxCollider2D boxCollider = block.GetComponent<BoxCollider2D>();
            boxCollider.enabled = true;
        }
    }
}
