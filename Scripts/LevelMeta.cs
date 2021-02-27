using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "LevelMeta", menuName = "LevelMeta")]
public class LevelMeta : ScriptableObject
{
    public enum StateValues
    {
        Unavailable,
        Available,
        Completed
    }
    public string Name;
    public string Scene;
    public StateValues State;
    public int Index;
}
