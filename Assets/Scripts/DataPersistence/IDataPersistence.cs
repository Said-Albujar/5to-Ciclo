using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence 
{
    void LoadData(GameData data);
    void SaveData(ref GameData data); //Se usa ref para permitir modificar los datos de GameData
}
