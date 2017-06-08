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

    public Material[] HexMats;

    int iNumColumns = 20;
    int iNumRows = 40;


    public void GenerateMap()
    {
        for(int column = 0; column < iNumColumns; column++)
        {
            for (int row = 0; row < iNumRows; row++)
            {
                Hex hex = new Hex(column, row);

                GameObject go_Hex = Instantiate(go_HexPrefab, hex.GetHexPosition(), Quaternion.identity, this.transform);

                MeshRenderer mr_Hex = go_Hex.GetComponentInChildren<MeshRenderer>();
                mr_Hex.material = HexMats[Random.Range(0, HexMats.Length)];
            }
        }

        StaticBatchingUtility.Combine(this.gameObject);
    }
}
