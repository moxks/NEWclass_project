using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup_01 : MonoBehaviour
{
    // player reference
    public player_Controller player;

    // Start is called before the first frame update
    void Start()
    {
        //grab player reference when game starts
        player = GameObject.Find("player_Capsule").GetComponent<player_Controller>();
    }

    void OnTriggerEnter(Collider other)
    {
        // if the player collides with pickup, coin number will increase and obj will be destroyed
        if (other.name == "player_Capsule")
        {
            player.coinCount++;
            Destroy(this.gameObject);
        }
    }
}