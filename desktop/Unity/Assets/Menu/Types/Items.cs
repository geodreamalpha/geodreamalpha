using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UserMenu
{
    public class Items : IType
    {
        (string name, int count)[] items;

        public Items(Menu menu, params (string name, int count)[] items)
        {
            this.items = items;

            UIElements elements = menu.GetComponent<UIElements>();

            for (int i = 0; i < items.Length; i++)
                elements.AddButton(items[i].name + " x" + items[i].count, Vector3.up * i * 50, menu.transform);
        }
    }
}