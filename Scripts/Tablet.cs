using TMPro;
using UnityEngine;

public class Tablet : MonoBehaviour
{
    public TextMeshProUGUI timeText;  
    public TextMeshProUGUI MoneyText;
    public TeamTable teamTable;
    private LevelManager _levelManager;
    private float _cycleEndTime;
    private bool _isNight;

    void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _cycleEndTime = _levelManager.CycleStartTime + _levelManager.dayDuration;
        _levelManager.OnNightStart += () =>
        {
            _isNight = true;
            _cycleEndTime = _levelManager.CycleStartTime + _levelManager.nightDuration;
        };
    }

    void Update()
    {
        timeText.text = $"{(_isNight?"Night":"Day")} ends in: {Mathf.Clamp(_cycleEndTime - Time.time, 0f, Mathf.Infinity):00}";
        MoneyText.text = "Money : " +_levelManager.Money.ToString() + "$";
    }

    public void Initialize()
    {
        teamTable.Initialize();
    }
    
    public void UpdateValues()
    {
        teamTable.UpdateDisplay();
    }
}
