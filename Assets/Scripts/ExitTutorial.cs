using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTutorial : MonoBehaviour
{
    public B_Doubts dobuts;
    private void OnMouseDown()
    {
        dobuts.papers[0].SetActive(false);
        dobuts.papers[1].SetActive(false);
        dobuts.papers[2].SetActive(false);
        dobuts.next.SetActive(false);
        dobuts.exit.SetActive(false);
    }
}
