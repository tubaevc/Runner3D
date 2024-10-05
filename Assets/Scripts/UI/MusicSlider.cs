using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using Utils;

public class MusicSlider : UISlider
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnValueChanged(float val)
    {
        MainMenuEvents.MusicValueChanged?.Invoke(val);
    }
}
