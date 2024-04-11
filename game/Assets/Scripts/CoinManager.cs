using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{

    [SerializeField] int coins;
    [SerializeField] TextMeshProUGUI text;

    void Start()
    {

    }
    public void Addone()
    {
        coins += 1;
        text.text = coins.ToString();
    }
}
