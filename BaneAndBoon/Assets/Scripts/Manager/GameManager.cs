using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Credits")]
    [SerializeField] private GameObject creditPanel;

    private void Start()
    {
        if (creditPanel != null)
            creditPanel.SetActive(false); 
    }
    public void StartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void Update()
    {
        if (creditPanel != null && creditPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCreditPanel();
        }
    }

    public void ToggleCreditPanel()
    {
        if (creditPanel != null)
            creditPanel.SetActive(!creditPanel.activeSelf);
    }
}



