using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Checkpoint_Manager.instance.SetCheckpoint(transform.position);
        }
    }
}
