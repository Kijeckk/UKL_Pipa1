using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLevelController : MonoBehaviour
{
    public Button level2;
    public Button level3;
    public Button level1;

    void Start()
    {
        if (!LevelSystem.IsUnlocked(2))
            level2.interactable = false;

        if (!LevelSystem.IsUnlocked(3))
            level3.interactable = false;
    }

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
