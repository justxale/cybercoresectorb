using System;
using System.Collections;
using System.Collections.Generic;
using GameData.Player;
using UnityEngine;

public class PuddleController : MonoBehaviour
{
    public string effectId = string.Empty;
    public float lifeTime = 10.5f;

    private SpriteRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame

    void LateUpdate()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;

            if (lifeTime <= 5)
            {
                var tmpColor = renderer.color;
                tmpColor.a -= 0.2f;
                renderer.color = tmpColor;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
