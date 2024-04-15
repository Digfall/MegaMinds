using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform buttonTransform; // Ссылка на RectTransform кнопки
    public float moveAmount = 50f; // Расстояние, на которое кнопка будет двигаться вверх
    private Vector3 originalPos;

    private void Start()
    {
        originalPos = buttonTransform.position;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Вызывается при наведении мыши на кнопку
        Vector3 targetPos = originalPos + (Vector3.up * moveAmount); // Двигаем кнопку вверх
        MoveButton(targetPos);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Вызывается при уходе мыши с кнопки
        MoveButton(originalPos);
    }

    private void MoveButton(Vector3 targetPos)
    {
        // Анимация движения кнопки
        buttonTransform.position = targetPos;
    }
}
