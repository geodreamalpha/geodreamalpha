using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UserMenu
{
    public abstract class Menu : MonoBehaviour
    {
        IType current;

        //ChangePage(new Options(this, ("options", 0, 50, Exit), ("Inventory", 0, 100, Exit))); //options

        public void ChangePage(IType page)
        {
            current = page;
        }
    }
}