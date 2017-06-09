using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotionHandler : MonoBehaviour {

    public HexMap hexMap;

    Vector3 oldPosition;

    // Use this for initialization
    void Start ()
    {
        oldPosition = this.transform.position;
    }

	// Update is called once per frame
	void Update ()
    {
        //TODO: Code to click and drag
        // WASD
        // Zoom in and out

        CheckIfCameraMoved();
	}

    public void PanToHex(Hex hex)
    {
        //TODO: Move camera to hex
    }

    void CheckIfCameraMoved()
    {
        if(oldPosition != this.transform.position)
        {
            oldPosition = this.transform.position;

            foreach(HexComponent hexComp in hexMap.hexes)
            {
                hexComp.UpdatePosition(); 
            }
        }
    }
}
