using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public ObstacleData obstacleData;
    public GameObject obstaclePrefab;

    void Start()
    {
        GenerateObstacles();
    }

    void GenerateObstacles()
    {
        //Instantiate obstacle on the grid
        if (obstacleData == null || obstaclePrefab == null)
        {
            Debug.LogError("ObstacleData or ObstaclePrefab is not assigned.");
            return;
        }

        for (int y = 0; y < 10; y++)
        {
            for (int x = 0; x < 10; x++)
            {
                int index = y * 10 + x;
                if (obstacleData.obstacleGrid[index])//if  obtascle assigned by grid editor tool, instentiate
                {
                    Vector3 position = new Vector3(x, 1, y); // Elevate the obstacle above the ground cube
                    Instantiate(obstaclePrefab, position, Quaternion.identity);
                }
            }
        }
    }
}
