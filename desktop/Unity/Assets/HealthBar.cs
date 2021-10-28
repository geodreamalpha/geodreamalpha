using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataManagerComponent;

//Nick Preston
public class HealthBar : MonoBehaviour
{
    //properties
    public Slider slider;
    public Text text;

    int health = 1;
    int maxHealth;

    Firebase fb = Firebase.GetInstance();

    void Start()
    {
        // int hp = int.Parse(fb.GetValue("health"));
        SetMaxHealth(100);
        SetHealth(this.health);

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            DataManager.GetXP(val=>{Debug.Log(val);});
        }
    }

    public void SetHealth(int health)
    {
        if(health > maxHealth){health = maxHealth;}
        if(health < 0){health = 0;}
        this.health = health;
        slider.value = health;
        text.text = health.ToString();
    }

    public void SetMaxHealth(int health)
    {
        if(health > 0)
        {
            this.maxHealth = health;
        }
    }
}
