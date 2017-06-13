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
        ElevateArea(58, 4, 4);

        //Add bumpiness. Perlin Noise?

        //Set mesh to appropriate mat based on height

        //Simulate rainfall/moisture and set plains/grasslands + forest

        //Make sure hex visuals are updated to match data
        UpdateHexVisuals();
    }

    void ElevateArea(int c, int r, int radius)
    {
        Hex centerHex = GetHexAt(c, r);

        Hex[] areaHexes = GetHexesWithinRadiusOf(centerHex, radius);

        foreach(Hex h in areaHexes)
        {
            h.fElevation = 0.5f;
        }
    }
}
