using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public Vector3 playerPosition;
    public bool haveMiner, haveEngineer, haveHairdress,haveGlider;
    public bool hold;
    public int numCheckpoint;

    //los valores definidos aqui, se usaran por defecto en caso no haya datos guardados-
    public GameData()
    {
        numCheckpoint = 0;
        this.haveMiner = false;
        this.haveEngineer = false;
        this.haveHairdress = false;
        this.hold = false;
    }
}
