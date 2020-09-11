using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera
{
    private Camera myCamera;


    public PlayerCamera(Camera camera)
    {
        myCamera = camera;
    }

    public void MoveCamera(GameObject player)
    {
        //カメラ追従
        var getPos = myCamera.transform.position;
        getPos.z = player.transform.position.z + 3;
        myCamera.transform.position = getPos;
    }
}
