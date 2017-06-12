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

    public Mesh MeshOcean;
    public Mesh MeshFlat;
    public Mesh MeshHill;
    public Mesh MeshMountain;

    public Material MatOcean;
    public Material MatPlains;
    public Material MatGrasslands;
    public Material MatMountains;

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
            x = x % iNumRows;
        }

        if(bAllowWrapNorthSouth)
        {
            y = y % iNumColumns;
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

                //Set the hex mat
                MeshRenderer mr_Hex = go_Hex.GetComponentInChildren<MeshRenderer>();
                mr_Hex.material = MatOcean;

                //Set the hex mesh
                MeshFilter mf_Hex = go_Hex.GetComponentInChildren<MeshFilter>();
                mf_Hex.mesh = MeshOcean;
            }
        }
    }
}
