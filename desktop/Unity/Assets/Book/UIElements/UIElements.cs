using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace UserUIElements
{
    public class UIElements : UIBase
    {
        public void Clear()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }
    }
}