using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    Monster monster;
    [SerializeField]GameObject _me;

    private void Awake()
    {
        Debug.Log("ItsATrap!");
        GameObject g = GameObject.FindGameObjectWithTag("Monster");
        monster = g.GetComponent<Monster>();
        monster.destinationtriggered = true;
        //_me.SetActive(false);
        
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("gone");
        if (other.gameObject.CompareTag("Monster"))
        {
            Debug.Log("gone");
            
        }
    }

}
