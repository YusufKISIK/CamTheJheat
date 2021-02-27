using System.Collections.Generic;
using UnityEngine;

public class TeamTable : MonoBehaviour
{
    public GameObject teamRowPrefab;
    
    private LevelManager _levelManager;
    private List<TeamUiRow> _teamUiRows;
    
    public void Initialize()
    {
        _levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        _teamUiRows = new List<TeamUiRow>();
        foreach (var teamData in _levelManager.teams)
        {
            var teamUiRow = Instantiate(teamRowPrefab, transform).GetComponent<TeamUiRow>();
            teamUiRow.teamData = teamData;
            _teamUiRows.Add(teamUiRow);
        }
    }

    public void UpdateDisplay()
    {
        foreach (var teamUiRow in _teamUiRows)
        {
            teamUiRow.UpdateDisplay();
        }
    }
}
