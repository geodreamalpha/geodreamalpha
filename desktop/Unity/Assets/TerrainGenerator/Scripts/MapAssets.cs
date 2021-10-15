using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;


namespace TerrainGeneratorComponent
{
    [System.Serializable]
    public class MapAssets
    {
        [SerializeField]
        List<TerrainLayer> layers = new List<TerrainLayer> { };

        [SerializeField]
        List<Texture2D> detailTextures = new List<Texture2D> { };

        [SerializeField]
        List<GameObject> detailMeshes = new List<GameObject> { };

        [SerializeField]
        List<GameObject> trees = new List<GameObject> { };

        public TerrainLayer[] GetLayers()
        {
            return layers.ToArray();
        }

        public DetailPrototype[] GetDetailTextures()
        {
            DetailPrototype[] detailPrototypes = new DetailPrototype[detailTextures.Count];

            for (int i = 0; i < detailTextures.Count; i++)
            {
                detailPrototypes[i] = new DetailPrototype();
                detailPrototypes[i].minWidth = 0.5f;
                detailPrototypes[i].minHeight = 0.5f;
                detailPrototypes[i].maxWidth = 0.5f;
                detailPrototypes[i].maxHeight = 0.5f;
                detailPrototypes[i].healthyColor = Color.white;
                detailPrototypes[i].dryColor = Color.white;
                detailPrototypes[i].prototypeTexture = detailTextures[i];
            }
            return detailPrototypes;
        }

        public TreePrototype[] GetTrees()
        {
            TreePrototype[] treePrototypes = new TreePrototype[trees.Count];

            for (int i = 0; i < trees.Count; i++)
            {
                treePrototypes[i] = new TreePrototype();
                treePrototypes[i].prefab = trees[i];

            }
            return treePrototypes;
        }

    }
}
