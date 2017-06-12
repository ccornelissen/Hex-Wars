using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap_Continent : HexMap
{
    public override void GenerateMap()
    {
        //Start off by generating the ocean
        base.GenerateMap();

        //Make some kind of raised area
        ElevateArea(21, 15, 6);

        //Add bumpiness. Perlin Noise?

        //Set mesh to appropriate mat based on height

        //Simulate rainfall/moisture and set plains/grasslands + forest
    }

    void ElevateArea(int c, int r, int radius)
    {

    }
}
