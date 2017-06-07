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

    //Returns hex world space position
    public Vector3 GetHexPosition()
    {
        float fRadius = 1.0f;
        float fHeight = fRadius * 2.0f;
        float fWidth = WIDTH_MULTIPLIER * fHeight;

        float fHorizSpacing = fWidth * (this.iColumn + this.iRow * 0.5f);
        float fVertSpacing = (fHeight * 0.75f) * this.iRow;

        return new Vector3(fHorizSpacing, 0, fVertSpacing);
    }


}
