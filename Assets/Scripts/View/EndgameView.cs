
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndgameView : BaseView
{
    public override void OnShow()
    {

    }

    public override void OnHide()
    {
        gameObject.SetActive(false);
    }
}
