using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
    public bool bAllowWrapEastWest = true;
    public bool bAllowWrapNorthSouth = true;

    // Use this for initialization
    void Start ()
    {
        GenerateMap();
	}

    public GameObject go_HexPrefab;

    public Mesh meshOcean;
    public Mesh meshFlat;
    public Mesh meshHill;
    public Mesh meshMountain;

    public Material matOcean;
    public Material matPlains;
    public Material matGrasslands;
    public Material matMountains;

    //Tiles above this float become the respective name
    public float fHeightMountain = 0.85f;
    public float fHeightPlains = 0.6f;
    public float fHeightFlat = 0.2f;


    public readonly int iNumRows = 30;
    public readonly int iNumColumns = 60;

    private Hex[,] hexes;
    private Dictionary<Hex, GameObject> hexToGameObjectMap;

    public Hex GetHexAt(int x, int y)
    {
        //Throw error if the array is empty
        if(hexes == null)
        {
            Debug.LogError("Hexes array is not yet instantiated");
            return null;
        }

        //Return the divisible if wrapping is on. To stop from returning decimals or negative numbers
        if(bAllowWrapEastWest)
        {
            x = x % iNumColumns;
            if(x < 0)
            {
                x += iNumColumns;
            }
        }

        if(bAllowWrapNorthSouth)
        {
            y = y % iNumRows;

            if(y < 0)
            {
                y += iNumRows;
            }
        }

        //Return the requested hex
        return hexes[x, y];
    }

    virtual public void GenerateMap()
    {
        //Create the array and dictionary for storing the hexes and gameobjects 
        hexes = new Hex[iNumColumns, iNumRows];
        hexToGameObjectMap = new Dictionary<Hex, GameObject>();

        //Generate a map filled with Ocean
        for (int column = 0; column < iNumColumns; column++)
        {
            for (int row = 0; row < iNumRows; row++)
            {
                //Create the hex
                Hex hex = new Hex(column, row);
                hex.fElevation = -0.5f;

                //Add the hex to the hexes array
                hexes[column, row] = hex;

                //Get the hex spawn position
                Vector3 spawnPos = hex.GetPositionFromCamera(Camera.main.transform.position, iNumRows, iNumColumns);

                //Create the hex
                GameObject go_Hex = Instantiate(go_HexPrefab, spawnPos, Quaternion.identity, this.transform);

                //Add the gameobject into the dictionary
                hexToGameObjectMap.Add(hex, go_Hex);

                //Set references on the gameobject
                go_Hex.name = string.Format("HEX: {0},{1}", column, row);
                go_Hex.GetComponent<HexComponent>().hexRef = hex;
                go_Hex.GetComponent<HexComponent>().hexMap = this;

                //Set hex wrapping rules based on the Hex map
                hex.bAllowWrapEastWest = bAllowWrapEastWest;
                hex.bAllowWrapNorthSouth = bAllowWrapNorthSouth;

                //Add the coordinates to the hexes text mesh
                go_Hex.GetComponentInChildren<TextMesh>().text = string.Format("{0}, {1}", column, row);
            }
        }
    }

    public void UpdateHexVisuals()
    {
        for (int column = 0; column < iNumColumns; column++)
        {
            for (int row = 0; row < iNumRows; row++)
            {
                //Get the Hex
                Hex hex = hexes[column, row];

                GameObject go_Hex = hexToGameObjectMap[hex];

                //Set the hex mat
                MeshRenderer mr_Hex = go_Hex.GetComponentInChildren<MeshRenderer>();

                if (hex.fElevation >= fHeightMountain)
                {
                    mr_Hex.material = matMountains;
                }
                else if (hex.fElevation >= fHeightPlains)
                {
                    mr_Hex.material = matPlains;
                }
                else if(hex.fElevation >= fHeightFlat)
                {
                    mr_Hex.material = matGrasslands;
                }
                else
                {
                    mr_Hex.material = matOcean;
                }

                //Set the hex mesh
                MeshFilter mf_Hex = go_Hex.GetComponentInChildren<MeshFilter>();
                mf_Hex.mesh = meshOcean;
            }
        }
    }

    public Hex[] GetHexesWithinRadiusOf(Hex centerHex, int radius)
    {
        List<Hex> results = new List<Hex>();

        for(int dx = -radius + 1; dx < radius; dx++)
        {
            for(int dy = Mathf.Max(-radius + 1, -dx-radius + 1); dy < Mathf.Min(radius, -dx+radius); dy++)
            {
                int c;
                int r;

                c = centerHex.iColumn + dx;

                r = centerHex.iRow + dy;

                results.Add(GetHexAt(c, r));
            }
        }

        return results.ToArray();
    }
}
