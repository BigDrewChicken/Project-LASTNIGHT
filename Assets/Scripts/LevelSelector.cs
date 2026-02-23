using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [Header("Level Settings")]
    public int totalLevels = 5;

    [Header("Level Buttons")]
    public Button[] levelButtons;           

    [Header("Locked Sprite")]
    public Sprite lockedSprite;            


    private string mainMenuScene = "MainMenu";

    private void Start()
    {
        UpdateButtonStates();
    }

    private void UpdateButtonStates()
    {
        for (int i = 0; i < totalLevels; i++)
        {
            int levelNumber = i + 1;
            bool unlocked = LevelProgressManager.instance.IsLevelUnlocked(levelNumber);

  
            if (levelButtons != null && levelButtons.Length > i && levelButtons[i] != null)
            {
                levelButtons[i].interactable = unlocked;

                if (!unlocked && lockedSprite != null)
                {
                    var image = levelButtons[i].GetComponent<Image>();
                    if (image != null)
                        image.sprite = lockedSprite;
                }
            }
        }
    }

    public void LoadLevel(int levelNumber)
    {
        if (levelNumber < 1 || levelNumber > totalLevels) return;

        string sceneName = $"Level {levelNumber}";
        SceneManager.LoadScene(sceneName);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
