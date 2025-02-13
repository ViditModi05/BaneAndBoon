using System.Collections.Generic;
using UnityEngine;

public class SpikesHolder : MonoBehaviour
{
    [Header("Refs")]
    private Dictionary<Rigidbody2D, Vector3> spikePositions = new Dictionary<Rigidbody2D, Vector3>();



    private void Start()
    {
        foreach (Rigidbody2D rb in GetComponentsInChildren<Rigidbody2D>())
        {
            spikePositions[rb] = rb.transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (Rigidbody2D spike in GetComponentsInChildren<Rigidbody2D>())
            {
                spike.bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }

    public void ResetSpikes()
    {
        foreach (var entry in spikePositions)
        {
            Rigidbody2D rb = entry.Key;
            rb.transform.position = entry.Value; 
            rb.linearVelocity = Vector2.zero; 
            rb.bodyType = RigidbodyType2D.Kinematic; 
            rb.GetComponent<SpikeTrap>().boolReset(false);
        }
    }
}
