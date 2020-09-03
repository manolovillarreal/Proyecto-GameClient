using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Commands {
    NONE,
    UP,
    DOWN
}

public class GameController : MonoBehaviour
{

    Network networkController;

    Dictionary<string,GameObject> Players= new Dictionary<string,GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        networkController = GameObject.Find("SocketIO").GetComponent<Network>();
        networkController.onPlayerInput+= OnPlayerInput;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPlayerInput(string playerId, string command)
    {
        //GameObject playerGameObject = Players[playerId];
        Debug.Log("command: " + command);
        proccessCommand(playerId,command);
    }

    void proccessCommand(string playerId,string command)
    {

        Commands playerCommand = Commands.NONE;


        if (command == ("UP"))
            playerCommand = Commands.UP;
        else if (command == "DOWN")
            playerCommand = Commands.DOWN;


        switch (playerCommand)
        {
            case Commands.UP:
                Debug.Log("PlayerId: " + playerId + " Press UP");
                break;
            case Commands.DOWN:
                Debug.Log("PlayerId: " + playerId + " Press DOWN");
                break;
            default:
                Debug.Log("PlayerId: " + playerId + " Press ");
                break;
        }
    }
}
