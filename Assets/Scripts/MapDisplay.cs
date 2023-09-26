using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public MeshRenderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public void DrawNoiseMap(float[,] noidedMap)
    {
        int width = noidedMap.GetLength(0);
        int height = noidedMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] colors = new Color[width * height];
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                colors[j*width + i] = Color.Lerp(Color.black, Color.white , noidedMap[i, j]);
            }
        }

        texture.SetPixels(colors);
        texture.Apply();

        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(width, 1, height);

    }

    public Texture2D DrawColorMap(Color[] colormap , int width, int height)
    {
        Texture2D texture = new Texture2D(width, height);

        texture.SetPixels(colormap);
        texture.Apply();

        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(width, 1, height);

        return texture;

    }

    public void DrawMesh(MeshData meshData , Color[] colormap, int width, int height) {
        
        meshFilter.sharedMesh = meshData.Generatemesh();
        Texture2D texture = new Texture2D(width, height);

        texture.SetPixels(colormap);
        texture.Apply();

        meshRenderer.sharedMaterial.mainTexture = texture;

    }
}
