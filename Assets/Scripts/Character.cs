using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum CHARACTER_TYPE
{
    AUTOMATICA,
    MECANICA,
    QUIMICA,
    HIDRAULICA,
    ELECTRICA,
    INFORMATICA,
    INDUSTRIAL
}

public class Character : MonoBehaviour
{
    private GameObject gridCell;
    private CHARACTER_TYPE type;
    private bool isArrival;

    // Start is called before the first frame update
    void Start()
    {
        this.isArrival = false;     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


