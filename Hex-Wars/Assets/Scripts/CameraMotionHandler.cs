using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotionHandler : MonoBehaviour
{
    public HexMap hexMap;

    Vector3 oldPosition;

    HexComponent[] HexComps;

    // Use this for initialization
    void Start ()
    {
        oldPosition = this.transform.position;

        HexComps = GameObject.FindObjectsOfType<HexComponent>();
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

    //If the camera is moving update all hexes to Wrap
    void CheckIfCameraMoved()
    {
        if(oldPosition != this.transform.position)
        {
            oldPosition = this.transform.position;

            foreach(HexComponent hexComp in HexComps)
            {
                hexComp.UpdatePosition(); 
            }
        }
    }
}
