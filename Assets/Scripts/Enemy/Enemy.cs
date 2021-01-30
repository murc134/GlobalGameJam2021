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
            return destinationTransform == null ? transform.position : destinationTransform.position;
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
        if(Vector3.Distance(Destination,transform.position) < 0.2f)
        {
            agent.SetDestination(transform.position);
            agent.speed = 0;
            animator.SetFloat("Velocity", 0);
        }
        else
        {
            agent.SetDestination(Destination);
            agent.speed = Speed;
            animator.SetFloat("Velocity", isRunning ? 1.0f : 0.5f);
        }

        Vector3 curMove = transform.position - previousPosition;
        curSpeed = curMove.magnitude / Time.deltaTime;
        previousPosition = transform.position;

        if (Debugging) Debug.Log(curSpeed);
    }
}
