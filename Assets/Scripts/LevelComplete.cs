using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public GameObject finishPopup;
    public int nextLevel;

    public void ShowFinish()
    {
        Debug.Log("POPUP DIPANGGIL!!!");

        finishPopup.SetActive(true);
        Time.timeScale = 0f;
        LevelSystem.UnlockLevel(nextLevel);
    }


    public void Next()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level" + nextLevel);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SelectLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
    }
}
