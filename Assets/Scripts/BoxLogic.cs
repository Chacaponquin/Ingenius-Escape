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
            int turn = this.gameController.turnPlayer;
            Player turnPlayer = this.gameController.players[turn];

            Cell selectedCell = gameController.selectedCell;

            if (selectedCell == null)
            {
                if (!found.isEmpty() && !found.item.disable )
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

                    print("Movido hacia " + found);

                    gameController.selectedCell = null;

                    this.gameController.decreaseTurnCount();
                }
            }
        }
    }
}
