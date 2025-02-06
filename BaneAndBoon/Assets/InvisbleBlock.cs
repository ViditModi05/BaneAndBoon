using UnityEngine;

public class InvisbleBlock : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private GameObject[] block;
    [SerializeField] private GameObject door;


    private void Update()
    {
        if(Player_Manager.instance.player.inShadowState)
        {
            foreach(GameObject item in block)
            {
                item.SetActive(false);
                door.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject item in block)
            {
                item.SetActive(true);
                door.SetActive(false);
            }
        }
    }
}
