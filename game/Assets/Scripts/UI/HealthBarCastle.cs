using UnityEngine;
using UnityEngine.UI;

public class HealthBarCastle : MonoBehaviour
{
    [SerializeField] private Slider sliderr;
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
        sliderr.value = healthFloat;
        sliderr.maxValue = maxHealth;
    }

    void Update()
    {
        //slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position);
    }
}
