using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;

namespace TerrainGeneratorComponent
{
    //this class stores the foilage prototypes that will be used for the terrain generator.
    //
    [System.Serializable]
    class MapAssets
    {
        [SerializeField]
        List<TerrainLayer> layers = new List<TerrainLayer> { }; //layers/ map textures

        [SerializeField]
        List<UserDetailPrototype> detailTextures = new List<UserDetailPrototype> { }; //grass

        [SerializeField]
        List<UserTreePrototype> trees = new List<UserTreePrototype> { }; //trees

        public TerrainLayer[] GetLayers()
        {
            return layers.ToArray();
        }

        public DetailPrototype[] GetDetailTextures()
        {
            DetailPrototype[] detailPrototypes = new DetailPrototype[detailTextures.Count];

            for (int i = 0; i < detailTextures.Count; i++)
            {
                detailPrototypes[i] = detailTextures[i].GetDetailTexture();
            }
            return detailPrototypes;
        }

        public TreePrototype[] GetTrees()
        {
            TreePrototype[] treePrototypes = new TreePrototype[trees.Count];

            for (int i = 0; i < trees.Count; i++)
            {
                treePrototypes[i] = trees[i].GetTreePrototype();

            }
            return treePrototypes;
        }

        //custom class to hold settings for each individual detail
        [System.Serializable]
        public struct UserDetailPrototype
        {
            [SerializeField]
            Texture2D detailTexture;
            [SerializeField]
            float minWidth;
            [SerializeField]
            float maxWidth;
            [SerializeField]
            float minHeight;
            [SerializeField]
            float maxHeight;

            public DetailPrototype GetDetailTexture()
            {
                DetailPrototype detailPrototype = new DetailPrototype();
                detailPrototype.prototypeTexture = detailTexture;
                detailPrototype.minWidth = minWidth;             
                detailPrototype.maxWidth = maxWidth;
                detailPrototype.minHeight = minHeight;
                detailPrototype.maxHeight = maxHeight;
                detailPrototype.healthyColor = Color.white;
                detailPrototype.dryColor = Color.white;               
                return detailPrototype;
            }
        }

        //custom class to hold settings for each individual tree
        [System.Serializable]
        public struct UserTreePrototype
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
}
