using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsButton1;
    [SerializeField] private GameObject[] objectsButton2;
    [SerializeField] private GameObject[] objectsButton3;

    public void Button1Pressed()
    {
        TurnOffObjects(objectsButton2);
        TurnOffObjects(objectsButton3);
        TurnOnObjects(objectsButton1);
    }

    public void Button2Pressed()
    {
        TurnOffObjects(objectsButton1);
        TurnOffObjects(objectsButton3);
        TurnOnObjects(objectsButton2);
    }
    public void Button3Pressed()
    {
        TurnOffObjects(objectsButton1);
        TurnOffObjects(objectsButton2);
        TurnOnObjects(objectsButton3);
    }

    private void TurnOnObjects(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(true);
        }
    }

    private void TurnOffObjects(GameObject[] objects)
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }
}
