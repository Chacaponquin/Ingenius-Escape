using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int sizeGrid;
    public int sizeBox;
    public List<List<Cell>> grid;
    public GameObject boxTest;

    private const int ISLAND_SIZE = 3;
    private const int CENTER_ISLAND_SIZE = 4;

    void Start()
    {
        this.init();
    }

    public void init()
    {
        this.initGrid();
        this.createIsland();
        this.createCenterIsland();
    }

    private void createIsland()
    {
        this.createTopLeftIsland();
        this.createTopRightIsland();
        this.createBottomLeftIsland();
        this.createBottomRightIsland();

        defineColorIsland(this.grid[1][1].gridCell);
        defineColorIsland(this.grid[sizeGrid - 2][1].gridCell);
        defineColorIsland(this.grid[1][sizeGrid - 2].gridCell);
        defineColorIsland(this.grid[sizeGrid - 2][sizeGrid - 2].gridCell);
    }

    private void initGrid()
    {
        int startPositionX = 0;
        int startPositionY = 0;
        grid = new List<List<Cell>>();

        for (int i = 0; i < sizeGrid; i++)
        {
            List<Cell> newRow = new List<Cell>();
            for (int j = 0; j < sizeGrid; j++)
            {
                Vector3 position = new Vector3(startPositionX, startPositionY, 0f);
                GameObject test = Instantiate(boxTest, position, Quaternion.identity);

                Cell newCell = new Cell(test, CELL_TYPE.WATER, this);

                newRow.Add(newCell);

                startPositionX++;
            }
            startPositionX = 0;
            startPositionY--;

            grid.Add(newRow);
        }
    }

    public int GetCenterIndex()
    {
        return sizeGrid / 2 + 1;
    }

    private void createCenterIsland()
    {
        int islandSpace = CENTER_ISLAND_SIZE;

        int indexCenter = this.GetCenterIndex();

        for (int j = indexCenter - 1; islandSpace >= 1; j++)
        {
            for (int i = indexCenter - islandSpace - 1; i < indexCenter + islandSpace; i++)
            {
                Cell cell = this.grid[j][i];
                this.defineColorIsland(cell.gridCell);
            }

            islandSpace--;
        }

        islandSpace = CENTER_ISLAND_SIZE;

        for (int j = indexCenter - 1; islandSpace >= 1; j--)
        {
            for (int i = indexCenter - islandSpace - 1; i < indexCenter + islandSpace; i++)
            {
                Cell cell = this.grid[j][i];
                this.defineColorIsland(cell.gridCell);
            }

            islandSpace--;
        }

        Cell extremeBottom = this.grid[indexCenter + CENTER_ISLAND_SIZE - 1][indexCenter - 1];
        this.defineColorIsland(extremeBottom.gridCell);

        Cell extremeTop = this.grid[indexCenter - CENTER_ISLAND_SIZE - 1][indexCenter - 1];
        this.defineColorIsland(extremeTop.gridCell);
    }

    public Cell GetCellCenter()
    {
        int indexCenter = this.GetCenterIndex();
        return this.grid[indexCenter][indexCenter];
    }

    private void defineColorIsland(GameObject cell)
    {
        if (cell.TryGetComponent<SpriteRenderer>(out SpriteRenderer component))
        {
            component.color = Color.blue;
        }
    }

    private void createBottomRightIsland()
    {
        for (int i = this.grid[0].Count - ISLAND_SIZE; i < this.grid[0].Count; i++)
        {
            Cell cell = this.grid[sizeGrid - 1][i];
            this.defineColorIsland(cell.gridCell);
        }

        for (int i = this.grid[0].Count - ISLAND_SIZE; i < this.grid[0].Count; i++)
        {
            Cell cell = this.grid[i][sizeGrid - 1];
            this.defineColorIsland(cell.gridCell);
        }
    }

    private void createBottomLeftIsland()
    {
        for (int i = 0; i < ISLAND_SIZE; i++)
        {
            Cell cell = this.grid[sizeGrid - 1][i];
            this.defineColorIsland(cell.gridCell);
        }

        for (int i = 0; i < ISLAND_SIZE; i++)
        {
            Cell cell = this.grid[i][sizeGrid - 1];
            this.defineColorIsland(cell.gridCell);
        }
    }

    private void createTopRightIsland()
    {
        for (int i = this.grid[0].Count - ISLAND_SIZE; i < this.grid[0].Count; i++)
        {
            Cell cell = this.grid[0][i];
            this.defineColorIsland(cell.gridCell);
        }

        for (int i = this.grid[0].Count - ISLAND_SIZE; i < this.grid[0].Count; i++)
        {
            Cell cell = this.grid[i][0];
            this.defineColorIsland(cell.gridCell);
        }
    }

    private void createTopLeftIsland()
    {
        for (int i = 0; i < ISLAND_SIZE; i++)
        {
            Cell cell = this.grid[0][i];
            this.defineColorIsland(cell.gridCell);
        }

        for (int i = 0; i < ISLAND_SIZE; i++)
        {
            Cell cell = this.grid[i][0];
            this.defineColorIsland(cell.gridCell);
        }
    }

    public List<Cell> freeCellSands()
    {
        List<Cell> free = new List<Cell>();

        foreach(List<Cell> cellRow in grid)
        {
            foreach(Cell cell in cellRow)
            {
                if(cell.type == CELL_TYPE.SAND)
                {
                    free.Add(cell);
                }
            }
        }

        return free;
    }

    public List<Cell> freeCellDirt()
    {
        List<Cell> free = new List<Cell>();

        foreach (List<Cell> cellRow in grid)
        {
            foreach (Cell cell in cellRow)
            {
                if (cell.type == CELL_TYPE.DIRT)
                {
                    free.Add(cell);
                }
            }
        }

        return free;
    }

    public Cell getCell(int x, int y) {
        try
        {
            return this.grid[x][y];
        }
         catch (System.Exception)
        {
            return null;
        }
     
    }

    public List<Cell> getCenterIsland()
    {
        List<Cell> cells = new List<Cell>();

        foreach(List<Cell> cellRow in this.grid)
        {
            foreach(Cell c in cellRow) {
                if (c.isLand())
                {
                    cells.Add(c);
                }
            }
        }

        return cells;
    }

    public (int, int) findCellCoords(Cell cell)
    {
        int x = -1;
        int y = -1;

        bool stop = false;

        for(int i = 0; i < this.grid.Count && !stop; i++)
        {
            int found = this.grid[i].IndexOf(cell);
            if(found != -1)
            {
                stop = true;
                x = i;
                y = found;
            }
        }

        return (x, y);
    }
}
