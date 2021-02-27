using UnityEngine;

[CreateAssetMenu(fileName = "TeamData", menuName = "TeamData")]
public class TeamData : ScriptableObject
{
    public string TeamName;
    public float Score;
    public float CurrentScore;
    public TeamStatus Status;
    public bool IsPlayerTeam;

    public string[] Dialogue;
    
    public string GetRandomDialogue()
    {
        return Dialogue[Random.Range(0, Dialogue.Length)];
    }

    public void LearnStatus()
    {
        Status.Visibility = TeamStatus.VisibilityValues.Visible;
    }
    
    public void Sabotage()
    {
        Status.Failed = true;
    }

}
