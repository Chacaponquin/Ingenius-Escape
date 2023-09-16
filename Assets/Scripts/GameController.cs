using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private MapGenerator map;
    private List<Player> players = new List<Player>();
    private Player turnPlayer;
    private List<GameCamera> cameras;
    [SerializeField]
    private B_Select selectButton;
    [SerializeField]
    private B_Exit exitButton;
    [SerializeField]
    private B_Play playButton;


    private void initPlayers()
    {
        Player player1 = new Player("Player1", COLOR.BLUE);
        Player player2 = new Player("Player2", COLOR.RED);

        Character character1 = new Character(1, CHARACTER_TYPE.ELECTRICA);
        Character character2 = new Character(1, CHARACTER_TYPE.ELECTRICA);
        Character character3 = new Character(1, CHARACTER_TYPE.ELECTRICA);
        Character character4 = new Character(1, CHARACTER_TYPE.ELECTRICA);
        Character character5 = new Character(1, CHARACTER_TYPE.ELECTRICA);

        Character character6 = new Character(1, CHARACTER_TYPE.ELECTRICA);
        Character character7 = new Character(1, CHARACTER_TYPE.ELECTRICA);
        Character character8 = new Character(1, CHARACTER_TYPE.ELECTRICA);
        Character character9 = new Character(1, CHARACTER_TYPE.ELECTRICA);
        Character character10 = new Character(1, CHARACTER_TYPE.ELECTRICA);

        player1.setCharacter(character1);
        player1.setCharacter(character2);
        player1.setCharacter(character3);
        player1.setCharacter(character4);
        player1.setCharacter(character5);

        player2.setCharacter(character6);
        player2.setCharacter(character7);
        player2.setCharacter(character8);
        player2.setCharacter(character9);
        player2.setCharacter(character10);

        this.players.Add(player1);
        this.players.Add(player2);
    }

    void Start()
    {
        this.map = transform.GetComponent<MapGenerator>();

        map.init();
        this.initCameras();
        this.initPlayers();
        this.defineCharactersPositions();

        selectButton.Select += Select;
        exitButton.Exit += Exit;
        playButton.Play += Play;
    }

    private void initCameras()
    {
        cameras = new List<GameCamera>();
        GameObject[] cam = GameObject.FindGameObjectsWithTag("MainCamera");
        for (int i = 0; i < cam.Length; i++)
        {
            cameras.Add(new GameCamera(i + 1, cam[i]));
        }
        activateCamera(0);
    }

    private List<Character> getAllCharacters()
    {
        List<Character> list = new List<Character>();

        foreach(Player p in this.players)
        {
            foreach(Character c in p.characters)
            {
                list.Add(c);
            }
        }

        return list;
    }

    public void defineCharactersPositions()
    {
        MapGenerator map = transform.GetComponent<MapGenerator>();
        List<Character> allCharacters = this.getAllCharacters();
        List<Cell> centerCells = map.getCenterIsland();

        foreach (Character c in allCharacters)
        {
            List<Cell> freeCells = new List<Cell>();
            foreach(Cell centerC in centerCells)
            {
                if (centerC.isEmpty())
                {
                    freeCells.Add(centerC);
                }
            }

            if(freeCells.Count > 0)
            {
                int randomIndex = Random.Range(0, freeCells.Count);
                freeCells[randomIndex].setItem(c);
                c.actualCell = freeCells[randomIndex];
                map.changeColorPlayer(c.actualCell);
            }

        }

        
    }

    public void sinkRandomEarthenware() {
        MapGenerator map = transform.GetComponent<MapGenerator>();
        List <Cell> freeSands = map.freeCellSands();
        List<Cell> freeDirt = map.freeCellDirt();

        if(freeSands.Count == 0)
        {
            int randomCellIndex = Random.Range(0, freeDirt.Count);
            freeDirt[randomCellIndex].type = CELL_TYPE.WATER;

        }else
        {
            int randomCellIndex = Random.Range(0, freeSands.Count);
            freeSands[randomCellIndex].type = CELL_TYPE.WATER;
        }
    }

    private void activateCamera(int id)
    {
        foreach(GameCamera cam in cameras)
        {
            cam.camera.SetActive(false);
        }

        foreach(GameCamera cam in cameras)
        {
            if(cam.camera.GetComponent<CameraIdentificator>().id == id)
            {
                cam.camera.SetActive(true);
            }
        }
    }
    private void Select()
    {
        activateCamera(1);
    }
    private void Exit()
    {
        Application.Quit();
    }
    private void Play()
    {
        activateCamera(2);
    }
}
