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

    public void ShowDamage(Vector3 position, float damageAmount, Color damageTint, CombatSystemAssets assets)
    {
        GameObject textObject = Instantiate(assets.getDamageText(), transform, false);

        TMPro.TMP_Text text = textObject.GetComponent<TMPro.TMP_Text>();
        text.text = damageAmount.ToString();
        text.color = damageTint;
        textObject.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(position);
    }
}
