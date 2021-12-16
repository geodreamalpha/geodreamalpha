using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserItem
{
    public interface IItem
    {
        void Use();
        void Toss();
    }
}