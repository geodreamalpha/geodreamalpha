using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UserMenu
{
    public class Options : IType
    {
        (string name, int min, int max, Action<float> action)[] items;

        public Options(Menu menu, params (string name, int min, int max, Action<float> action)[] items)
        {
            this.items = items;
            UIElements elements = menu.GetComponent<UIElements>();

            for (int i = 0; i < items.Length; i++)
            {
                elements.AddButton(items[i].name, (Vector3.up * i * 50f) + (Vector3.left * 100f), menu.transform);
                elements.AddSlider(items[i].min, items[i].max, (Vector3.up * i * 50f) + (Vector3.right * 100f), items[i].action, menu.transform);
            }               
        }
    }
}
