using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Doubts : MonoBehaviour
{
    public List<GameObject> papers;
    public GameObject next;
    public GameObject exit;
    private void OnMouseDown()
    {
        papers[0].SetActive(true);
        next.SetActive(true);
        exit.SetActive(true);
    }
}
