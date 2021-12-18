using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;

namespace UserBook
{
    public interface IInventory
    {
        ReadOnlyCollection<string> GetReadOnlyCollection();
    }
}