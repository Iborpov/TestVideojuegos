using BehaviorTree;
using UnityEngine;

public class WaitNode : Node
{
    GhostBT ghostBT;
    UnityEngine.AI.NavMeshAgent agent;

    float time = 1f;
    float waitTime;
    bool onPoint = false;

    public WaitNode(BTree btree)
        : base(btree)
    {
        ghostBT = bTree as GhostBT;
        agent = ghostBT.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();
        bTree.SetData("OnPoint", false);
        waitTime = 0f;
    }

    public override NodeState Evaluate()
    {
        onPoint = (bool)bTree.GetData("OnPoint");
        if (onPoint) //Si esta en el punto
        {
            waitTime += Time.deltaTime;
            if (waitTime >= time)
            {
                state = NodeState.SUCCESS;
            }
            else
            {
                state = NodeState.RUNNING;
            }
        }
        else
        { //Si no eta en el punto
            waitTime = 0f; //Reinicia el tiempo de espera a 0
            state = NodeState.SUCCESS;
        }

        return state;
    }
}
