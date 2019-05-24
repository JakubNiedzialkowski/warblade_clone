using UnityEngine;
using UnityEngine.SceneManagement;

public class GameNavigationScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject Shop;

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void EnterShop()
    {
        Time.timeScale = 0;
        Shop.SetActive(true);
    }

    public void ExitShop()
    {
        Shop.SetActive(false);
        Time.timeScale = 1;  
    }

    public void UnpauseGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void QuitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenuScene");
    }

}
