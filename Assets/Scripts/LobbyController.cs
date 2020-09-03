using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    public int MAX_PLAYERS = 2;

    Network networkController;


    List<Player> Players = new List<Player>();

    // Start is called before the first frame update
    void Start()
    {
        networkController = GameObject.Find("SocketIO").GetComponent<Network>();
        networkController.CreateRoom();

        networkController.onRoomCreated += SetRoomCode;

        networkController.onPlayerEnter += OnPlayerEnter;
        networkController.onPlayerReady += OnPlayerReady;

    }

   
    void SetRoomCode(string roomCode)
    {
        InputField txtRoomCode = GameObject.Find("txtRoomCode").GetComponent<InputField>();
        txtRoomCode.text = roomCode;
    }


    void OnPlayerEnter(string playerId)
    {
        if (Players.Count <= this.MAX_PLAYERS)
        {
            Player player = new Player(playerId, Players.Count+1);
            Players.Add(player);
            GameObject.Find("SlotPlayer" + Players.Count).GetComponentInChildren<Text>().text = player.Nickname+" Conectado";
        }

    }

    void OnPlayerReady(string playerId)
    {
        var player = Players.Find(p => p.Id == playerId);
        player.Ready = true;
        Debug.Log("Player Ready: " + player.Id + "Name : " + player.Nickname);
        int index = Players.IndexOf(player)+1;
        Debug.Log(player.Nickname + "Ready..... Slot" + index);
        GameObject.Find("SlotPlayer" + index).GetComponentInChildren<Text>().text = player.Nickname+ " Listo";

        if (!Players.Exists(p => !p.Ready) && Players.Count == MAX_PLAYERS)
            this.gameObject.SendMessage("LoadScene", 2, SendMessageOptions.RequireReceiver);               

    }
}

class Player
{
    public string Id { get; set; }
    public bool Ready { get; set; }
    public string Nickname { get; set; }


    public Player(string id, int number)
    {
        this.Id = id;
        this.Nickname = "Jugador " + number;
    }
    public Player(string id, string nickname)
    {
        this.Id = id;
        this.Nickname = nickname;
    }
}
