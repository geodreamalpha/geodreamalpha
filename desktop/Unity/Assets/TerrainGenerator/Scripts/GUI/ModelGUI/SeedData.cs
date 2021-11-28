using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataManagerComponent;

namespace TerrainGeneratorComponent
{
    public class SeedData
    {
        List<string> seeds = new List<string> { "", "", "", "", "", "" };

        public void PullFromFirebase()
        {
            for (int i = 0; i < 6; i++)
                DataManager.GetSeed(i, slot => { seeds[i] = slot; });
        }

        public void PushToFirebase()
        {
            for (int i = 0; i < 6; i++)
                DataManager.SetSeed(seeds[i], i);
        }

        public void ReplaceWith(string seed)
        {
            seeds.RemoveAt(5);
            seeds.Insert(0, seed);            
        }

        public string GetAt(int index)
        {
            return seeds[index];
        }
    }
}
