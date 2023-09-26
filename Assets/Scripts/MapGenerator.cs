using System;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Terrain Zones")]
    public terrainType[] zones;

    const int chunkSize = 241;                               //241 -1 = 240 Which is a factor of 2, 4, 6, 8, 12 which makes for a lot of variation when we choose our LOD (Level Of Detail)
    
    [Range(0, 6)]
    public int levelOfDetail = 0;
    
    public float mapScale;
    public float meshHeight = 1f;

    public enum mapMode {Noise , Color , Mesh}
    public mapMode mode;

    public int octaves;

    [Range (0f, 1f)]
    public float persistance;
    public float lacunarity;

    public AnimationCurve heightCtrl;

    public bool autoGen;

    public void generateMap()
    {
        float[,] noidedMap = Noise.CreateNoiseMap(chunkSize, chunkSize , mapScale , octaves, persistance, lacunarity);            //Generating the map by using the noise function
        
        Color[] colorMap = new Color[chunkSize*chunkSize];                                                                        //This color map will store the colors according to the zones and the height of the map

        for(int x=0; x<chunkSize; x++)
        {
            for(int y=0; y<chunkSize; y++)
            {
                float currentHeight = noidedMap[x,y];
                for (int z=0; z<zones.Length; z++)                                                                              //Colors are based on the height and the color of the zone associated to that height
                {
                    if(currentHeight <= zones[z].height)
                    {
                        colorMap[y*chunkSize + x] = zones[z].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindAnyObjectByType<MapDisplay>();                                                                  //Painting the texture either by using the noided map to showcase the depth or the color map                               

        if (mode == mapMode.Noise) display.DrawNoiseMap(noidedMap);
        else if (mode == mapMode.Color) display.DrawColorMap(colorMap, chunkSize, chunkSize);
        else if (mode == mapMode.Mesh) display.DrawMesh(MeshGeerator.GenerateMesh(noidedMap ,heightCtrl, meshHeight, levelOfDetail) , colorMap, chunkSize, chunkSize);

    }

    private void Start()
    {
        generateMap();
    }

    [Serializable]
    public struct terrainType       //Describes the zones of the map
    {
        public string name;
        public float height;
        public Color color;
    }

}
