using System.Collections;
using TMPro;
using UnityEngine;

public class TeamMember : MonoBehaviour
{
    public TeamData teamData;
    
    private GameObject _speechBubble;
    
    private TextMeshProUGUI _speechText;

    void Start()
    {
        _speechBubble = transform.GetChild(0).gameObject;
        _speechText = _speechBubble.GetComponentInChildren<TextMeshProUGUI>();
        GetComponent<Interactable>().OnInteract += Interact;
        FindObjectOfType<LevelManager>().OnNightStart += OnNightStart;
    }

    private void OnNightStart()
    {
        gameObject.SetActive(false);
    }
    
    private void Interact()
    {
        _speechText.SetText(teamData.GetRandomDialogue());
        _speechBubble.SetActive(true);
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (teamData.Status.Visibility == TeamStatus.VisibilityValues.Visible)
        {
            _speechBubble.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(1f);
        _speechBubble.SetActive(false);
    }
}