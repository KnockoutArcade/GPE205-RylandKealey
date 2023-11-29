using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPressGoToCredits : MonoBehaviour
{
    public void ChangeToCreditsMenu()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ActivateCreditsScreen();
        }
    }
}
