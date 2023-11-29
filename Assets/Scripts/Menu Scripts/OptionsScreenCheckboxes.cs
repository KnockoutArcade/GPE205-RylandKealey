using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScreenCheckboxes : MonoBehaviour
{
    public Toggle multiplayerCheckbox;
    public Toggle mapOfTheDayCheckbox;

    public void SetMultiplayerStatus()
    {
        GameManager.instance.enableMultiplayer = multiplayerCheckbox.isOn;
    }
}
