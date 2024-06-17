using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsOnOff : MonoBehaviour
{
    [SerializeField] private GameObject gameOverCanvas;

    public void turnOnCanvas()
    {
        gameOverCanvas.SetActive(true);
    }
    public void turnOffCanvas()
    {
        gameOverCanvas.SetActive(false);
    }

}