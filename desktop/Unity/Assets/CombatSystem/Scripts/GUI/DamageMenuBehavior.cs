using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CombatSystemComponent; 

public class DamageMenuBehavior : MonoBehaviour
{
    GameObject healthBar;
    Slider slider;
    CombatSystem combatSystem;
    

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.Find("HealthBar");
        slider = healthBar.GetComponent<Slider>();
        combatSystem = GameObject.Find("CombatSystem").GetComponent<CombatSystem>(); 
        slider.value = combatSystem.GetPlayerHealth(); 
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = combatSystem.GetPlayerHealth();
    }
}
