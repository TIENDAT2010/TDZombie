using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class IngameView : BaseView
{
    [SerializeField] Image weapon = null;
    [SerializeField] Text healthText = null;
    [SerializeField] Sprite[] weaponImages = null;

    public override void OnShow()
    {
        for (int i = 0; i < weaponImages.Length; i++)
        {
            if(weaponImages[i].name == PlayerDataController.GetCurrentWeapon().ToString()) 
            {
                weapon.sprite = weaponImages[i];
                break;
            }
        }    
    }

    public override void OnHide()
    {
        gameObject.SetActive(false);
    }


    public void UpdateHealth(float current, float total)
    {
        healthText.text = Mathf.RoundToInt(current).ToString() + "/" + Mathf.RoundToInt(total).ToString();
    }
}
