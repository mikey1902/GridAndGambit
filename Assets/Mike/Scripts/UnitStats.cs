using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitStats : MonoBehaviour
{
    public bool isDestroyed = false;
    public int health;
    public TMP_Text healthText;
    private int startHealth;
    

	private void Awake()
	{
        startHealth = health;
        healthText.text = health.ToString();
    }

	public void TakeDamage(int damage)
	{
        health -= damage;
        healthText.text = health.ToString();

        if (health <= 0)
		{
            GameManager.Instance.playerPieces.Remove(gameObject.GetComponent<PlayerUnit>());
            DestroyUnit();
        }
	}

    public void Heal(int healAmount)
    {
        health += healAmount;

        if (health >= startHealth)
        {
            health = startHealth;
        }

        healthText.text = health.ToString();
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
