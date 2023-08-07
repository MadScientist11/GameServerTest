using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using SharedLibrary;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private async void Start()
    {
        PlayerModel player = await Tools.HttpClient.Get<PlayerModel>("http://localhost:5150/player/66");
        Debug.Log(player.Id);
    }
}