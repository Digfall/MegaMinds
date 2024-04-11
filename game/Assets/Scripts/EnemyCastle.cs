using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCastle : MonoBehaviour
{
    public int HP;
    public LayerMask TowerMask;

    public void TakeDamage(int damage)
    {
        HP -= damage;
    }
    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
