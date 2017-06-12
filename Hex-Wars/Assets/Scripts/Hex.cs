using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex
{
    //Integers for storing hex location
    public readonly int iColumn;
    public readonly int iRow;
    public readonly int iSum;

    //Data for map generation and in-game effects
    public float Elevation;
    public float Moisture;

    //Bool to control hex movement
    public bool bAllowWrapEastWest = true;
    public bool bAllowWrapNorthSouth = true;

    //Function used to set the hex locations (called when the hex is created by the HexMaps
    public Hex (int column, int row)
    {
        this.iColumn = column;
        this.iRow = row;
        this.iSum = -(column + row);
    }

    //The half width of the hex
    readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;

    //Default hex radius
    private float fRadius = 1.0f;

    //Returns hex world space position
    public Vector3 GetHexPosition()
    {
        float fHorizSpacing = HexWidth() * (this.iColumn + this.iRow * 0.5f);
        float fVertSpacing = HexVerticleSpacing() * this.iRow;

        return new Vector3(fHorizSpacing, 0, fVertSpacing);
    }

    //Calcuate the hex height
    public float HexHeight()
    {
        return fRadius * 2.0f;
    }

    //Calculate the hex width
    public float HexWidth()
    {
        return WIDTH_MULTIPLIER * HexHeight();
    }

    //Calculate hex verticle spacing
    public float HexVerticleSpacing()
    {
        return HexHeight() * 0.75f;
    }

    //Find the hexes position relative to the player cam (for wrapping purposes)
    public Vector3 GetPositionFromCamera(Vector3 cameraPosition, float numRows, float numColumns)
    {
        //Find map dimensions 
        float mapHeight = numRows * HexVerticleSpacing();
        float mapWidth = numColumns * HexWidth();

        Vector3 position = GetHexPosition();

        //Wrap/move hex if horizontal wrapping is enabled
        if(bAllowWrapEastWest)
        {
            float widthFromCamera = (position.x - cameraPosition.x) / mapWidth;

            if (widthFromCamera > 0)
            {
                widthFromCamera += 0.5f;
            }
            else
            {
                widthFromCamera -= 0.5f;
            }

            int widthToCamera = (int)widthFromCamera;

            position.x -= widthToCamera * mapWidth;
        }

        //Wrap/move hex if vertical wrapping is enabled
        if (bAllowWrapNorthSouth)
        {
            float heightFromCamera = (position.z - cameraPosition.z) / mapHeight;

            if (heightFromCamera > 0)
            {
                heightFromCamera += 0.5f;
            }
            else
            {
                heightFromCamera -= 0.5f;
            }

            int heightToCamera = (int)heightFromCamera;

            position.z -= heightToCamera * mapHeight;
        }

        //Return new hex position
        return position;
    }


}
