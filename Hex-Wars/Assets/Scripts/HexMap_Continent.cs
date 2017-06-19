using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap_Continent : HexMap
{
    public override void GenerateMap()
    {
        //Start off by generating the ocean
        base.GenerateMap();

        int iNumContinents = 2;
        int iContinentSpacing = 20;

        for (int c = 0; c < iNumContinents; c++)
        {
            //Make some kind of raised area
            int iNumSplats = Random.Range(5, 8);

            for (int i = 0; i < iNumSplats; i++)
            {
                int range = Random.Range(4, 8);
                int y = Random.Range(range, iNumRows - range);
                int x = Random.Range(0, 10) - y / 2 + c * iContinentSpacing;

                ElevateArea(x, y, range);
            }
        }
        

        //Add bumpiness. Perlin Noise?

        //Set mesh to appropriate mat based on height

        //Simulate rainfall/moisture and set plains/grasslands + forest

        //Make sure hex visuals are updated to match data
        UpdateHexVisuals();
    }

    void ElevateArea(int c, int r, int range, float centerHeight = 1f)
    {

        Hex centerHex = GetHexAt(c, r);

        Hex[] areaHexes = GetHexesWithinRadiusOf(centerHex, range);


        foreach(Hex h in areaHexes)
        {
            h.fElevation += centerHeight * Mathf.Lerp(1.0f, 0.25f, Hex.Distance(centerHex, h) / range);
        }
    }
}
