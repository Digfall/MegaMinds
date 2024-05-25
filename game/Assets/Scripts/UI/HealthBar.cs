using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Vector3 offset;
    public float maxHealth;

    void Start()
    {
        // Инициализация слайдера при старте
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetHealth(int health)
    {
        float healthFloat = (float)health; // Преобразуем int в float
        slider.gameObject.SetActive(health < maxHealth);
        slider.value = healthFloat;
    }

    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
    }
}
