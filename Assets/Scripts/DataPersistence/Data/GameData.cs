using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public int health;
    public int maxHearts;
    public bool haveMiner, haveEngineer, haveHairdress;
    public bool hold;

    //los valores definidos aqui, se usaran por defecto en caso no haya datos guardados-
    public GameData()
    {
        this.health = 3;
        this.maxHearts = 3;
        this.haveMiner = false;
        this.haveEngineer = false;
        this.haveHairdress = false;
        this.hold = false;
    }
}
