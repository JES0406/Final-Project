using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPersistanceManager : MonoBehaviour
{
    public static DataPersistanceManager Instance;
    public GameData gameData;
    void Start()
    {
        if (Instance == null)
        {
            Instance = new DataPersistanceManager();
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

}
