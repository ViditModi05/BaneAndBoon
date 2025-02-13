using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [Header("Settings")]
    private bool hasHitPlayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player_Manager.instance.player.Death();
            hasHitPlayer = true;
            SpikesHolder spikesHolder = GetComponentInParent<SpikesHolder>();   
            spikesHolder.ResetSpikes();
        }
        else if(CompareTag("FallingSpikes") && collision.CompareTag("Ground") && !hasHitPlayer)
        {
            Destroy(gameObject);
        }
    }

    public bool boolReset(bool _reset) => hasHitPlayer = _reset; 
}
