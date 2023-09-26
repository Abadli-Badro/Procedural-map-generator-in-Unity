using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    //Generates a 2D array (or a grid) of noided values between 0 & 1
    public static float[,] CreateNoiseMap(int width ,  int height , float scale , int octaves, float persistance, float lacunarity)
    {
        float[,] noiseMap = new float[width,height];
        float maxnoise = float.MinValue;
        float minnoise = float.MaxValue;

        for (int x=0; x<width; x++)
        {
            for (int y=0; y<height; y++) {

                float frequency = 1;
                float amplitude = 1;
                float noiseHeight = 0;

                for(int i=0;  i<octaves; i++)
                {
                    float sampleX = x / scale * frequency;
                    float sampleY = y / scale * frequency;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY)*2-1;             //We use "Perlin noise" between the X and Y values to find the values (we multiply and subtract to add depth)

                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;                                                //0 < persistance < 1 so the amplitude decreases with each iteration
                    frequency *= lacunarity;                                                 //1 < lacunarity so the frequency increases with each iteration
                }

                if(noiseHeight>maxnoise) maxnoise = noiseHeight;                             
                else if (noiseHeight < minnoise) minnoise = noiseHeight;

                noiseMap[x,y] = noiseHeight;
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minnoise, maxnoise, noiseMap[x, y]);       //Normalizing the values so that we don't get negatives, or large values
            }
        }

        return noiseMap;
    }

}
