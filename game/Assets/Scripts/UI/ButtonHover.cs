using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform buttonTransform;
    public float moveAmount = 0.9f; // Расстояние, на которое кнопка будет двигаться вверх
    public float moveSpeed = 4.5f; // Скорость движения кнопки
    private Vector3 originalPos;
    private bool isHovering = false;

    private void Start()
    {
        originalPos = buttonTransform.position;
    }

    private void Update()
    {

        if (isHovering)
        {
            Vector3 targetPos = originalPos + new Vector3(0, moveAmount, 0);
            buttonTransform.position = Vector3.Lerp(buttonTransform.position, targetPos, moveSpeed * Time.deltaTime);
        }
        else
        {

            buttonTransform.position = Vector3.Lerp(buttonTransform.position, originalPos, moveSpeed * Time.deltaTime);
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