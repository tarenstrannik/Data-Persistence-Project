using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBehavior : MonoBehaviour
{
    [SerializeField] private Slider slider; 
    // Start is called before the first frame update
    void Start()
    {
        slider.value = GameManager.Instance.GetSpeedCoef();
    }

    // Update is called once per frame
    public void SetSpeedCoef()
    {
        GameManager.Instance.SetSpeedCoef(slider.value);
    }
}
