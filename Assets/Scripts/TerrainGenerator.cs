using UnityEngine;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject grassBlockPrefab;
    public GameObject stoneBlockPrefab;

    public Vector2 terrainSize;

    public float terrainHeight;
    public float terrainDepth;
    public float terrainScale;

    private void Start ()
    {
        GenerateTerrain();
    }
    
    private void GenerateTerrain ()
    {
        List<Block> generatedBlocks = new List<Block>();

        for (int x = 0; x < terrainSize.x; x++)
        {
            for (int z = 0; z < terrainSize.y; z++)
            {
                int y = Mathf.FloorToInt(Mathf.PerlinNoise(x / terrainScale, z / terrainScale) * terrainHeight);

                GameObject grassBlock = Instantiate(grassBlockPrefab, new Vector3(x, y, z), Quaternion.identity, transform);

                generatedBlocks.Add(grassBlock.GetComponent<Block>());

                for (int i = -1; i > terrainDepth; i--)
                {
                    GameObject stoneBlock = Instantiate(stoneBlockPrefab, new Vector3(x, y + i, z), Quaternion.identity, transform);
                    generatedBlocks.Add(stoneBlock.GetComponent<Block>());
                }
            }
        }

        for (int i = 0; i < generatedBlocks.Count; i++)
        {
            generatedBlocks[i].GetNeighbouringBlocks();

            if (generatedBlocks[i].transform.CompareTag("Stone"))
            {
                generatedBlocks[i].gameObject.SetActive(false);
            }
        }
    }
}
