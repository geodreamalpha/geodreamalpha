using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CombatSystemComponent; 

public class DamageMenuBehavior : MonoBehaviour
{

    [SerializeField] Slider slider;
    [SerializeField] CombatSystem combatSystem;

    List<(Transform transform, RectTransform rectTransform)> damageList = new List<(Transform, RectTransform)>();

    // Update is called once per frame
    void Update()
    {
        slider.value = combatSystem.GetPlayerHealth();
        //stamina bar update goes here

        //updates damage text positions
        for (int i = 0; i < damageList.Count; i++)
            if (damageList[i].transform == null || damageList[i].rectTransform == null)
            {
                damageList.RemoveAt(i);
                i--;
            }
            else if (damageList[i].rectTransform.position.z > 0)
                damageList[i].rectTransform.position = Camera.main.WorldToScreenPoint(damageList[i].transform.position);
    }
    
    public void ShowDamage(Transform targetTransform, float damageAmount, Color damageTint, CombatSystemAssets assets)
    {
        damageList.Add((targetTransform, Instantiate(assets.GetDamageText(),
            Camera.main.WorldToScreenPoint(targetTransform.position),
            Quaternion.identity,
            transform).GetComponent<RectTransform>()));

        GameObject textObject = damageList[damageList.Count - 1].rectTransform.gameObject;

        TMPro.TMP_Text text = textObject.GetComponent<TMPro.TMP_Text>();
        text.text = damageAmount.ToString();
        text.color = damageTint;
    }
}
