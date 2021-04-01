using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoobyTrap : MonoBehaviour
{
    [SerializeField] private GameObject beacon;
    [SerializeField] private GameObject boobytrap;
    [SerializeField] private Transform beaconDrop;
    private float timer;
    bool isTriggered = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            GameObject.Instantiate(beacon, beaconDrop);
            isTriggered = true;
            
        }
    }
    private void Update()
    {
        if (isTriggered) 
        { 
            timer += Time.deltaTime;
        }
        if (timer >= 2)
        {
            boobytrap.SetActive(false);
        }
    }
}
