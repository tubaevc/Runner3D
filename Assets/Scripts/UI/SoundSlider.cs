using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Utils;

public class SoundSlider : UISlider
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnValueChanged(float val)
    {
        MainMenuEvents.SoundValueChanged?.Invoke(val);
    }
}