using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using TerrainGeneratorComponent;
using UnityEditor;

//Tyler Anderson; Unit Test
public class TerrainGeneratorTest
{
    //stores the height for each y,x vertex of the terrain
    float[,] heightmap;
    //stores the texture maps of the terrain
    float[,,] alphamapLayers;
    //stores the grass/ flower/ small plants/ small rocks detail of the terrain
    int[][,] detailLayers;
    //stores the trees, big plants, and big rocks of the terrain
    List<TreeInstance> trees;
    //tracks the list of enemies that can be spawned
    public static List<(int index, Vector3 position)> enemies = new List<(int index, Vector3 position)>();

    [UnityTest]
    public IEnumerator TerrainGeneratorTestWithEnumeratorPasses()
    {     
        TerrainGenerator generator = new TerrainGenerator();

        //Terrain generator is a notoriously hard thing to unit test.  Fortunately, many things went correctly when it was designed.
        //1) the terrain generator algorithm "Generator" class contains no gameobjects.  It only contains logic.  Therefore, it can tested.

        //2) the terrain generator algorithm tester iterates though all main locations in the game where assets differentiate.  If the location raises an
        //exception, the message will appear informing the coordinates of the failed generation for debugging purposes.
        for (int x = 0; x < 50000; x += 5000)
        {
            detailLayers = new int[14][,];
            trees = new List<TreeInstance> { };
            heightmap = new float[Chunk.lineLength, Chunk.lineLength];
            alphamapLayers = new float[Chunk.faceLength, Chunk.faceLength, 12];
            for (int i = 0; i < detailLayers.Length; i++)
                detailLayers[i] = new int[Chunk.faceLength, Chunk.faceLength];

            Assert.DoesNotThrow(() => generator.RunAUnitTest(x, 1000, heightmap, alphamapLayers, detailLayers, trees, enemies),
            "Area " + x.ToString() + "_y failed to load properly");
            yield return null;
        }              
    }
}
