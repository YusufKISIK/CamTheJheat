using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamPhyTable : MonoBehaviour
{
    public TeamData TeamData;
    
    void Start()
    {
        GetComponent<Interactable>().OnInteract += Interact;
    }
    
    private void Interact()
    {
        TeamData.Sabotage();
    }
    
}
