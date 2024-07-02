using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class IngameView : BaseView
{
    public override void OnShow()
    {

    }

    public override void OnHide()
    {
        gameObject.SetActive(false);
    }
}
