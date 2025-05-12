using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTerraforming : MonoBehaviour
{
    public Terrain terrain;
    public float digRadius = 0.1f;
    public float digStrength = 0.01f;

    private TerrainData terrainData;
    private int heightmapWidth;
    private int heightmapHeight;
    // Start is called before the first frame update
    void Start()
    {
        terrain.terrainData = Instantiate(terrain.terrainData);
        terrainData = terrain.terrainData;
        heightmapWidth = terrainData.heightmapResolution;
        heightmapHeight = terrainData.heightmapResolution;
    }

    public void Terraform(Vector3 worldPos)
    {
        Vector3 terrainPos = worldPos - terrain.transform.position;

        int x = Mathf.RoundToInt((terrainPos.x / terrainData.size.x) * heightmapWidth);
        int z = Mathf.RoundToInt((terrainPos.z / terrainData.size.z) * heightmapHeight);

        int digSize = Mathf.RoundToInt((digRadius / terrainData.size.x) * heightmapWidth);

        float[,] heights = terrainData.GetHeights(x - digSize / 2, z - digSize / 2, digSize, digSize);

        for (int i = 0; i < digSize; i++)
        {
            for (int j = 0; j < digSize; j++)
            {
                heights[i, j] -= digStrength;
            }
        }

        terrainData.SetHeights(x - digSize / 2, z - digSize / 2, heights);
    }
}
