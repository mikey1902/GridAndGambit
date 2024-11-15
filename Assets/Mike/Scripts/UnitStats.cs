using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour
{
    public bool isDestroyed = false;
    public int health;

    public void TakeDamage(int damage)
	{
        health -= damage;

        if(health <= 0)
		{
            DestroyUnit();
        }
	}

    public void DestroyUnit()
    {
        isDestroyed = true;
        if (isDestroyed)
        {
            Destroy(gameObject);
        }
    }
}
