using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class simpleSetup : MonoBehaviour
{

    private int initHp=3;
    public int HP;
    public int OverHealth;
    public bool dead;
    public bool isFriendly;

    public void Awake()
    {
        HP = initHp;
        dead = false;
        if (!isFriendly) gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }
    // Update is called once per frame
    void Update()
    {
        OverHealth = HP - initHp;
        OverHealth =  Math.Clamp(OverHealth, 0, 50);
        if (dead == true) Destroy(this.gameObject);
        
    }
    
}
