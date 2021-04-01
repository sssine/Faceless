using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionSeq2 : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] private GameObject bird;
    [SerializeField] private GameObject invisbleWall;
    [SerializeField] private GameObject itself;
    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            bird.SetActive(true);
            invisbleWall.SetActive(true);
            GameObject g = GameObject.FindGameObjectWithTag("GameManager");
            gameManager = g.GetComponent<GameManager>();
            gameManager.winCondition2 = true;
            itself.SetActive(false);
        }
    }
}
