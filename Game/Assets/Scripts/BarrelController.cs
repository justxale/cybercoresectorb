using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BarrelController : MonoBehaviour
{
    public GameObject puddlePrefab;
    public BarrelTypes barrelDropdown;

    public void Shooted()
    {
        if (barrelDropdown == BarrelTypes.ExplosionBarrel)
        {
            Explode();
        }
        else if (barrelDropdown == BarrelTypes.PoisonBarrel)
        {
            SpawnPuddle("player_poisoning");
        }
        else if (barrelDropdown == BarrelTypes.WaterBarrel)
        {
            SpawnPuddle(string.Empty);
        }
    }

    private void SpawnPuddle(string effectID)
    {
        var newPuddle = Instantiate(puddlePrefab, transform.position, new Quaternion(0,0,Random.Range(0,360),0));
        newPuddle.GetComponent<PuddleController>().effectId = effectID;
        Destroy(gameObject);
    }

    public void Explode()
    {
        transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }
}

public enum BarrelTypes
{
    ExplosionBarrel,
    PoisonBarrel,
    WaterBarrel,
}
