using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
   
    private NavMeshAgent navMeshAgent;
    [SerializeField] private List<Transform> Waypoints;
    private int currentIndexWP;
    private float switchProbability = 0.1f;
    public bool isEnraged = false;
    private float enrageTime;
    public bool isGoing;
    private bool isForward;
    [SerializeField] private float enrageTimer;
    private Vector3 targetVector;
    private Animator animator;
    [SerializeField] private GameObject movingWall1;
    [SerializeField] private GameObject movingWall2;
    private int seed ;
    public bool destinationtriggered = false;
    public GameObject monsterDestination;
    public bool playerFound;
    private GameObject[] traps;
    public Vector3 temporaryTransform;
    private Transform finaldestination;
    public bool hasTarget;
    [SerializeField]private float agroRange = 20f;
    [SerializeField]private Transform player;
    [SerializeField]private LayerMask playerLayer;
    [SerializeField] private AudioClip AgroSound;
    [SerializeField] private AudioClip enrageSound;
    private AudioSource audioSource;
    public bool lightsAreActive;
    [SerializeField] private GameObject lights;
    public Transform _transform;
    public int noDoubleSound;
    
    
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        seed = UnityEngine.Random.Range(1, 3);
        animator = GetComponent<Animator>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        if (navMeshAgent == null)
        { 
         
        }
        else
        {
            Debug.Log("startdebug");
            SetDestination();
          
        }
    }
    private void FixedUpdate()
    {
        if (destinationtriggered && !playerFound)
        {

            if (monsterDestination != null)
            {
                temporaryTransform = monsterDestination.transform.position;
                hasTarget = true;
                Debug.Log("destinationtriggereddebug");
                SetDestination();

                Debug.Log("I know where to go");
                seed = seed == 1 ? 2 : 1;
                monsterDestination = null;
                destinationtriggered = false;

            }

        }
        if (isEnraged)
        {
            lightsAreActive = !lightsAreActive;
            lights.SetActive(lightsAreActive);
           
        }

        if (isGoing = true && navMeshAgent.remainingDistance <= 0.3f && !playerFound)
        {
            isGoing = false;
            hasTarget = false;
            Debug.Log("arriveddebug");
            RandomPatrol();
            SetDestination();
           
        }
    }
    private void Update()
    {
        if (destinationtriggered && !playerFound)
        {
            monsterDestination = GameObject.FindGameObjectWithTag("MonsterDestination");
            temporaryTransform = monsterDestination.transform.position;
        }
            playerFound = Physics.CheckSphere(transform.position, agroRange, playerLayer);
        if (playerFound)
        {
            
            if (noDoubleSound == 0)
            {
                AudioSource.PlayClipAtPoint(AgroSound, _transform.position, 1);
                noDoubleSound = 1;
            }
            isEnraged = true;
            traps = GameObject.FindGameObjectsWithTag("MonsterDestination");
            foreach (GameObject I in traps)
            {
                I.SetActive(false);
            }
            navMeshAgent.SetDestination(player.position);
            monsterDestination = null;
            if (playerFound && navMeshAgent.remainingDistance <= 0.1f)
            {
                Debug.Log("GameOver");
                SceneManager.LoadScene(2);
            }
        }
        if (seed == 1)
        {
            movingWall1.SetActive(true);
            movingWall2.SetActive(false);
        }
        if (seed == 2)
        {
            movingWall1.SetActive(false);
            movingWall2.SetActive(true);
        }
        animator.SetBool("IsEnraged", isEnraged);
        if (isEnraged==true)
        {
          
            enrageTime += Time.deltaTime;
            navMeshAgent.speed = 10;
            if (noDoubleSound == 0) 
            { 
                AudioSource.PlayClipAtPoint(enrageSound, _transform.position, 1);
                noDoubleSound = 1;
            }
            
           
        }
        if (isEnraged==false)
        {
            noDoubleSound = 0;
            navMeshAgent.speed = 5;
            enrageTimer += Time.deltaTime;
            lights.SetActive(true);
            lightsAreActive = true;
        }
        if (enrageTime >= 5 && !hasTarget && !playerFound)
        {
            isEnraged = false;
            enrageTime = 0f;
            enrageTimer = 0f;

        }
        if (enrageTimer >= 60)
        { 
           
            seed = seed == 1 ? 2 : 1;
            isEnraged = true;
            enrageTime += Time.deltaTime;
            enrageTimer = 0f;
        }
        
       
    }
    private void SetDestination()
    {
        if(hasTarget)
        {
            Debug.Log("HasTarget");
            navMeshAgent.SetDestination(temporaryTransform);
            isEnraged = true;
            //isGoing = true;
        }
        if (Waypoints != null && !hasTarget)
        {
            targetVector = Waypoints[currentIndexWP].transform.position;
            navMeshAgent.SetDestination(targetVector);
            //isGoing = true;
        }
    }
    private void RandomPatrol()
    {
        //Can and should be done better, I can make an empty list and fill it with all active WP, and active WP with monsters 
        if (UnityEngine.Random.Range(0f, 1f) <= switchProbability)
        {
            isForward = !isForward;
        }
        if (isForward)
        {
            currentIndexWP = (currentIndexWP + 1) % Waypoints.Count; // increments and checks if not above WP.count, if it is %
        }
        else
        {
            if (--currentIndexWP < 0) //remove 1 and checks if its not under 0
            {
                currentIndexWP = Waypoints.Count - 1;
            }
        }
    }

}
