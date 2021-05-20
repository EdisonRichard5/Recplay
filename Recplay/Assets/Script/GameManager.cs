using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int lifesss = 3;
    public int points = 0;
    public float timeFinal = 6f;

    private void Awake() {
        int gameStatusCount = FindObjectsOfType<GameManager>().Length;
        if(gameStatusCount>1){
           
            Debug.Log("entra--");
            Destroy(gameObject);
        }
        else{
            Debug.Log("entra--pp");
            DontDestroyOnLoad(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
