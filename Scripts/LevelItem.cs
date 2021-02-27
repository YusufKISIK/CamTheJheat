using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelItem : MonoBehaviour
{
    public TextMeshProUGUI LevelNumberText;
    
    public LevelMeta LevelMeta;
    public Button LevelButton;

    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelMeta.Scene, LoadSceneMode.Single);
    }
    
    void Start()
    {
        if (LevelMeta.State == LevelMeta.StateValues.Unavailable)
        {
            LevelButton.interactable = false;
        }
        LevelNumberText.SetText(LevelMeta.Name);
        var image = GetComponent<Image>();

        switch (LevelMeta.State)
        {
            case LevelMeta.StateValues.Available:
                image.color = new Color(1f, 1f, 0, 0.5f);
                break;
            case LevelMeta.StateValues.Completed:
                image.color = new Color(0, 1f, 0, 0.5f);
                break;
            case LevelMeta.StateValues.Unavailable:
                image.color = new Color(0, 0, 0, 0.5f);
                break;
            
            default:
                image.color = image.color;
                break;
        }
    }
}
