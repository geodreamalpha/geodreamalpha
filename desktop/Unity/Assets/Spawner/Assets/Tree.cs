using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spawner
{
    [System.Serializable]
    public struct Tree
    {
        [SerializeField]
        GameObject tree;

        public TreePrototype GetTreePrototype()
        {
            TreePrototype treePrototype = new TreePrototype();
            treePrototype.prefab = tree;
            return treePrototype;
        }//
    }
}