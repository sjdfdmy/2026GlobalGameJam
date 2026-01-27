using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{

    private static GameDataManager instance;
    public static GameDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance=FindObjectOfType<GameDataManager>();
                if (instance == null)
                {
                    Debug.Log("No GameDataManager found!");
                }
            }
            return instance;
        }
    }

    public float blood;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
