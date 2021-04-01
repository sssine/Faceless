using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SealDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject monster;
    [SerializeField] private GameObject lights;
    [SerializeField] private GameObject labSection;

    private void OnTriggerEnter(Collider other)
    {
        door.SetActive(true);
        monster.SetActive(false);
        lights.SetActive(false); 
        labSection.SetActive(false);

    }
}
