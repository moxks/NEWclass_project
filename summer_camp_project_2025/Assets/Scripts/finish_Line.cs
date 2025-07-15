using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finish_Line : MonoBehaviour
{
    public game_Manager Manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "player_Capsule")

            Manager.endGame();
    }
}