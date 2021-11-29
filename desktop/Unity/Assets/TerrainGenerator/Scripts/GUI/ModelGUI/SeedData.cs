using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataManagerComponent;
using System.Linq;
using System;
using System.Collections.ObjectModel;

namespace TerrainGeneratorComponent
{
    public class SeedData
    {
        List<string> seeds = new List<string> { "", "", "", "", "", "" };

        public void PullFromFirebase()
        {
            DataManager.GetSeed(0, slot => { seeds[0] = slot; });
            DataManager.GetSeed(1, slot => { seeds[1] = slot; });
            DataManager.GetSeed(2, slot => { seeds[2] = slot; });
            DataManager.GetSeed(3, slot => { seeds[3] = slot; });
            DataManager.GetSeed(4, slot => { seeds[4] = slot; });
            DataManager.GetSeed(5, slot => { seeds[5] = slot; });
        }

        public void PushToFirebase()
        {
            DataManager.SetSeed(seeds[0], 0);
            DataManager.SetSeed(seeds[1], 1);
            DataManager.SetSeed(seeds[2], 2);
            DataManager.SetSeed(seeds[3], 3);
            DataManager.SetSeed(seeds[4], 4);
            DataManager.SetSeed(seeds[5], 5);
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
