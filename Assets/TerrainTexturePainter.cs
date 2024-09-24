using UnityEngine;

public class TerrainTexturePainter : MonoBehaviour
{
    public Terrain terrain; // Assign your terrain object here
    public float grassStartHeight = 56f; // The height at which grass texture starts
    public float grassEndHeight = 58f;   // The height at which grass texture ends

    void Start()
    {
        ApplyTextures();
    }

    void ApplyTextures()
    {
        TerrainData terrainData = terrain.terrainData;
        float[,,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];

        for (int y = 0; y < terrainData.alphamapHeight; y++)
        {
            for (int x = 0; x < terrainData.alphamapWidth; x++)
            {
                // Get the normalized terrain coordinate
                float normX = (float)x / (terrainData.alphamapWidth - 1);
                float normY = (float)y / (terrainData.alphamapHeight - 1);

                // Get the height at this coordinate
                float height = terrainData.GetHeight(Mathf.RoundToInt(normY * terrainData.heightmapResolution), Mathf.RoundToInt(normX * terrainData.heightmapResolution));

                // Initialize an array to store the texture weights
                float[] splatWeights = new float[terrainData.alphamapLayers];

                // Apply base texture below the grass range
                if (height < grassStartHeight)
                {
                    splatWeights[0] = 1; // Base texture (e.g., dirt or rock)
                }
                // Apply grass texture within the specific height range
                else if (height >= grassStartHeight && height <= grassEndHeight)
                {
                    splatWeights[1] = 1; // Grass texture
                }
                else
                {
                    splatWeights[0] = 1; // Default to the base texture outside the grass range
                }

                // Normalize the weights so they sum to 1
                float total = splatWeights[0] + splatWeights[1];
                for (int i = 0; i < splatWeights.Length; i++)
                {
                    splatWeights[i] /= total;
                }

                // Assign this point's weights to the splatmap
                for (int i = 0; i < terrainData.alphamapLayers; i++)
                {
                    splatmapData[x, y, i] = splatWeights[i];
                }
            }
        }

        // Apply the new splatmap to the terrain
        terrainData.SetAlphamaps(0, 0, splatmapData);
    }
}
