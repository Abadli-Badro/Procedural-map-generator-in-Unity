using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGeerator 
{
    public static MeshData GenerateMesh(float[,] heightMap ,AnimationCurve heightCtrl, float heightMultiplier , int levelOfDetail)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        int meshSimplificationIncrement = (levelOfDetail == 0) ? 1 : levelOfDetail*2;  //this value will be added to the loop that adds the vertices, lowering the number of iteration and thus the vertices and triangles => less detailed mesh
        int verticesPerLine = (width - 1) / meshSimplificationIncrement + 1;  

        MeshData meshData = new MeshData(width, width);
        int vertexIndex = 0;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCtrl.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ - y);
                meshData.UVs[vertexIndex] = new Vector2((float)x / width, (float)y / height);             //Fixed rotation by subtracting

                if (x < width - 1 && y < height - 1)
                {
                    meshData.addTriangle(vertexIndex,  vertexIndex + width + 1, vertexIndex + width);
                    meshData.addTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }

        return meshData;

    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] UVs;

    int triangleIndex;
    public MeshData(int width , int height)
    {
        vertices = new Vector3[width * height];
        UVs = new Vector2[width * height];
        triangles = new int[(width-1) * (height-1) * 6];
    }

    public void addTriangle(int x, int y, int z)
    {
        triangles[triangleIndex] = x;
        triangles[triangleIndex+1] = y;
        triangles[triangleIndex+2] = z;

        triangleIndex += 3;
    }

    public Mesh Generatemesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = UVs;

        mesh.RecalculateNormals();      //To fix lighting issues

        return mesh;
    }

}