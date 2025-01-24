using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using Unity.VisualScripting;
using UnityEngine;

public class WaitNode : Node
{
    GhostBT ghostBT;
    UnityEngine.AI.NavMeshAgent agent;

    float exitTime;
    int point;
    float time = 1f;
    float distance;

    public WaitNode(BTree btree)
        : base(btree)
    {
        ghostBT = bTree as GhostBT;
        agent = ghostBT.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public override NodeState Evaluate()
    {
        Debug.Log("Wait Node ----------------------------------");
        exitTime = ghostBT.exitTime;
        point = ghostBT.point;
        exitTime = exitTime + Time.time;
        time = time + Time.time;

        Debug.Log(point + "p wait");
        if (point == 0)
        {
            if (exitTime <= Time.time)
            {
                state = NodeState.SUCCESS;
            }
            else
            {
                state = NodeState.RUNNING;
            }
        }
        else
        {
            distance = (float)bTree.GetData("distance");
            if (distance == 0)
            {
                if (time <= Time.time)
                {
                    Debug.Log(time + "s");
                    state = NodeState.SUCCESS;
                }
                else
                {
                    state = NodeState.RUNNING;
                }
            }
        }

        return state;
    }
}
