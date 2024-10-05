using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Utils;

public class SettingExitBTN : UIBTN
{
    protected override void OnClick()
    {
        MainMenuEvents.SettingsExitBTN?.Invoke();
    }
}