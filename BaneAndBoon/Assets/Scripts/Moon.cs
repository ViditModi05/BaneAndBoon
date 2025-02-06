using UnityEngine;

public class MoonFollow : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Transform player;  
    public Vector2 offset;    
    public float fixedY;

    private void Start()
    {
        player = Player_Manager.instance.player.transform;
    }
    void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(moveSpeed * player.position.x + offset.x, fixedY, transform.position.z);
        }
    }
}
