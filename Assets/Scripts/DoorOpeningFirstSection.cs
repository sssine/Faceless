using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpeningFirstSection : MonoBehaviour
{
    bool doorOpened1 = false;
    [SerializeField] private GameObject bird1, bird2, bird3;
    private int seed;
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject monster;
    [SerializeField] private GameObject lights;
    private GameManager gameManager;
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            doorOpened1 = true;
            seed = UnityEngine.Random.Range(1, 4);
        }
    }
    private void Update()
    {
        if (doorOpened1 == true)
        {
            GameObject g = GameObject.FindGameObjectWithTag("GameManager");
            gameManager = g.GetComponent<GameManager>();
            gameManager.door1Opened = true;
            door.SetActive(false);
            monster.SetActive(true);
            lights.SetActive(true);
            if (seed == 1)
            {
                bird1.SetActive(true);
            }
            if (seed == 2)
            {
                bird2.SetActive(true);
            }
            if (seed == 3)
            {
                bird3.SetActive(true);
            }
            Destroy(this);
        }
    }
}
