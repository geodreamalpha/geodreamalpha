using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CombatSystemComponent;
using TMPro;

namespace CombatSystemComponent
{
    //manages the in-game menu that holds health/ stamina meters, companion buttons, etc.
    class DamageMenuBehavior : MonoBehaviour
    {
        [SerializeField] Slider slider;
        [SerializeField] Slider staminaSlider;
        [SerializeField] CombatSystem combatSystem;
        [SerializeField] GameObject statBackground;
        [SerializeField] TMP_Text statInfo;

        List<(Transform transform, RectTransform rectTransform)> damageList = new List<(Transform, RectTransform)>();

        // Update is called once per frame
        void Update()
        {
            //update player health and stamina meters
            slider.value = combatSystem.GetPlayerHealth();
            staminaSlider.value = combatSystem.GetPlayerStamina();
            
            //update damage text positions
            for (int i = 0; i < damageList.Count; i++)
                if (damageList[i].transform == null || damageList[i].rectTransform == null)
                {
                    damageList.RemoveAt(i);
                    i--;
                }
                else if (damageList[i].rectTransform.position.z > 0)
                    damageList[i].rectTransform.position = Camera.main.WorldToScreenPoint(damageList[i].transform.position);

            //update player and companion stats
            if (Input.GetKeyDown(KeyCode.Alpha1))
                statBackground.SetActive(!statBackground.activeSelf);

            if (statBackground.activeSelf)
                statInfo.text = combatSystem.PrintPlayerStats() + combatSystem.PrintCompanionStats();
        }

        //show damage text on screen
        public void ShowDamage(Transform targetTransform, float damageAmount, Color damageTint, CombatSystemAssets assets)
        {
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);

            if (screenPosition.z > 0)
            {
                damageList.Add((targetTransform, Instantiate(assets.GetDamageText(),
                screenPosition,
                Quaternion.identity,
                transform).GetComponent<RectTransform>()));

                GameObject textObject = damageList[damageList.Count - 1].rectTransform.gameObject;

                TMPro.TMP_Text text = textObject.GetComponent<TMPro.TMP_Text>();
                text.text = damageAmount.ToString();
                text.color = damageTint;
            }
        }
    }
}