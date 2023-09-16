using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera
{
    public GameObject camera;
    public int id;

    public GameCamera(int id, GameObject camera)
    {
        this.camera = camera;
        this.id = id;
    }
}
