using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }
}
