using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGeneratorComponent
{
    //biome class was not in domain model but will be needed since terrain will have several types of biomes
    //domain model may need to be updated.
    [System.Serializable]
    public class Biome
    {
        //gets the octave and frequency values that will be used from the perlin noise texture
        public const int OCTAVE = 8;
        public const float FREQUENCY = 0.6f;

        //name of the biome
        [SerializeField]
        string name;

        public string getName
        {
            get { return name; }
        }//

        //slope ranges in degrees, explaining which slope ranges certain details of the biome will go 
        [SerializeField]
        public List<Range> ranges = new List<Range> { new Range(), new Range(), new Range() };

        //color of healthy and dead grasses of biome
        [SerializeField]
        Color healthyGrass = new Color(155f / 255f, 171f / 255f, 115f / 255f); //(155, 171, 115)
        [SerializeField]
        Color dryGrass = new Color(155f / 255f, 171f / 255f, 115f / 255f); //(155, 171, 115)

        public Color getHealthyGrass
        {
            get { return healthyGrass; }
        }//

        public Color getDryGrass
        {
            get { return dryGrass; }
        }//

        //curves that will be used with the perlin noise texture to manipulate the map's height data
        [SerializeField]
        public AnimationCurve height = new AnimationCurve(new Keyframe(-0.2f, 0f));
        [SerializeField]
        public AnimationCurve lacunarity = new AnimationCurve(new Keyframe(-0.2f, 2f));
        [SerializeField]
        public AnimationCurve persistance = new AnimationCurve(new Keyframe(-0.2f, 0.45f), new Keyframe(1.2f, 0.65f));
        [SerializeField]
        public AnimationCurve amplitude = new AnimationCurve(new Keyframe(-0.2f, 0.2f), new Keyframe(1.2f, 0.4f));
        [SerializeField]
        public AnimationCurve frequency = new AnimationCurve(new Keyframe(-0.2f, 0.6f), new Keyframe(1.2f, 0.602f));

        //range class used for slopes
        [System.Serializable]
        public class Range
        {
            [SerializeField]
            public float min = 0;
            [SerializeField]
            public float max = 35;
            [SerializeField]
            public int layer = 0;
            [SerializeField]
            public int detail = 0;
            [SerializeField]
            public int tree = -1;
        }//
    }
}