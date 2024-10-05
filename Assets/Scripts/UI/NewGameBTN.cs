using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Utils;

public class NewGameBTN : UIBTN
{
    protected override void OnClick()
    {
        Debug.Log("NewGame Button clicked");
        MainMenuEvents.NewGameBTN?.Invoke();
    }
}