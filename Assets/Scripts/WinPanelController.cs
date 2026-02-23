using UnityEngine;

public class WinPanelController : MonoBehaviour
{
    [Header("Win Trigger")]
    public GameObject winTrigger; 

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {

            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    private void Update()
    {
        if (winTrigger == null)
        {
            ShowPanel();
        }
    }

    private void ShowPanel()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        enabled = false;
    }
}
