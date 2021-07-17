using System;
using System.Collections;
using System.Collections.Generic;
using GameData.Weapon.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace GameData.Player
{
    public class PlayerStats : MonoBehaviour
    {
        [Header("Player Specifies")]
        public int health = 6;
        public int curHeartsCount = 3;
        public int singCount = 2;
        public int armorLevel = 0;
        public int money = 0;
        public bool isDead = false;

        public Transform healthHud;
        public Sprite fullHeart;
        public Sprite halfHeart;
        public Sprite nullHeart;
        public Image[] hearts;

        public Transform singHud;
        public float singCooldown;

        public List<string> effectsDatabase;

        [Header("Other Components")] 
        public WeaponSwitchingController weaponSwitchingController;
        public GameObject deadPanel;

        // Start is called before the first frame update
        private void Start()
        {
            StartCoroutine(UseSingularity());
            deadPanel.SetActive(false);
        }

        // Update is called once per frame
        private void Update()
        {
            if (health <= 0)
            {
                PlayerDie();
            }

            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < curHeartsCount)
                {
                    hearts[i].enabled = true;
                }
                else
                {
                    hearts[i].enabled = false;
                }
            }

            UpdateHearts();
            UpdateSingularity();
        }

        private void PlayerDie()
        {
            isDead = true;
            weaponSwitchingController.PlayerDies();
            deadPanel.SetActive(true);
        }

        public void PlayerAwakes()
        {
            gameObject.SetActive(true);
            health = 100;
            weaponSwitchingController.GiveFreeWeapon();
        }

        public void AddEffect(string effectName)
        {
            if (effectsDatabase.Contains(effectName))
            {
                switch (effectName)
                {
                    case "player_burning":
                        StartCoroutine(BurningEffect(1.25f));
                        break;
                    case "player_explosion":
                        StartCoroutine(ExplosionEffect(0.15f));
                        break;
                    case "player_poisoning":
                        StartCoroutine(PoisoningEffect(0.75f));
                        break;
                }
            }
        }

        public IEnumerator BurningEffect(float delay)
        {
            var origDelay = delay;
            while (delay > origDelay)
            {
                health -= 2;
                
                delay -= Time.deltaTime;
                yield return null;
            }
        }
        public IEnumerator ExplosionEffect(float delay)
        {
            var origDelay = delay;
            while (delay > origDelay)
            {
                health -= 3;
                delay -= Time.deltaTime;
                yield return null;
            }
        }
        public IEnumerator PoisoningEffect(float delay)
        {
            var origDelay = delay;
            while (delay > origDelay)
            {
                health -= 4;
                delay -= Time.deltaTime;
                yield return null;
            }
        }
        
        private void UpdateHearts()
        {
            switch (health)
            {
                case 20:
                    GetHeartImg(9).sprite = fullHeart;
                    GetHeartImg(8).sprite = fullHeart;
                    GetHeartImg(7).sprite = fullHeart;
                    GetHeartImg(6).sprite = fullHeart;
                    GetHeartImg(5).sprite = fullHeart;
                    GetHeartImg(4).sprite = fullHeart;
                    GetHeartImg(3).sprite = fullHeart;
                    GetHeartImg(2).sprite = fullHeart;
                    GetHeartImg(1).sprite = fullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 19:
                    GetHeartImg(9).sprite = halfHeart;
                    GetHeartImg(8).sprite = fullHeart;
                    GetHeartImg(7).sprite = fullHeart;
                    GetHeartImg(6).sprite = fullHeart;
                    GetHeartImg(5).sprite = fullHeart;
                    GetHeartImg(4).sprite = fullHeart;
                    GetHeartImg(3).sprite = fullHeart;
                    GetHeartImg(2).sprite = fullHeart;
                    GetHeartImg(1).sprite = fullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 18:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = fullHeart;
                    GetHeartImg(7).sprite = fullHeart;
                    GetHeartImg(6).sprite = fullHeart;
                    GetHeartImg(5).sprite = fullHeart;
                    GetHeartImg(4).sprite = fullHeart;
                    GetHeartImg(3).sprite = fullHeart;
                    GetHeartImg(2).sprite = fullHeart;
                    GetHeartImg(1).sprite = fullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 17:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = halfHeart;
                    GetHeartImg(7).sprite = fullHeart;
                    GetHeartImg(6).sprite = fullHeart;
                    GetHeartImg(5).sprite = fullHeart;
                    GetHeartImg(4).sprite = fullHeart;
                    GetHeartImg(3).sprite = fullHeart;
                    GetHeartImg(2).sprite = fullHeart;
                    GetHeartImg(1).sprite = fullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 16:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = nullHeart;
                    GetHeartImg(7).sprite = fullHeart;
                    GetHeartImg(6).sprite = fullHeart;
                    GetHeartImg(5).sprite = fullHeart;
                    GetHeartImg(4).sprite = fullHeart;
                    GetHeartImg(3).sprite = fullHeart;
                    GetHeartImg(2).sprite = fullHeart;
                    GetHeartImg(1).sprite = fullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 15:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = nullHeart;
                    GetHeartImg(7).sprite = halfHeart;
                    GetHeartImg(6).sprite = fullHeart;
                    GetHeartImg(5).sprite = fullHeart;
                    GetHeartImg(4).sprite = fullHeart;
                    GetHeartImg(3).sprite = fullHeart;
                    GetHeartImg(2).sprite = fullHeart;
                    GetHeartImg(1).sprite = fullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 14:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = nullHeart;
                    GetHeartImg(7).sprite = nullHeart;
                    GetHeartImg(6).sprite = fullHeart;
                    GetHeartImg(5).sprite = fullHeart;
                    GetHeartImg(4).sprite = fullHeart;
                    GetHeartImg(3).sprite = fullHeart;
                    GetHeartImg(2).sprite = fullHeart;
                    GetHeartImg(1).sprite = fullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 13:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = nullHeart;
                    GetHeartImg(7).sprite = nullHeart;
                    GetHeartImg(6).sprite = halfHeart;
                    GetHeartImg(5).sprite = fullHeart;
                    GetHeartImg(4).sprite = fullHeart;
                    GetHeartImg(3).sprite = fullHeart;
                    GetHeartImg(2).sprite = fullHeart;
                    GetHeartImg(1).sprite = fullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 12:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = nullHeart;
                    GetHeartImg(7).sprite = nullHeart;
                    GetHeartImg(6).sprite = nullHeart;
                    GetHeartImg(5).sprite = fullHeart;
                    GetHeartImg(4).sprite = fullHeart;
                    GetHeartImg(3).sprite = fullHeart;
                    GetHeartImg(2).sprite = fullHeart;
                    GetHeartImg(1).sprite = fullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 11:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = nullHeart;
                    GetHeartImg(7).sprite = nullHeart;
                    GetHeartImg(6).sprite = nullHeart;
                    GetHeartImg(5).sprite = halfHeart;
                    GetHeartImg(4).sprite = fullHeart;
                    GetHeartImg(3).sprite = fullHeart;
                    GetHeartImg(2).sprite = fullHeart;
                    GetHeartImg(1).sprite = fullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 10:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = nullHeart;
                    GetHeartImg(7).sprite = nullHeart;
                    GetHeartImg(6).sprite = nullHeart;
                    GetHeartImg(5).sprite = nullHeart;
                    GetHeartImg(4).sprite = fullHeart;
                    GetHeartImg(3).sprite = fullHeart;
                    GetHeartImg(2).sprite = fullHeart;
                    GetHeartImg(1).sprite = fullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 9:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = nullHeart;
                    GetHeartImg(7).sprite = nullHeart;
                    GetHeartImg(6).sprite = nullHeart;
                    GetHeartImg(5).sprite = nullHeart;
                    GetHeartImg(4).sprite = halfHeart;
                    GetHeartImg(3).sprite = fullHeart;
                    GetHeartImg(2).sprite = fullHeart;
                    GetHeartImg(1).sprite = fullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 8:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = nullHeart;
                    GetHeartImg(7).sprite = nullHeart;
                    GetHeartImg(6).sprite = nullHeart;
                    GetHeartImg(5).sprite = nullHeart;
                    GetHeartImg(4).sprite = nullHeart;
                    GetHeartImg(3).sprite = fullHeart;
                    GetHeartImg(2).sprite = fullHeart;
                    GetHeartImg(1).sprite = fullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 7:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = nullHeart;
                    GetHeartImg(7).sprite = nullHeart;
                    GetHeartImg(6).sprite = nullHeart;
                    GetHeartImg(5).sprite = nullHeart;
                    GetHeartImg(4).sprite = nullHeart;
                    GetHeartImg(3).sprite = halfHeart;
                    GetHeartImg(2).sprite = fullHeart;
                    GetHeartImg(1).sprite = fullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 6:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = nullHeart;
                    GetHeartImg(7).sprite = nullHeart;
                    GetHeartImg(6).sprite = nullHeart;
                    GetHeartImg(5).sprite = nullHeart;
                    GetHeartImg(4).sprite = nullHeart;
                    GetHeartImg(3).sprite = nullHeart;
                    GetHeartImg(2).sprite = fullHeart;
                    GetHeartImg(1).sprite = fullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 5:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = nullHeart;
                    GetHeartImg(7).sprite = nullHeart;
                    GetHeartImg(6).sprite = nullHeart;
                    GetHeartImg(5).sprite = nullHeart;
                    GetHeartImg(4).sprite = nullHeart;
                    GetHeartImg(3).sprite = nullHeart;
                    GetHeartImg(2).sprite = halfHeart;
                    GetHeartImg(1).sprite = fullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 4:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = nullHeart;
                    GetHeartImg(7).sprite = nullHeart;
                    GetHeartImg(6).sprite = nullHeart;
                    GetHeartImg(5).sprite = nullHeart;
                    GetHeartImg(4).sprite = nullHeart;
                    GetHeartImg(3).sprite = nullHeart;
                    GetHeartImg(2).sprite = nullHeart;
                    GetHeartImg(1).sprite = fullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 3:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = nullHeart;
                    GetHeartImg(7).sprite = nullHeart;
                    GetHeartImg(6).sprite = nullHeart;
                    GetHeartImg(5).sprite = nullHeart;
                    GetHeartImg(4).sprite = nullHeart;
                    GetHeartImg(3).sprite = nullHeart;
                    GetHeartImg(2).sprite = nullHeart;
                    GetHeartImg(1).sprite = halfHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 2:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = nullHeart;
                    GetHeartImg(7).sprite = nullHeart;
                    GetHeartImg(6).sprite = nullHeart;
                    GetHeartImg(5).sprite = nullHeart;
                    GetHeartImg(4).sprite = nullHeart;
                    GetHeartImg(3).sprite = nullHeart;
                    GetHeartImg(2).sprite = nullHeart;
                    GetHeartImg(1).sprite = nullHeart;
                    GetHeartImg(0).sprite = fullHeart;
                    break;
                case 1:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = nullHeart;
                    GetHeartImg(7).sprite = nullHeart;
                    GetHeartImg(6).sprite = nullHeart;
                    GetHeartImg(5).sprite = nullHeart;
                    GetHeartImg(4).sprite = nullHeart;
                    GetHeartImg(3).sprite = nullHeart;
                    GetHeartImg(2).sprite = nullHeart;
                    GetHeartImg(1).sprite = nullHeart;
                    GetHeartImg(0).sprite = halfHeart;
                    break;
                case 0:
                    GetHeartImg(9).sprite = nullHeart;
                    GetHeartImg(8).sprite = nullHeart;
                    GetHeartImg(7).sprite = nullHeart;
                    GetHeartImg(6).sprite = nullHeart;
                    GetHeartImg(5).sprite = nullHeart;
                    GetHeartImg(4).sprite = nullHeart;
                    GetHeartImg(3).sprite = nullHeart;
                    GetHeartImg(2).sprite = nullHeart;
                    GetHeartImg(1).sprite = nullHeart;
                    GetHeartImg(0).sprite = nullHeart;
                    break;
                
            }
        }

        private void UpdateSingularity()
        {
            for (int i = 0; i < singHud.childCount; i++)
            {
                if (i+1 > singCount)
                {
                    singHud.GetChild(i).GetComponent<Image>().enabled = false;
                }
                else if (i+1 <= singCount)
                {
                    singHud.GetChild(i).GetComponent<Image>().enabled = true;
                }
            }
        }

        private IEnumerator UseSingularity()
        {
            while (true)
            {
                yield return new WaitWhile((() => !Input.GetKeyDown(KeyCode.Q)));
                if (singCount > 0)
                {
                    var sceneBullets = GameObject.FindGameObjectsWithTag("enemyBullet");
                    foreach (var bullet in sceneBullets)
                    {
                        print("Destroyer!");
                        Destroy(bullet);
                    }

                    singCount--;
                }
                yield return new WaitForSeconds(singCooldown);
            }
        }

        public void AddSingularity(int sCount)
        {
            singCount += sCount;
        }

        public void AddHeart(int hCount)
        {
            curHeartsCount += hCount;
        }

        private Image GetHeartImg(int heartID)
        {
            return healthHud.GetChild(heartID).GetComponent<Image>();
        }
        
        public void OnTriggerEnter2D(Collider2D other)
        {
            var puddleC = other.GetComponent<PuddleController>();

            if (puddleC != null)
            {
                var lifeTime = puddleC.lifeTime;
                var effectId = puddleC.effectId;
                if (lifeTime > 2.5f)
                {
                    if (effectId != String.Empty)
                    {
                        AddEffect(effectId);
                    }
                }
            }
        }
    }
}
