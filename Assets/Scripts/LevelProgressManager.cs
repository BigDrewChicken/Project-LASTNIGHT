using UnityEngine;

public class LevelProgressManager : MonoBehaviour
{
    public static LevelProgressManager instance;

    private int totalLevels = 5;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        if (!PlayerPrefs.HasKey("Level1Unlocked"))
            PlayerPrefs.SetInt("Level1Unlocked", 1); 
    }


    public bool IsLevelUnlocked(int levelNumber)
    {
        return PlayerPrefs.GetInt("Level" + levelNumber + "Unlocked", 0) == 1;
    }


    public void UnlockLevel(int levelNumber)
    {
        if (levelNumber < 1 || levelNumber > totalLevels) return;
        PlayerPrefs.SetInt("Level" + levelNumber + "Unlocked", 1);
        PlayerPrefs.Save();
    }
}
