using Mirror.Cloud.Examples.Pong;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public bool Debugging = false;

    [SerializeField]
    private float walkSpeed = 1;

    [SerializeField]
    private float runSpeed = 4;

    [SerializeField]
    private bool isRunning = false;

    public float Speed
    {
        get
        {
            return isRunning ? runSpeed : walkSpeed;
        }
    }

    [SerializeField]
    private Transform destinationTransform;

    public Vector3 Destination
    {
        get
        {
            return CanSeePlayer ? playerTransform.transform.position : destinationTransform == null ? transform.position : destinationTransform.position;
        }
        set
        {
            if (destinationTransform == null)
            {
                destinationTransform = new GameObject($"{transform.name}Destination").transform;
            }
            destinationTransform.position = value;
            //agent.SetDestination(destinationTransform.position);
        }
    }

    [SerializeField]
    private NavMeshAgent agent;

    private Vector3 previousPosition;

    private float curSpeed;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Transform rightHand;

    private BasicBehaviour playerTransform;

    private bool carriesPlayer = false;

    public bool CanSeePlayer
    {
        get
        {
            return playerTransform != null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogError($"{name} is missing a {typeof(Animator)}", gameObject);
            }
        }

        if (agent == null)
        {
            agent = GetComponent<NavMeshAgent>();
            if (agent == null)
            {
                Debug.LogError($"{name} is missing a {typeof(NavMeshAgent)}", gameObject);
            }
        }

        if (destinationTransform == null)
        {
            Destination = transform.position;
        }
        else
        {
            Destination = destinationTransform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleNavigation();

        if (Debugging) Debug.Log(curSpeed);
    }

    private void onReachDestination()
    {
        agent.SetDestination(transform.position);
        agent.speed = 0;
        animator.SetFloat("Velocity", 0);
    }

    private void onSetDestination()
    {
        agent.SetDestination(Destination);
        agent.speed = Speed;
        animator.SetFloat("Velocity", isRunning ? 1.0f : 0.5f);
    }

    private void pickupPlayer()
    {
        Debug.Log("Pickup Player");
        animator.SetTrigger("Pickup");
        carriesPlayer = true;
        playerTransform.transform.parent = rightHand;
        playerTransform.transform.localPosition = Vector3.zero;
    }

    private void putDownPlayer()
    {
        Debug.Log("Put down Player");
        animator.SetTrigger("Pickup");
        carriesPlayer = false;
        playerTransform.transform.parent = null;
        playerTransform.transform.localPosition = Vector3.zero;
    }

    private void handleNavigation()
    {
        if(carriesPlayer)
        {
            onReachDestination();
        }
        else
        {
            if (Vector3.Distance(Destination, transform.position) < 0.2f)
            {
                onReachDestination();

                if (CanSeePlayer)
                {
                    pickupPlayer();
                }
            }
            else
            {
                onSetDestination();
            }
        }

        Vector3 curMove = transform.position - previousPosition;
        curSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Player")
        {
            Debug.Log($"{name} has spotted Player");
            BasicBehaviour playerController = other.transform.GetComponent<BasicBehaviour>();
            if(playerController != null)
            {
                playerTransform = playerController;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log($"{name} sees Player => {CanSeePlayer}");

            if(CanSeePlayer)
            {
                playerTransform.GetRigidBody.velocity = Vector3.zero;
                playerTransform.IsActive = false;
            }
            else
            {
                playerTransform.IsActive = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log($"{name} does not see Player anymore");

            BasicBehaviour playerController = other.transform.GetComponent<BasicBehaviour>();

            if (playerController == playerTransform)
            {
                playerTransform.IsActive = true;
                playerTransform = null;
            }
        }
    }
}
