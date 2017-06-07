using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexMap : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        GenerateMap();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public GameObject go_HexPrefab;

    public void GenerateMap()
    {
        for(int column = 0; column < 10; column++)
        {
            for (int row = 0; row < 10; row++)
            {
                Hex hex = new Hex(column, row);

                Instantiate(go_HexPrefab, hex.GetHexPosition(), Quaternion.identity, this.transform);
            }
        }
    }
}
