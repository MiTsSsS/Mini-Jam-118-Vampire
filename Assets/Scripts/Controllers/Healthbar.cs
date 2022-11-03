using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
    public Slider slider;

    public void setMaxHpValue(float value) { 
        slider.maxValue = value;
        slider.value = value;
    }

    public void setHp(float value) {
        slider.value = value;
    }
}
