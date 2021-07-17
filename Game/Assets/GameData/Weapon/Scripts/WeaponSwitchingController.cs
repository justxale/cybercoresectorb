using GameData.Player;
using UnityEngine;

namespace GameData.Weapon.Scripts
{
    public class WeaponSwitchingController : MonoBehaviour
    {
        public int selectedWeapon = 0;

        [Header("Other Components")] 
        public PlayerStats playerStats;
        public Transform unlockedW;
        public Transform lockedWea;
        private Transform _curWeapon;
    
        // Start is called before the first frame update
        private void Start()
        {
            SelectWeapon();
        }

        // Update is called once per frame
        private void Update()
        {
            _curWeapon = unlockedW.GetChild(selectedWeapon);
            int previousSelWeapon = selectedWeapon;
            
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedWeapon >= unlockedW.childCount - 1)
                {
                    selectedWeapon = 0;
                }
                else
                {
                    selectedWeapon++;
                }
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedWeapon <= 0)
                {
                    selectedWeapon = unlockedW.childCount - 1;
                }
                else
                {
                    selectedWeapon--;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                selectedWeapon = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && unlockedW.childCount >= 2)
            {
                selectedWeapon = 1;
            }

            if (previousSelWeapon != selectedWeapon)
            {
                SelectWeapon();
            }
        }

        public void SelectWeapon()
        {
            int i = 0;
            foreach (Transform weapon in unlockedW)
            {
                if (i == selectedWeapon && weapon.GetComponent<Shooting>().isUnlocked)
                {
                    weapon.gameObject.SetActive(true);
                }
                else
                {
                    weapon.gameObject.SetActive(false);
                }
                i++;
            }
        }

        public void DropWeapon(Shooting weapon)
        {
            if (unlockedW.childCount > 1)
            {
                Vector3 dropPos = new Vector3(transform.position.x, weapon.transform.position.y + 5, 
                    transform.position.z);
                GameObject dropedWeapon = Instantiate(weapon.weaponPrefab, dropPos, Quaternion.identity);
                dropedWeapon.transform.position = dropPos;
                weapon.isUnlocked = false;
                UpdateWeapons();
            }
        }

        public void UpdateWeapons()
        {
            foreach (Transform weapon in lockedWea)
            {
                if (weapon.GetComponent<Shooting>().isUnlocked)
                {
                    weapon.SetParent(unlockedW);
                }
            }
            
            foreach (Transform weapon in unlockedW)
            {
                if (!weapon.GetComponent<Shooting>().isUnlocked)
                {
                    weapon.SetParent(lockedWea);
                    weapon.gameObject.SetActive(false);
                    selectedWeapon = 0;
                    SelectWeapon();
                }
            }
        }

        public void ResetWeapons()
        {
            foreach (Transform weapon in lockedWea)
            {
                weapon.GetComponent<Shooting>().isUnlocked = false;
            }

            if (lockedWea.Find("Plazman") != null)
            {

                UpdateWeapons();

                lockedWea.Find("Plazman").GetComponent<Shooting>().isUnlocked = true;

                UpdateWeapons();
            }

            foreach (var drpWeapon in GameObject.FindGameObjectsWithTag("DropedW"))
            {
                Destroy(drpWeapon);
            }
        }

        public void PlayerDies()
        {
            foreach (Transform weapon in unlockedW)
            {
                DropWeapon(weapon.GetComponent<Shooting>());
            }
            
            UpdateWeapons();
            
            lockedWea.Find("Plazman").GetComponent<Shooting>().isUnlocked = true;
            
            UpdateWeapons();
        }

        public void GiveFreeWeapon()
        {
            if (playerStats.isDead)
            {
                lockedWea.Find("Plazman").GetComponent<Shooting>().isUnlocked = true;
                UpdateWeapons();
            }
            else
            {
                foreach (Transform weapon in unlockedW)
                {
                    weapon.GetComponent<Shooting>().ammoCount = weapon.GetComponent<Shooting>().needAmmo;
                }
            }
        }
    }
}
