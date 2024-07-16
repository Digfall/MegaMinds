using UnityEngine;

public class CameraController : MonoBehaviour
{
    public RectTransform background;  // Ссылка на фоновый объект
    public float speed = 10f;         // Скорость движения камеры

    // Ограничения по оси X, задаваемые через инспектор
    public float minX;
    public float maxX;

    private Vector3 lastMousePosition;

    void Update()
    {
        // Проверяем, нажата ли левая кнопка мыши
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            // Вычисляем разницу в положении мыши с последнего кадра
            Vector3 deltaMousePosition = Input.mousePosition - lastMousePosition;

            // Вычисляем новое положение камеры, только по оси X
            Vector3 newPosition = transform.position - new Vector3(deltaMousePosition.x, 0, 0) * speed * Time.deltaTime;

            // Ограничиваем положение камеры размерами, заданными через инспектор
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

            // Устанавливаем новое положение камеры
            transform.position = newPosition;

            // Обновляем последнее положение мыши
            lastMousePosition = Input.mousePosition;
        }
    }
}
