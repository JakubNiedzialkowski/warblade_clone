using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManuNavigation : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartNewGame()
    {
        Player.ResetData();
        SceneManager.LoadScene("Demo_Scene"); // to be changed with introduction of levels
    }

    public void ContinueGame()
    {
        //SceneManager.LoadScene();
    }
}
