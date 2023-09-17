using UnityEngine;

public class B_Play : MonoBehaviour
{
    public delegate void Delegate();
    public event Delegate Play;
    public FormController form;
    public GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        if(this.form.listPlayer1.Count == 5 && this.form.listPlayer2.Count == 5)
        {
            Player player1 = new Player("Player 1", COLOR.BLUE);
            Player player2 = new Player("Player 2", COLOR.RED);

            foreach (CHARACTER_TYPE c in this.form.listPlayer1)
            {
                Character character = new Character(c);
                player1.characters.Add(character);
            }

            foreach (CHARACTER_TYPE c in this.form.listPlayer2)
            {
                Character character = new Character(c);
                player2.characters.Add(character);
            }

            this.gameController.players.Add(player1);
            this.gameController.players.Add(player2);

            this.gameController.defineCharactersPositions();
            this.gameController.definePlayerCharacters();

            Play();
        }
    }
}
