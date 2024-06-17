using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RectTransform buttonTransform;
    [SerializeField] private float moveAmount = 10f; // Расстояние, на которое кнопка будет двигаться вверх
    [SerializeField] private float moveSpeed = 4.5f; // Скорость движения кнопки
    private Vector2 originalPos;
    private bool isHovering = false;

    private void Start()
    {
        originalPos = buttonTransform.anchoredPosition;
    }

    private void Update()
    {
        if (isHovering)
        {
            Vector2 targetPos = originalPos + new Vector2(0, moveAmount);
            buttonTransform.anchoredPosition = Vector2.Lerp(buttonTransform.anchoredPosition, targetPos, moveSpeed * Time.deltaTime);
        }
        else
        {
            buttonTransform.anchoredPosition = Vector2.Lerp(buttonTransform.anchoredPosition, originalPos, moveSpeed * Time.deltaTime);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
    }
}
