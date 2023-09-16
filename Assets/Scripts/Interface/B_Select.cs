using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Select : MonoBehaviour
{
    public delegate void Delegate();
    public event Delegate Select;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        Select();
    }
}
