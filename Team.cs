using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Team
{
    public string name;
    Card_Unit[] teamDeck;
    public int rank, win, defeat, point, id, draw;

    public Team(string name, int id) {
        this.name = name;
       //this.teamDeck = deck;
        this.id = id;
        this.rank = 0; this.win = 0; this.defeat = 0; this.point = 0;
    }
    public void Initialize()
    {
        this.rank = 0;
        this.win = 0;
        this.defeat = 0;
        this.point = 0;
        this.draw = 0;
    }
}
