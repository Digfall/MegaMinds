using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Vector3 offset;
    public float maxHealth;


    public void SetHealth(int health)
    {
        float healthFloat = (float)health; // Преобразуем int в float
        slider.gameObject.SetActive(health < maxHealth);
        slider.value = healthFloat;
        slider.maxValue = maxHealth;
    }


    // Update is called once per frame
    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
