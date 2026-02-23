using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToLevelSelector : MonoBehaviour
{

    public string levelSelectorScene = "LevelSelector";

    public void BackToLevels()
    {
        SceneManager.LoadScene(levelSelectorScene);
    }
}
