using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public float moveSpeed = 5.0f; // Скорость перемещения фона
    public float minX = -10.0f; // Минимальная позиция по X
    public float maxX = 10.0f; // Максимальная позиция по X

    void Update()
    {
        // Получаем движение мыши по оси X
        float mouseX = Input.GetAxis("Mouse X");

        // Перемещаем фон по оси X
        transform.Translate(Vector3.right * mouseX * moveSpeed * Time.deltaTime);

        // Ограничиваем движение фона
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, minX, maxX);
        transform.position = position;
    }
}
