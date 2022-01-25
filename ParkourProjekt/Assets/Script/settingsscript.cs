using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingsscript : MonoBehaviour
{

    public Slider MouseSensSlider;

    // Start is called before the first frame update
    void Start()
    {
        MouseSensSlider.value = LookScript.MouseSens;
    }

    // Update is called once per frame
    void Update()
    {

        LookScript.MouseSens = MouseSensSlider.value;
    }
}
