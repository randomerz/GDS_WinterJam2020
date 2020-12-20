using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider sl;
    // Start is called before the first frame update
    void Start()
    {
        sl.onValueChanged.AddListener(delegate { ValueChanged(); });
    }

    void ValueChanged()
    {
        AudioManager.AudioManager.vol_mult = sl.value;
    }
}
