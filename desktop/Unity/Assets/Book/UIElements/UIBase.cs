using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace UserUIElements
{
    public abstract class UIBase : MonoBehaviour
    {
        GameObject obj;

        public void AddButton(string name, float x, float y, Action onClick)
        {
            obj = InstantiateGUI("UserButton");
            SetParameters(name, x, y);
            Button button = obj.GetComponent<Button>();
            button.onClick.AddListener(delegate { onClick(); });
        }

        public void AddDropDown(string name, float x, float y, Action onSelect)
        {
            obj = InstantiateGUI("UserDrop");
            SetParameters(name, x, y);
            //Button button = obj.GetComponent<Button>();
            //button.onClick.AddListener(delegate { onSelect(); });
        }

        public void AddInput(string name, float x, float y, Action onValueChanged)
        {
            obj = InstantiateGUI("UserDrop");
            SetParameters(name, x, y);
            //Button button = obj.GetComponent<Button>();
            //button.onClick.AddListener(delegate { onValueChanged(); });
        }

        public void AddToggle(string name, float x, float y, Action onCheck)
        {
            obj = InstantiateGUI("UserToggle");
            SetParameters(name, x, y);
            //Button button = obj.GetComponent<Button>();
            //button.onClick.AddListener(delegate { onClick(); });
        }

        public void AddText(string name, float x, float y)
        {
            obj = InstantiateGUI("UserText");
            SetParameters(name, x, y);
        }

        public void AddSlider(int min, int max, float x, float y, Action<float> onValueChanged)
        {
            obj = InstantiateGUI("UserSlider");
            SetParameters(x, y);
            Slider slider = obj.GetComponent<Slider>();
            slider.minValue = min;
            slider.maxValue = max;
            slider.wholeNumbers = true;
            slider.onValueChanged.AddListener(delegate { onValueChanged(slider.normalizedValue); });
        }

        GameObject InstantiateGUI(string fileName)
        {
            return GameObject.Instantiate((GameObject)Resources.Load("GUI/" + fileName, typeof(GameObject)), transform);
        }

        void SetParameters(string name, float x, float y)
        {
            obj.GetComponentInChildren<TMP_Text>().text = name;
            SetParameters(x, y);
        }

        void SetParameters(float x, float y)
        {
            obj.GetComponent<RectTransform>().position += new Vector3(x, y, 0);
        }
    }
}