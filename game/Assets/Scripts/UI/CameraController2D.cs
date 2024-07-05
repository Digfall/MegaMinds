using UnityEngine;

public class CameraController : MonoBehaviour
{
    public RectTransform background;  // Ссылка на фоновый объект
    public float speed = 10f;         // Скорость движения камеры

    // Ограничения по оси X, задаваемые через инспектор
    public float minX;
    public float maxX;

    void Update()
    {
        // Получаем ввод пользователя по горизонтали
        float horizontal = Input.GetAxis("Horizontal");

        // Вычисляем новое положение камеры, только по оси X
        Vector3 newPosition = transform.position + new Vector3(horizontal, 0, 0) * speed * Time.deltaTime;

        // Ограничиваем положение камеры размерами, заданными через инспектор
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        // Устанавливаем новое положение камеры
        transform.position = newPosition;
    }
}
