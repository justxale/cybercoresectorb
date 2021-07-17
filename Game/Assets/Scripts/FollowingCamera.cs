using System;
using System.Collections;
using System.Collections.Generic;
using GameData.Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class FollowingCamera : MonoBehaviour
{
    [Header("Camera Shake Params")]
    public float rotationMultiplier = 15f;
    public static FollowingCamera instance;

    [Header("Camera Follow Params")] 
    public Vector3 offset;
    public float smoothSpeed = 0.125f;
    
    
    private Transform target;
    private float shakeTimeRemaining, shakePower, shakeFadeTime, shakeRotation;
    
    
    // Start is called before the first frame update
    private void Start()
    {
        target = FindObjectOfType<PlayerStats>().transform;
        instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        target = FindObjectOfType<PlayerStats>().transform;
    }

    private void LateUpdate()
    {
        if (target != null)
        {
            if (shakeTimeRemaining > 0)
            {
                shakeTimeRemaining -= Time.deltaTime;

                float xAmount = Random.Range(-1f, 1f) * shakePower;
                float yAmount = Random.Range(-1f, 1f) * shakePower;

                transform.position += new Vector3(xAmount, yAmount, 0f);

                shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * rotationMultiplier
                                                                             * Time.deltaTime);
            }

            transform.rotation = Quaternion.Euler(0f, 0f, shakeRotation * Random.Range(-1f, 1f));
        }
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, -1f);
            Vector3 myPos = new Vector3(transform.position.x, transform.position.y, -1f);

            Vector3 desiredPos = targetPos + offset;
            Vector3 smoothedPosition = Vector3.Lerp(myPos, desiredPos, smoothSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }

    public void StartShake(float length, float power)
    {
        shakeTimeRemaining = length;
        shakePower = power;

        shakeFadeTime = power / length;

        shakeRotation = power * rotationMultiplier;
    }
}
