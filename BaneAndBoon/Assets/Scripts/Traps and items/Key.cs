using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player_Manager.instance.player.hasKey = true;   
            Destroy(gameObject);
        }
    }
}
