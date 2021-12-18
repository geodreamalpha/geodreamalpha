using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UserMenu
{
    public class UIElements : MonoBehaviour
    {
        GameObject obj;

        public void AddButton(string name, Vector3 offset, Transform transform)
        {
            obj = InstantiateGUI("UserButton", transform);
            SetParameters(name, offset);
        }

        public void AddText(string name, Vector3 offset, Transform transform)
        {
            obj = InstantiateGUI("UserText", transform);
            SetParameters(name, offset);
        }

        public void AddSlider(int min, int max, Vector3 offset, Action<float> action, Transform transform)
        {
            obj = InstantiateGUI("UserSlider", transform);
            SetParameters(offset);
            Slider slider = obj.GetComponent<Slider>();
            slider.minValue = min;
            slider.maxValue = max;
            slider.wholeNumbers = true;
            slider.onValueChanged.AddListener(delegate { action(slider.value); });
        }

        GameObject InstantiateGUI(string fileName, Transform transform)
        {
            return Instantiate((GameObject)Resources.Load("GUI/" + fileName, typeof(GameObject)), transform);
        }

        void SetParameters(string name, Vector3 offset)
        {
            obj.GetComponentInChildren<TMP_Text>().text = name;
            SetParameters(offset);
        }

        void SetParameters(Vector3 offset)
        {
            obj.GetComponent<RectTransform>().position += offset;
        }
    }
}