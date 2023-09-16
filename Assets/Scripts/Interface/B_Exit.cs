using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Exit : MonoBehaviour
{
    public delegate void Delegate();
    public event Delegate Exit;
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
        print("TETOCO SALIR");
        Exit();
    }
}
