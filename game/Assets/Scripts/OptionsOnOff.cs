using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsOnOff : MonoBehaviour
{
    public GameObject gameOverCanvas;

    public void turnOnCanvas()
    {
        gameOverCanvas.SetActive(true);
    }
    public void turnOffCanvas()
    {
        gameOverCanvas.SetActive(false);
    }
    void Update()
    {

    }
}