using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{

    public GameObject targetUI;

    public TMP_Text dialogueText;
    public TMP_Text selectionText;
    public Button attackButton;
    public Button targetButton;
    public Button nextButton;
    public Button backButton;
    public Button targetAttackButton;

    public void SetupHUD()
    {
        dialogueText.text = "Time to battle!";
        targetUI.SetActive(false);
    }

    public void SetSelection(string targetName) 
    {
        selectionText.text = targetName;
    }

    public void EnableUI(bool isEnabled)
    {
        attackButton.interactable = isEnabled;
        targetButton.interactable = isEnabled;
        nextButton.interactable = isEnabled;
        backButton.interactable = isEnabled;
        targetAttackButton.interactable = isEnabled;
    }
}
