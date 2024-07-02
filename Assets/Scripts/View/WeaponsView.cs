
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponsView : BaseView
{
    [SerializeField] WeaponItemController[] itemController;
    [SerializeField] Button leftButton = null;
    [SerializeField] Button rightButton = null;
    [SerializeField] Button unlockButton = null;
    [SerializeField] Button equipButton = null;

    private int weaponIndex = 0;
    public override void OnShow()
    {
        WeaponType weaponType = PlayerDataController.GetCurrentWeapon();
        for(int i = 0; i < itemController.Length; i++)
        {
            if(itemController[i].WeaponType == weaponType)
            {
                weaponIndex = i;
                itemController[i].gameObject.SetActive(true);
                itemController[i].OnInit();
                unlockButton.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(false);
            }
            else
            {
                bool isUnlock = PlayerDataController.IsUnlockWeapon(itemController[i].WeaponType);
                if(isUnlock)
                {
                    unlockButton.gameObject.SetActive(false);
                    equipButton.gameObject.SetActive(true);
                }
                else
                {
                    unlockButton.gameObject.SetActive(true);
                    equipButton.gameObject.SetActive(false);
                }
                itemController[i].gameObject.SetActive(false);
            }         
        }
        leftButton.gameObject.SetActive(weaponIndex > 0);
        rightButton.gameObject.SetActive(weaponIndex < itemController.Length - 1);
    }

    public override void OnHide()
    {
        gameObject.SetActive(false);
    }


    public void OnClickStartGameButton()
    {
        SceneManager.LoadScene("Map_1");
        OnHide();
    }

    public void OnClickHomeButton()
    {
        ViewManager.Instance.SetActiveView(ViewType.HomeView);
    }

    public void OnClickUnlockButton()
    {
        for(int i = 0; i < itemController.Length; i++)
        {
            PlayerDataController.UpdateUnlockWeapon(itemController[weaponIndex].WeaponType);
        }
        unlockButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(true);
    }

    public void OnClickEquipButton()
    {
        for (int i = 0; i < itemController.Length; i++)
        {
            PlayerDataController.UpdateCurrentWeapon(itemController[weaponIndex].WeaponType);
        }
        unlockButton.gameObject.SetActive(false);
        equipButton.gameObject.SetActive(false);
    }


    public void OnClickLeftButton()
    {
        for(int i = 0; i < itemController.Length; i++)
        {
            itemController[weaponIndex].gameObject.SetActive(false);
        }
        weaponIndex--;
        for(int i = 0; i < itemController.Length; i++)
        {
            itemController[weaponIndex].gameObject.SetActive(true);
            itemController[weaponIndex].OnInit();
            if(itemController[weaponIndex].WeaponType == PlayerDataController.GetCurrentWeapon())
            {
                unlockButton.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(false);
            }   
            else
            {
                bool isUnlock = PlayerDataController.IsUnlockWeapon(itemController[weaponIndex].WeaponType);
                if (isUnlock)
                {
                    unlockButton.gameObject.SetActive(false);
                    equipButton.gameObject.SetActive(true);
                }
                else
                {
                    unlockButton.gameObject.SetActive(true);
                    equipButton.gameObject.SetActive(false);
                }
            }    
        }
        if(weaponIndex == 0)
        {
            leftButton.gameObject.SetActive(false);
        }
        rightButton.gameObject.SetActive(true);
    }


    public void OnClickRightButton()
    {
        for (int i = 0; i < itemController.Length; i++)
        {
            itemController[weaponIndex].gameObject.SetActive(false);
        }
        weaponIndex++;
        for (int i = 0; i < itemController.Length; i++)
        {
            itemController[weaponIndex].gameObject.SetActive(true);
            itemController[weaponIndex].OnInit();
            if (itemController[weaponIndex].WeaponType == PlayerDataController.GetCurrentWeapon())
            {
                unlockButton.gameObject.SetActive(false);
                equipButton.gameObject.SetActive(false);
            }
            else
            {
                bool isUnlock = PlayerDataController.IsUnlockWeapon(itemController[weaponIndex].WeaponType);
                if (isUnlock)
                {
                    unlockButton.gameObject.SetActive(false);
                    equipButton.gameObject.SetActive(true);
                }
                else
                {
                    unlockButton.gameObject.SetActive(true);
                    equipButton.gameObject.SetActive(false);
                }
            }
        }
        if(weaponIndex == itemController.Length-1)
        {
            rightButton.gameObject.SetActive(false);
        }
        leftButton.gameObject.SetActive(true);
    }

}
