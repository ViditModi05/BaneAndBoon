using UnityEngine;

public class Checkpoint_Manager : MonoBehaviour
{
    public static Checkpoint_Manager instance;
    [Header("Checkpoints")]
    private Vector2 lastCheckpoint;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void SetCheckpoint(Vector2 position)
    {
        lastCheckpoint = position;
    }

    public Vector2 GetCheckpoint()
    {
        return lastCheckpoint;
    }
}
