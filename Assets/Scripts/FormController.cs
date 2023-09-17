using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FormController : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> list1;
    [SerializeField]
    public List<GameObject> list2;

    public List<GameObject> listSelectedPlayer1;
    public List<GameObject> listSelectedPlayer2;

    public int indexT1 = 0;
    public int indexT2 = 0;

    public int turnPlayer = 0;

    public List<CHARACTER_TYPE> listPlayer1 = new List<CHARACTER_TYPE>();
    public List<CHARACTER_TYPE> listPlayer2 = new List<CHARACTER_TYPE>();
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
