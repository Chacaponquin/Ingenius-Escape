using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLogic : MonoBehaviour
{
    private void Start()
    {
    }
    void Update()
    {
        
    }
    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = Color.gray;
    }
    private void OnMouseExit()
    {
;       GetComponent<SpriteRenderer>().color = Color.white;
    }
}
