using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLogic : MonoBehaviour
{
    MapGenerator mapGenerator;
    GameController gameController;
    private void Start()
    {
        mapGenerator = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapGenerator>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
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
    private void OnMouseDown()
    {
        Cell found = this.mapGenerator.findCellByObject(gameObject);

        if(found != null)
        {
            Player turnPlayer = this.gameController.turnPlayer;

            Cell selectedCell = gameController.selectedCell;

            if (selectedCell == null)
            {
                if (!found.isEmpty())
                {
                    bool canControl = found.item.isPlayerOwner(turnPlayer);

                    if (canControl)
                    {
                        gameController.selectedCell = found;
                        print("Seleccionada " + found);
                    }
                }
            }
            else
            {
                bool canMove = selectedCell.item.canMoveToCell(found);

                if (canMove)
                {
                    selectedCell.item.moveToCell(found);
                    gameController.selectedCell = null;

                    selectedCell.image.transform.position = found.gridCell.transform.position;
                    found.image = selectedCell.image;

                    print("Movido hacia " + found);
                }
            }
        }
    }
}
