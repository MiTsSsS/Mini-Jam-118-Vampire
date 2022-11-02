using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {
    public Slider slider;

    public void setMaxHpValue(int value) { 
        slider.maxValue = value;
        slider.value = value;
    }

    public void setHp(int value) {
        slider.value = value;
    }
}
