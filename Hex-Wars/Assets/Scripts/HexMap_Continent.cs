using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap_Continent : HexMap
{
    //Used for altering the perlin noise function
    public float fNoiseResolution = 0.1f;
    public float fNoiseScale = 0.9f;
    public int fGeneratorSeed = 0;

    public override void GenerateMap()
    {
        //Start off by generating the ocean
        base.GenerateMap();

        int iNumContinents = 2;
        int iContinentSpacing = iNumColumns/iNumContinents;

        //Seed the random generator
        if(fGeneratorSeed != 0)
        {
           Random.InitState(fGeneratorSeed);
        }

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

        //Add bumpiness
        Vector2 noiseOffset = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f));
        for (int column = 0; column < iNumColumns; column++)
        {
            for(int row = 0; row < iNumRows; row++)
            {
                Hex hex = GetHexAt(column, row);
                float noise = Mathf.PerlinNoise(((float)column / Mathf.Max(iNumRows, iNumColumns) / fNoiseResolution) + noiseOffset.x, ((float)row / Mathf.Max(iNumRows, iNumColumns) / fNoiseResolution) + noiseOffset.y);
                hex.fElevation += noise * fNoiseScale;
            }
        }


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
            h.fElevation = centerHeight * Mathf.Lerp(1.0f, 0.25f, (Hex.Distance(centerHex, h) / range));
        }
    }
}
