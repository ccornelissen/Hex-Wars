using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexComponent : MonoBehaviour
{
    public Hex hexRef;
    public HexMap hexMap;

    //Update hex position
	public void UpdatePosition()
    {
        this.transform.position = hexRef.GetPositionFromCamera(Camera.main.transform.position, hexMap.iNumRows, hexMap.iNumColumns);
    }
}
