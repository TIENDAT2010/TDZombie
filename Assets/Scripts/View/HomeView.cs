
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeView : BaseView
{
    public override void OnShow()
    {

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

    public void OnClickWeaponButton()
    {
        ViewManager.Instance.SetActiveView(ViewType.WeaponsView);
    }
}
