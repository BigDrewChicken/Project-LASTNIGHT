using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelButton : MonoBehaviour
{
    public void LoadNextLevel()
    {
        int currentLevel = GetCurrentLevelNumber();
        int nextLevel = currentLevel + 1;

        if (nextLevel > 5) return; 

        SceneManager.LoadScene("Level " + nextLevel);

        MusicManager.instance.PlayGameplayMusic(true);
    }

    private int GetCurrentLevelNumber()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        string number = sceneName.Replace("Level ", "");
        int.TryParse(number, out int levelNum);
        return levelNum;
    }
}
