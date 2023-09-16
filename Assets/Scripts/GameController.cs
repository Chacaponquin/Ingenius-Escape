using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private MapGenerator map;
    public List<Player> players = new List<Player>();
    public List<Creature> creatures = new List<Creature>();

    private int countMovements;

    public Player turnPlayer;
    public Character selectedCharacter;

    public Cell selectedCell;

    private List<GameCamera> cameras;
    [SerializeField]
    private B_Select selectButton;
    [SerializeField]
    private B_Exit exitButton;
    [SerializeField]
    private B_Play playButton;

    [SerializeField]
    private GameObject tileCrocodile;
    [SerializeField]
    private GameObject tileKillerWhale;
    [SerializeField]
    private GameObject tileDelphin;
    [SerializeField]
    private GameObject tileCharacter1;



    private void initPlayers()
    {
        KillerWhale wale1 = new KillerWhale();
        KillerWhale wale2 = new KillerWhale();
        Crocodile crocodile1 = new Crocodile();
        Dolphin dolphin = new Dolphin();

        this.setCreature(wale1);
        this.setCreature(wale2);
        this.setCreature(crocodile1);
        this.setCreature(dolphin);
    }

    void Start()
    {
        this.map = transform.GetComponent<MapGenerator>();

        map.init();
        this.initCameras();
        this.initPlayers();
        this.initTurn();
        this.defineCharactersPositions();
        this.defineCreaturesPositions();

        selectButton.Select += Select;
        exitButton.Exit += Exit;
        playButton.Play += Play;
    }

    private void initTurn()
    {
        this.turnPlayer = this.players[0];
        this.countMovements = 3;
    }

    public void setCreature(Creature c)
    {
        this.creatures.Add(c);
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

    public void defineCreaturesPositions()
    {
        List<Cell> seaCells = map.getSeaCells();

        foreach (Creature creature in creatures)
        {
            List<Cell> freeCells = new List<Cell>();
            foreach (Cell seaC in seaCells)
            {
                if (seaC.isEmpty())
                {
                    freeCells.Add(seaC);
                }
            }

            if (freeCells.Count > 0)
            {
                int randomIndex = Random.Range(0, freeCells.Count);
                Cell cell = freeCells[randomIndex];
                cell.setItem(creature);
                cell.setImage(this.createCreatureImage(cell, creature));
                creature.actualCell = cell;
            }

        }
    }

    private GameObject createCreatureImage(Cell cell, Creature creature) {
        if(creature is KillerWhale)
        {
            return Instantiate<GameObject>(tileKillerWhale, cell.gridCell.transform.position, Quaternion.identity);
        }
        else if(creature is Crocodile)
        {
            return Instantiate<GameObject>(tileCrocodile, cell.gridCell.transform.position, Quaternion.identity);
        }
        else
        {
            return Instantiate<GameObject>(tileDelphin, cell.gridCell.transform.position, Quaternion.identity);
        }
    }

    public void defineCharactersPositions()
    {
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
                Cell cell = freeCells[randomIndex];
                cell.setItem(c);
                c.actualCell = cell;
                cell.setImage(Instantiate(tileCharacter1, cell.gridCell.transform.position, Quaternion.identity));
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

        }
        else
        {
            int randomCellIndex = Random.Range(0, freeSands.Count);
            freeSands[randomCellIndex].type = CELL_TYPE.WATER;
        }
    }

    public void setPlayer(Player p)
    {
        this.players.Add(p);
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
