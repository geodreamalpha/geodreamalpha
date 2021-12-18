using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace UserMenu
{
    public class Tabs : IType
    {
        string[] items;

        public Tabs(Menu menu, params string[] items)
        {
            this.items = items;

            UIElements elements = menu.GetComponent<UIElements>();

            for (int i = 0; i < items.Length; i++)
                elements.AddButton(items[i], Vector3.up * i * 50, menu.transform);
        }
    }
}