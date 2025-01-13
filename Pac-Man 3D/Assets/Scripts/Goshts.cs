using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goshts : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    [SerializeField]
    GameObject target;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            navMeshAgent.destination = target.transform.position;
    }
}
