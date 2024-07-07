using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridGenerator : MonoBehaviour
{
    public int gridLength;
    public int gridWidth;

    public GameObject[,] grid;

    public GameObject chunk;
    public ObstacleData obstacleData;

    void Awake()
    {
        generate();
    }

    void generate()
    {
        // generating grid 
        Vector3 position;
        grid = new GameObject[gridLength, gridWidth];
        for (int x = 0; x < gridLength; x++)
        {
            for(int z =0; z < gridWidth; z++)
            {
                position = new Vector3(x, 0, z);
                grid[x,z] = Instantiate(chunk, position, Quaternion.identity);
                Chunk m_chunk= grid[x, z].GetComponent<Chunk>();
                m_chunk.Xpos = x; m_chunk.Zpos = z;//adding posintion of chunks in chunk objects

                int index = z * 10 + x;
                if (obstacleData.obstacleGrid[index])//if  obtascle is assigned by grid editor
                {
                    m_chunk.hasObst = true;
                }
            }
        }
    }
}
