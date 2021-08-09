using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public float offset;
    public GameObject bullet;
    public float toShootTime = 0.25f;
    public float bulletForce = 25f;
    public int ammoCount;
    public int relAmmoCount;
    public float reloadTime;
    public bool isUnlocked;
    public bool isUnlimitedAmmo;
    public GameObject weaponPrefab;
    public Transform shotPoint;

    public int wDamage;
    public GameObject reloadingIcon;
    public GameObject hudWeaponIcon;

    public Sprite fullAmmo;
    public Sprite nullAmmo;
    public GameObject hudCurAmmo;

    private bool isReloading = false;
    private OctoBot player;
    private Vector3 shotPointOrig;
    public int needAmmo;

    public TMP_Text ammoCountTxt;
    
    private void Start()
    {
        needAmmo = ammoCount;
        player = FindObjectOfType<OctoBot>().GetComponent<OctoBot>();
        shotPointOrig = shotPoint.position;
    }

    private void OnEnable()
    {
        StartCoroutine(Shoot());
        StartCoroutine(Reload());
        hudWeaponIcon.SetActive(true);
    }

    private void OnDisable()
    {
        StopCoroutine(Shoot());
        StopCoroutine(Reload());
        hudWeaponIcon.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isUnlimitedAmmo)
        {
            ammoCountTxt.text = ammoCount + " / " + relAmmoCount;
        }
        else if (isUnlimitedAmmo)
        {
            ammoCountTxt.text = ammoCount + " / ∞";
        }

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        
        if (player.isFlipped)
        {
            transform.rotation = Quaternion.Euler(180f, 180f, rotZ - offset);
        }
        else if (!player.isFlipped)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        }
    }

    /*private void DisplayAmmoIcons()
    {
        var hcrTransform = hudCurAmmo.GetComponent<RectTransform>();

        for (int i = 0; i < hudCurAmmo.transform.childCount; i++)
        {
            if (i > needAmmo)
            {
                hudCurAmmo.transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                hudCurAmmo.transform.GetChild(i).gameObject.SetActive(true);
            }

            var hcrTransformRect = hcrTransform.rect;
            hcrTransformRect.height = 1;
        }
    }

    private void UpdateAmmo()
    {
        
    }*/

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitWhile(() => !Input.GetButton("Fire1") || ammoCount <= 0 || player.isRolling);
            
            var insBullet = Instantiate(bullet, shotPoint.position, transform.rotation);
            insBullet.GetComponent<BulletController>().damage = wDamage;

            if (player.isFlipped)
            {
                insBullet.GetComponent<Rigidbody2D>().AddForce(-transform.right * bulletForce, ForceMode2D.Impulse);
            }
            else
            {
                insBullet.GetComponent<Rigidbody2D>().AddForce(transform.right * bulletForce, ForceMode2D.Impulse);
            }

            Camera.main.GetComponent<FollowingCamera>().StartShake(.15f, .025f);

            ammoCount--;
            Destroy(insBullet, 10f);
            yield return new WaitForSeconds(toShootTime);
        }
    }

    private IEnumerator Reload()
    {
        while (true)
        {
            yield return new WaitWhile(() => !Input.GetKeyDown(KeyCode.R) || ammoCount >= needAmmo || relAmmoCount <= 0);

            print("Start Reload.");
            reloadingIcon.SetActive(true);
            
            yield return new WaitForSeconds(reloadTime);
            if (ammoCount <= 0 || relAmmoCount > 0 && !isUnlimitedAmmo)
            {
                relAmmoCount -= (needAmmo - ammoCount);
                ammoCount = needAmmo;
            }
            else if (ammoCount <= 0 || relAmmoCount > 0 && isUnlimitedAmmo)
            {
                ammoCount = needAmmo;
            }
            
            
            reloadingIcon.SetActive(false);

            print("Stop Reload.");
        }
    }
}
