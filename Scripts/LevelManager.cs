using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class LevelManager : MonoBehaviour
{
    [Range(0, 600)]
    public int dayDuration;
    [Range(0, 600)]
    public int nightDuration;
    
    public GameObject NightPanel;
    public GameObject NightLights;
    public GameObject DayLights;
    public GameObject WinPanel;
    public GameObject LosePanel;

    public AudioSource AudioSource;
    public AudioClip Gunduz; 
    public AudioClip Gece; 
    
    public int penalty;
    
    public List<TeamData> teams;
    public LevelMeta levelMeta;

    public GameData gameData;
    public int WinMoney;
    public int Money { get; set; }

    public delegate void CycleAction();
    public event CycleAction OnNightStart;
    public event CycleAction OnNightEnd;
    public float CycleStartTime { get; set; }
    
    void Awake()
    {
        Money = gameData.Money;

        foreach (var teamData in teams)
        {
            if (!teamData.IsPlayerTeam )
            {
                teamData.Status.Visibility = TeamStatus.VisibilityValues.Invisible;
            }
            teamData.Status.Failed = false;
            teamData.CurrentScore = 0;
        }
        AudioSource.clip = Gunduz;
        AudioSource.Play();
        CycleStartTime = Time.time;
        StartCoroutine(WaitNight());
        
    }

    IEnumerator WaitNight()
    {
        yield return new WaitForSeconds(dayDuration);
        StartNight();
    }

    IEnumerator WaitDay()
    {
        yield return new WaitForSeconds(nightDuration);
        EndLevel();
    }

    void StartNight()
    {
        NightPanel.SetActive(true);
        NightLights.SetActive(true);
        DayLights.SetActive(false);
        CycleStartTime = Time.time;
        OnNightStart?.Invoke();
        Debug.Log("Night started");
        StartCoroutine(WaitDay());
        AudioSource.Stop();
        AudioSource.clip = Gece;
        AudioSource.Play();
    }

    void EndLevel()
    {
        var playerTeam = teams.Find(t => t.IsPlayerTeam);
        var otherTeams = teams.Where(t => !t.IsPlayerTeam);

        var win = otherTeams.All(otherTeam => otherTeam.Status.Failed || otherTeam.Score < playerTeam.Score);

        
     
        OnNightEnd?.Invoke();

        if (win)
        {
            gameData.Money = Money + WinMoney;
            levelMeta.State = LevelMeta.StateValues.Completed;
            WinPanel.SetActive(true);
            if (gameData.Levels.Length == levelMeta.Index +1)
            {
                StartCoroutine(Wait3Sec(BackToMainMenu));
                return;
            }

            gameData.Levels[levelMeta.Index + 1].State = LevelMeta.StateValues.Available;

            StartCoroutine(Wait3Sec(() => SceneManager.LoadScene(gameData.Levels[levelMeta.Index + 1].Scene)));
        }
        else
        {

            LoseLevel();

        }
        Debug.Log(win);
        Debug.Log("Night ended");
    }


    void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator Wait3Sec(Action f)
    {
        yield return new WaitForSeconds(5);
        f.Invoke();
    }

    public void LoseLevel()
    {
        LosePanel.SetActive(true);
        StartCoroutine(Wait3Sec(BackToMainMenu));
    }
}