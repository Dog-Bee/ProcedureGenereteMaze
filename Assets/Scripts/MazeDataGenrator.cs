using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeDataGenerator
{
    public float placeTrash;
    public MazeDataGenerator()
    {
        placeTrash = .1f;
    }
    public int[,] FromDimensions(int sizeRows, int sizeCols)
    {
        int[,] maze = new int[sizeRows, sizeCols];
        //stub
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);
        for(int i=0;i<=rMax;i++)
        {
            for(int j=0;j<=cMax;j++)
            {
                if(i==0|| j==0||i==rMax||j==cMax)
                {
                    maze[i, j] = 1;
                }
                else if(i%2==0&&j%2==0)
                {
                    if(Random.value>placeTrash)
                    {
                        maze[i, j] = 1;
                        int a = Random.value < .5 ? 0 : (Random.value < .5f ? -1 : 1);
                        int b = a != 0 ? 0:(Random.value < .5 ? -1 : 1);
                        maze[i + a, j + b]=1;
                    }
                }
            }
        }
        return maze;
    }
}
