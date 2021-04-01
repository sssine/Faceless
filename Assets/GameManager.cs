 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public bool door1Opened;
    public bool door2Opened;
    public bool door3Opened;
    public bool winCondition1;
    public bool winCondition2;
    public bool winCondition3;
    private float gameTimer = 0f; 
    private float gameMaxTimer = 900f;

    [SerializeField] private GameObject winPedestal;

    // Start is called before the first frame update
    void Start()
    {
       


    }

    // Update is called once per frame
    void Update()
    {
        if (door1Opened || door2Opened || door3Opened)
        {
            gameTimer = gameTimer + Time.deltaTime;
            
        }
        if (gameTimer >= gameMaxTimer)
        {
            SceneManager.LoadScene(2);

        }
        if (winCondition1 && winCondition2 && winCondition3)
        {
            winPedestal.SetActive(true);
        }
    }
}
