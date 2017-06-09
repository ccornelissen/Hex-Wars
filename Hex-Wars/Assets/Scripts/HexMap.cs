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

    public readonly int iNumColumns = 20;
    public readonly int iNumRows = 40;

    public List<HexComponent> hexes;


    public void GenerateMap()
    {
        for(int column = 0; column < iNumColumns; column++)
        {
            for (int row = 0; row < iNumRows; row++)
            {
                Hex hex = new Hex(column, row);

                Vector3 spawnPos = hex.GetPositionFromCamera(Camera.main.transform.position, iNumRows, iNumColumns);

                GameObject go_Hex = Instantiate(go_HexPrefab, spawnPos, Quaternion.identity, this.transform);

                go_Hex.GetComponent<HexComponent>().hexRef = hex;
                go_Hex.GetComponent<HexComponent>().hexMap = this;

                hexes.Add(go_Hex.GetComponent<HexComponent>());

                MeshRenderer mr_Hex = go_Hex.GetComponentInChildren<MeshRenderer>();
                mr_Hex.material = HexMats[Random.Range(0, HexMats.Length)];
            }
        }
    }
}
