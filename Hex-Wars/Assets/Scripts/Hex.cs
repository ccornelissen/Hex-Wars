using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex{

    public readonly int iColumn;
    public readonly int iRow;
    public readonly int iSum;

    public Hex (int column, int row)
    {
        this.iColumn = column;
        this.iRow = row;
        this.iSum = -(column + row);
    }

    //The half width of the hex
    readonly float WIDTH_MULTIPLIER = Mathf.Sqrt(3) / 2;

    private float fRadius = 1.0f;

    //Returns hex world space position
    public Vector3 GetHexPosition()
    {
        float fHorizSpacing = HexWidth() * (this.iColumn + this.iRow * 0.5f);
        float fVertSpacing = HexVerticleSpacing() * this.iRow;

        return new Vector3(fHorizSpacing, 0, fVertSpacing);
    }

    public float HexHeight()
    {
        return fRadius * 2.0f;
    }

    public float HexWidth()
    {
        return WIDTH_MULTIPLIER * HexHeight();
    }

    public float HexVerticleSpacing()
    {
        return HexHeight() * 0.75f;
    }


    public Vector3 GetPositionFromCamera(Vector3 cameraPosition, float numRows, float numColumns)
    {
        float mapHeight = numRows * HexVerticleSpacing();
        float mapWidth = numColumns * HexWidth();

        Vector3 position = GetHexPosition();

        float widthFromCamera = (position.x - cameraPosition.x) / mapWidth;


    }


}
