using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarlyFinish : MonoBehaviour
{
    public void EarlyFinishEnd()
    {
        FindObjectOfType<Castle>().DestroyCastle();
    }

}