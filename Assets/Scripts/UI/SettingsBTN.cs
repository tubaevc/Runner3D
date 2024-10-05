using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Utils;

public class SettingsBTN : UIBTN
{
    protected override void OnClick()
    {
        MainMenuEvents.SettingsBTN?.Invoke();
    }
}