using UnityEngine;
using UnityEngine.UI;

public class HealthBarCastle : MonoBehaviour
{
    public Slider slider;
    //public Vector3 offset;
    public float maxHealth;

    // Устанавливаем начальное значение слайдера равным максимальному здоровью
    void Start()
    {
        SetHealth((int)maxHealth);
    }

    public void SetHealth(int health)
    {
        float healthFloat = (float)health;
        slider.value = healthFloat;
        slider.maxValue = maxHealth;
    }

    void Update()
    {
        //slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position);
    }
}
