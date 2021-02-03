using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public GameObject roads;
    public GameObject grass;
    public GameObject water;
    public List<GameObject> buildings;
    public Vector2 islandSize = new Vector2(5, 5);
    public Vector2 buildingSpawnArea = new Vector2(4, 4);
    public Vector2 oceanSize = new Vector2(10, 10);
    public float buildingSpawnChance = 0.3f;
    public int tilesize = 8;

    void Awake()
    {
        GameObject[,] tiles = new GameObject[(int)oceanSize.x * 2 + 1, (int) oceanSize.y * 2 + 1];
        GenerateWater(ref tiles);
        GenerateGrass(ref tiles);
        GenerateHouses(ref tiles);
        ClearRoads(ref tiles);
        Spawn(tiles);
    }

    public void GenerateWater(ref GameObject[,] tiles)
    {
        for(int x = 0; x <= oceanSize.x * 2; x++)
        {
            for (int y = 0; y <= oceanSize.y * 2; y++)
            {
                tiles[x, y] = this.water;
            }
        }
    }

    public void GenerateGrass(ref GameObject[,] tiles)
    {
        for (int x = -(int)islandSize.x + (int)oceanSize.x; x <= islandSize.x + oceanSize.x; x++)
        {
            for (int y = -(int)islandSize.y + (int)oceanSize.y; y <= islandSize.y + oceanSize.y; y++)
            {
                tiles[x, y] = this.grass;
            }
        }
    }

    public void GenerateHouses(ref GameObject[,] tiles)
    {
        for (int x = -(int)buildingSpawnArea.x + (int)oceanSize.x; x <= buildingSpawnArea.x + oceanSize.x; x++)
        {
            for (int y = -(int)buildingSpawnArea.y + (int)oceanSize.y; y <= buildingSpawnArea.y + oceanSize.y; y++)
            {
                if(Random.Range(0.0f, 1.0f) < this.buildingSpawnChance)
                {
                    tiles[x, y] = this.buildings[Random.Range(0, this.buildings.Count)];
                }
            }
        }
    }

    public void ClearRoads(ref GameObject[,] tiles)
    {
        Transform[] transforms = roads.GetComponentsInChildren<Transform>();

        foreach(Transform t in transforms)
        {
            int x = (int) ((t.transform.position.x / tilesize) + oceanSize.x);
            int y = (int) ((t.transform.position.z / tilesize) + oceanSize.y);
            tiles[x, y] = null;
            Debug.Log("Cleared: " + x + " " + y);
        }
    }

    public void Spawn(GameObject[,] tiles)
    {
        for (int x = 0; x <= oceanSize.x * 2; x++)
        {
            for (int y = 0; y <= oceanSize.y * 2; y++)
            {
                if (tiles[x, y] != null)
                {
                    Instantiate(tiles[x, y], new Vector3((x - oceanSize.x) * 8, tiles[x,y].transform.position.y, (y - oceanSize.y) * 8), this.transform.rotation);
                }
            }
        }
    }
}
