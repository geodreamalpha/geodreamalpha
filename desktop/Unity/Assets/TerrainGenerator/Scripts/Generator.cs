using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainGeneratorComponent
{
    class Generator
    {
        AnimationCurve smoother = new AnimationCurve(new Keyframe(-0.2f, 3f), new Keyframe(1.2f, 0.8f));
        AnimationCurve lacunarity = new AnimationCurve(new Keyframe(-0.2f, 2f), new Keyframe(1.2f, 2f));
        AnimationCurve persistance = new AnimationCurve(new Keyframe(-0.2f, 0.45f), new Keyframe(1.2f, 0.65f));
        AnimationCurve amplitude = new AnimationCurve(new Keyframe(-0.2f, 0.2f), new Keyframe(1.2f, 0.8f));
        AnimationCurve eAmplitude = new AnimationCurve(new Keyframe(-0.2f, 0.2f), new Keyframe(1.2f, 0.8f)); //------
        AnimationCurve frequency = new AnimationCurve(new Keyframe(-0.2f, 0.6f), new Keyframe(1.2f, 0.602f));

        Variation[] layerVariations = new Variation[3] { new Variation(0, 5, 1, 1f, 0f, 0f, 10f), 
                                                         new Variation(5, 1, 1, 1f, 0f, 10f, 26f), 
                                                         new Variation(6, 6, 3, 1f, 0f, 36f, 18f) };

        Variation[] detailVariations = new Variation[1] { new Variation(0, 9, 4, 1f, 0f, 10f, 7f) };

        Variation[] treeVariations = new Variation[1] { new Variation(0, 5, 3, 0.0005f, 0f, 0f, 20f) };

        public void Generate(float worldX, float worldY, float[,] heightmap, float[,,] alphamap, int[][,] detailLayer, List<TreeInstance> instances, Vector3 chunkSize)
        {
            #region Initialize Local Vars
            float result;
            float posX;
            float posY;

            float lValue;
            float lElevation;
            float lSmoother;
            float lFrequency;
            float lAmplitude;
            float lLacunarity;
            float lPersistance;          
          
            int bioX;
            int bioY;
            float slope = 0f;
            float gradientX = 0f;
            float gradientY = 0f;
            int bioIndex = -1;

            TreeInstance instance = new TreeInstance();
            instance.widthScale = 1f;
            instance.heightScale = 1f;
            instance.lightmapColor = Color.white;
            instance.color = Color.white;
            instances.Add(instance);
            #endregion

            for (int y = 0; y < Chunk.lineLength; y++)
                for (int x = 0; x < Chunk.lineLength; x++)
                {
                    #region Set In-Loop Vars                 
                    result = 0;
                    posX = worldX + x;
                    posY = worldY + y;
                    #endregion

                    #region Initialize Height Fields
                    lValue = Mathf.PerlinNoise(posX * 0.6f * 0.01f, posY * 0.6f * 0.01f);
                    lElevation = Mathf.PerlinNoise(posX * 0.0001f, posY * 0.0001f);                  
                    lSmoother = smoother.Evaluate(lElevation);
                    lFrequency = frequency.Evaluate(lValue);
                    lAmplitude = amplitude.Evaluate(lValue);
                    lLacunarity = lacunarity.Evaluate(lValue);
                    lPersistance = persistance.Evaluate(lValue);
                    #endregion

                    //begin calculate height
                    #region Calculate Height
                    for (int i = 0; i < 10; i++)
                    {
                        result += Mathf.PerlinNoise(posX * lFrequency * 0.01f, posY * lFrequency * 0.01f) * lAmplitude;
                        lFrequency *= lLacunarity;
                        lAmplitude *= lPersistance;
                    }
                    heightmap[y, x] = (result * 0.1f + lElevation * 0.202f) / lSmoother;
                    #endregion
                    //end calculate height

                    if (x > 0 && y > 0)
                    {
                        #region Calculate Slope
                        gradientX = Mathf.Atan(Mathf.Abs((heightmap[y, x] - heightmap[y, x - 1]) * Chunk.size.y)) * Mathf.Rad2Deg;
                        gradientY = Mathf.Atan(Mathf.Abs((heightmap[y, x] - heightmap[y - 1, x]) * Chunk.size.y)) * Mathf.Rad2Deg;
                        slope = Mathf.Max(gradientX, gradientY);
                        #endregion

                        #region Initialize Position
                        posX = x + worldX - 1;
                        posY = y + worldY - 1;
                        bioX = x - 1;
                        bioY = y - 1;
                        #endregion

                        //begin calculate foilage
                        #region Calculate Layers (map textures)
                        bioIndex = -1;
                        foreach (Variation layer in layerVariations)
                        {
                            bioIndex = layer.GetPrototypeIndex(slope, lElevation);
                            if (bioIndex > -1)
                            {
                                alphamap[bioY, bioX, bioIndex] = 1f;
                                break;
                            }
                        }
                        #endregion

                        //only places trees and grass if they are above sea level
                        if (heightmap[y, x] * chunkSize.y > Chunk.seaLevel)
                        {
                            #region Calculate details (grass/ flowers/ small plants)
                            bioIndex = -1;
                            foreach (Variation detail in detailVariations)
                            {
                                bioIndex = detail.GetPrototypeIndex(slope, lElevation);
                                if (bioIndex > -1)
                                {
                                    detailLayer[bioIndex][bioY, bioX] = 14;
                                    break;
                                }
                            }
                            #endregion

                            #region Calculate Trees (trees/ bushes/ bolders/ giant objects)
                            bioIndex = -1;
                            foreach (Variation tree in treeVariations)
                            {
                                bioIndex = tree.GetPrototypeIndex(slope, lElevation);
                                if (bioIndex > -1)
                                {
                                    instance.prototypeIndex = bioIndex;
                                    instance.position = new Vector3((float)x / Chunk.faceLength, 0f, (float)y / Chunk.faceLength);
                                    instances.Add(instance);
                                    break;
                                }
                            }
                            #endregion
                        }
                        //end calculate foilage
                    }
                }
            
        }
    }
}


