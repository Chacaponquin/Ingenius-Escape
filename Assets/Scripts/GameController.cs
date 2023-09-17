using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private const int DEFAULT_COUNT_MOVEMENTS = 3;

    private MapGenerator map;
    public List<Player> players = new List<Player>();
    public List<Creature> creatures = new List<Creature>();
    public List<Boat> boats = new List<Boat>();

    private int countMovements = DEFAULT_COUNT_MOVEMENTS;

    public int turnPlayer;
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
    private GameObject tileChMecanica;
    [SerializeField]
    private GameObject tileChQuimica;
    [SerializeField]
    private GameObject tileChHidraulica;
    [SerializeField]
    private GameObject tileChElectrica;
    [SerializeField]
    private GameObject tileChAutomatica;
    [SerializeField]
    private GameObject tileChInformatica;
    [SerializeField]
    private GameObject tileChIndustrial;

    [SerializeField]
    private GameObject tileShip;

    public List<GameObject> characteres1;
    public List<GameObject> characteres2;


    private void initPlayers()
    {
        Boat boat1 = new Boat();
        Boat boat2 = new Boat();
        Boat boat3 = new Boat();
        Boat boat4 = new Boat();

        KillerWhale wale1 = new KillerWhale();
        KillerWhale wale2 = new KillerWhale();
        Crocodile crocodile1 = new Crocodile();
        Dolphin dolphin = new Dolphin();

        this.setCreature(wale1);
        this.setCreature(wale2);
        this.setCreature(crocodile1);
        this.setCreature(dolphin);

        this.setBoat(boat1);
        this.setBoat(boat2);
        this.setBoat(boat3);
        this.setBoat(boat4);

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
        this.defineBoatsPositions();

        selectButton.Select += Select;
        exitButton.Exit += Exit;
        playButton.Play += Play;
    }

    public void decreaseTurnCount() {
        this.countMovements--;

        if(this.countMovements == 0)
        {
            this.turnPlayer = this.turnPlayer == 0 ? 1 : 0;
            this.countMovements = this.calcMovements(this.turnPlayer);
            this.map.sinkRandomEarthenware();
        }
    }

    public void definePlayerCharacters()
    {
        for(int i = 0; i < this.players[0].characters.Count; i++)
        {
            Character character = this.players[0].characters[i];
            GameObject obj = this.characteres1[i];

            GameObject image = this.createCharacterImage(obj, character);
            image.transform.position = new Vector3(image.transform.position.x, image.transform.position.y, 0);
        }

        for (int i = 0; i < this.players[1].characters.Count; i++)
        {
            Character character = this.players[1].characters[i];
            GameObject obj = this.characteres2[i];

            GameObject image = this.createCharacterImage(obj, character);
            image.transform.position = new Vector3(image.transform.position.x, image.transform.position.y, 0);
        }

    }

    private int calcMovements(int turn)
    {
        int movs = DEFAULT_COUNT_MOVEMENTS;

        Player playerInTourn = this.players[turn];
        List<Character> mechanicInBoats = playerInTourn.mechanicsInBoats();
        List<Character> hidraulicInWater = playerInTourn.hidraulicsInWater();

        movs += mechanicInBoats.Count * 2;
        movs += hidraulicInWater.Count * 3;

        return movs;
    }

    private List<Cell> getBoatsFreeCells()
    {
        List<Cell> centerCells = this.map.getCenterIsland();
        List<Cell> freeCells = new List<Cell>();

        foreach (Cell centerCell in centerCells)
        {
            List<Cell> adjacents = centerCell.getAdjacentCells();

            foreach (Cell aCell in adjacents)
            {
                int index = freeCells.IndexOf(aCell);
                if (index == -1 && aCell.type == CELL_TYPE.WATER && aCell.isEmpty())
                {
                    freeCells.Add(aCell);
                }
            }
        }

        return freeCells;
    }

    private void defineBoatsPositions()
    {
        foreach(Boat boat in this.boats)
        {
            List<Cell> freeCells = this.getBoatsFreeCells();

            int randomIndex = Random.Range(0, freeCells.Count);
            Cell cell = freeCells[randomIndex];
            cell.setItem(boat);
            boat.actualCell = cell;
            cell.setImage(Instantiate(tileShip, cell.gridCell.transform.position, Quaternion.identity));
        }
    }

    public void setBoat(Boat b)
    {
        this.boats.Add(b);
    }

    private void initTurn()
    {
        this.turnPlayer = 0;
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
                cell.setImage(this.createCharacterImage(cell.gridCell, c));
            }
        }
    }

    private GameObject createCharacterImage(GameObject cell, Character ch) {
        if(ch.type == CHARACTER_TYPE.AUTOMATICA)
        {
            return Instantiate(tileChAutomatica, cell.transform.position, Quaternion.identity);
        }
        else if (ch.type == CHARACTER_TYPE.ELECTRICA)
        {
            return Instantiate(tileChElectrica, cell.transform.position, Quaternion.identity);
        }
        else if (ch.type == CHARACTER_TYPE.HIDRAULICA)
        {
            return Instantiate(tileChHidraulica, cell.transform.position, Quaternion.identity);
        }
        else if (ch.type == CHARACTER_TYPE.INDUSTRIAL)
        {
            return Instantiate(tileChIndustrial, cell.transform.position, Quaternion.identity);
        }
        else if (ch.type == CHARACTER_TYPE.INFORMATICA)
        {
            return Instantiate(tileChInformatica, cell.transform.position, Quaternion.identity);
        }
        else if (ch.type == CHARACTER_TYPE.MECANICA)
        {
            return Instantiate(tileChMecanica, cell.transform.position, Quaternion.identity);
        }
        else
        {
            return Instantiate(tileChQuimica, cell.transform.position, Quaternion.identity);
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
