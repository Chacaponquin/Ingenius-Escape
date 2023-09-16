using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private List<Player> players;
    private Player turnPlayer;
    private List<GameCamera> cameras;
    [SerializeField]
    private B_Play playButton;
    [SerializeField]
    private B_Exit exitButton;


    void Start()
    {
        cameras = new List<GameCamera>();
        GameObject[] cam = GameObject.FindGameObjectsWithTag("MainCamera");
        for (int i = 0; i < cam.Length; i++)
        {
            cameras.Add(new GameCamera(i + 1, cam[i]));
        }
        activateCamera(0);
        playButton.Play += Play;
        exitButton.Exit += Exit;
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
    private void Play()
    {
        activateCamera(1);
    }
    private void Exit()
    {
        Application.Quit();
    }
}
