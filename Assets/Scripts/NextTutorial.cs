using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextTutorial : MonoBehaviour
{
    public B_Doubts dobuts;
    private void OnMouseDown()
    {
        if (dobuts.papers[0].active)
        {
            dobuts.papers[0].SetActive(false);
            dobuts.papers[1].SetActive(true);
        }
        else if (dobuts.papers[1].active)
        { 
        dobuts.papers[1].SetActive(false);
        dobuts.papers[2].SetActive(true);
        }
    }
}
