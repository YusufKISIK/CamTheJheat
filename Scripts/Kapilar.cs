using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kapilar : MonoBehaviour
{
    
    public GameObject Portal;
    public GameObject Player;

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            Teleport();
            
        }
    }
    void Teleport()
    {
        Player.transform.position = new Vector2 (Portal.transform.position.x, Portal.transform.position.y);
    }
    
}
