using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsButton1;
    [SerializeField] private GameObject[] objectsButton2;
    [SerializeField] private GameObject[] objectsButton3;
    [SerializeField] private GameObject[] objectsButton4;
    [SerializeField] private GameObject[] objectsButton5;
    [SerializeField] private GameObject[] objectsButton6;
    [SerializeField] private GameObject[] objectsButton7;
    [SerializeField] private GameObject[] objectsButton8;
    [SerializeField] private GameObject[] objectsButton9;


    private GameObject[][] objectsGroups;

    private void Start()
    {
        // Инициализация массива массивов с группами объектов
        objectsGroups = new GameObject[][]
        {
            objectsButton1,
            objectsButton2,
            objectsButton3,
            objectsButton4,
            objectsButton5,
            objectsButton6,
            objectsButton7,
            objectsButton8,
            objectsButton9
        };
    }

    // Общий метод для обработки нажатий кнопок
    public void ButtonPressed(int buttonIndex)
    {
        for (int i = 0; i < objectsGroups.Length; i++)
        {
            if (i == buttonIndex)
            {
                TurnOnObjects(objectsGroups[i]);
            }
            else
            {
                TurnOffObjects(objectsGroups[i]);
            }
        }
    }

    // Включение объектов
    private void TurnOnObjects(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
        }
    }

    // Выключение объектов
    private void TurnOffObjects(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }

    // Методы для кнопок
    public void Button1Pressed()
    {
        ButtonPressed(0);
    }

    public void Button2Pressed()
    {
        ButtonPressed(1);
    }

    public void Button3Pressed()
    {
        ButtonPressed(2);
    }
    public void Button4Pressed()
    {
        ButtonPressed(3);
    }
    public void Button5Pressed()
    {
        ButtonPressed(4);
    }
    public void Button6Pressed()
    {
        ButtonPressed(5);
    }
    public void Button7Pressed()
    {
        ButtonPressed(6);
    }
    public void Button8Pressed()
    {
        ButtonPressed(7);
    }
    public void Button9Pressed()
    {
        ButtonPressed(8);
    }
}
