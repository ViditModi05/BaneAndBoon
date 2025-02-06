using UnityEngine;

public class Player_Manager : MonoBehaviour
{
    public static Player_Manager instance;

    [Header("Refs")]
    public Player player;

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
}
