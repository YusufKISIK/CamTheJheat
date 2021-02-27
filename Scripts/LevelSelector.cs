using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public GameObject LevelItemPrefab;
    public GameData GameData;
    
    
    void Start()
    {
        foreach (var levelMeta in GameData.Levels)
        {
            var levelItem = Instantiate(LevelItemPrefab, transform).GetComponent<LevelItem>();
            levelItem.LevelMeta = levelMeta;
        }
    }
}
