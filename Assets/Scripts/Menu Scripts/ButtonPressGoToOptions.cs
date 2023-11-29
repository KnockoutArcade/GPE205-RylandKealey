using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressGoToOptions : MonoBehaviour
{
    public void ChangeToOptionsMenu()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateOptionsScreen();
        }
    }
}
