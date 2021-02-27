using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeamUiRow : MonoBehaviour
{
    public TextMeshProUGUI teamNameText;
    public TextMeshProUGUI teamScoreText;
    private Scrollbar _scrollbar;
    public TeamData teamData;

    private Image _image;
    
    private void Awake()
    {
        _scrollbar = GetComponentInChildren<Scrollbar>(true);
        _image = _scrollbar.handleRect.GetComponent<Image>();
    }

    public void UpdateDisplay()
    {
        teamNameText.SetText(teamData.TeamName);

        if (teamData.Status.Visibility == TeamStatus.VisibilityValues.Visible)
        {
            teamScoreText.SetText(teamData.Score.ToString(CultureInfo.CurrentCulture));
            _scrollbar.size = teamData.Score / 100f;
        }
        else
        {
            _scrollbar.size = 0f;
            teamScoreText.SetText("???");
        }
        if (teamData.Status.Failed)
        {
            _image.color = Color.red;
            _scrollbar.size = 1;
        }
    }
}
