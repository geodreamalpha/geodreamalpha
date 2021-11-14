using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace CombatSystemComponent
{
    public class TagGroup : MonoBehaviour
    {
        public List<string> identifiers = new List<string> { "enemy" };
        public List<string> harmers = new List<string> { "allie" };

        public bool IsHarmedBy(List<string> identifiers)
        {
            return harmers.Any(harmer => identifiers.Contains(harmer));
        }
    }
}