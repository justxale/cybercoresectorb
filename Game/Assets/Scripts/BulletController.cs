using System;
using System.Collections;
using System.Collections.Generic;
using GameData.Player;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage;
    
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if (other.collider.GetComponent<PlayerStats>() != null)
        {
            other.collider.GetComponent<PlayerStats>().health -= damage;
        }
        else if (other.collider.GetComponent<BarrelController>())
        {
            other.collider.GetComponent<BarrelController>().Shooted();
        }
    }
}
