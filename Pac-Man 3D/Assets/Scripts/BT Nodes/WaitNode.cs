using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class WaitNode : Node
{
    GhostBT ghostBT;
    UnityEngine.AI.NavMeshAgent agent;

    float exitTimePassed = 0f;
    float exitTime;
    int point;
    float timePassed = 0f;
    float time = 1f;

    public WaitNode(BTree btree)
        : base(btree)
    {
        ghostBT = bTree as GhostBT;
        agent = ghostBT.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        exitTime = ghostBT.exitTime;
        exitTimePassed += Time.deltaTime;

        if (exitTimePassed > exitTime) //Tiempo para salir de base
        {

            timePassed += Time.deltaTime;
            if (timePassed > time)
            {
                timePassed = 0;
                point++;
                if (point >= 4)
                {
                    point = 0;
                }
            }
        }
        else
        {
            state = NodeState.RUNNING;
        }
        ;
        return state;
    }
}
