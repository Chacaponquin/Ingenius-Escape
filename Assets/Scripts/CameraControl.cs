using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public MapGenerator map;
    private Transform target;
    // Start is called before the first frame update
    void Awake()
    {
        map = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapGenerator>();
        map.init();
        target = map.GetCellCenter().gridCell.transform;
        transform.position = new Vector3(target.position.x - 1, target.position.y + 1, target.position.z - 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
