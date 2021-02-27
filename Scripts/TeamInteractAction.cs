using UnityEngine;

public class TeamInteractAction : MonoBehaviour
{
    public TeamData teamData;
    public bool learnProgressOnInteract;
    public bool failTeamOnInteract;
    public int scoreReduceAmount;

    private Interactable _interactable;
    private Player _player;
    
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _interactable = GetComponent<Interactable>();

        _interactable.OnInteract += OnInteract;
    }

    private void OnInteract()
    {
        if (learnProgressOnInteract)
            teamData.Status.Visibility = TeamStatus.VisibilityValues.Visible;

        if (failTeamOnInteract)
        {
            teamData.Status.Failed = true;
            _player.StartSabotage();
        }
        else
            teamData.CurrentScore = Mathf.Max(0, teamData.CurrentScore - scoreReduceAmount);
        
    }
}
