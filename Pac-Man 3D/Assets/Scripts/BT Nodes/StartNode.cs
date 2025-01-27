using BehaviorTree;
using UnityEngine;

public class StartNode : Node
{
    GhostBT ghostBT;
    UnityEngine.AI.NavMeshAgent agent;

    float exitTime;

    public StartNode(BTree btree)
        : base(btree)
    {
        ghostBT = bTree as GhostBT;
        agent = ghostBT.transform.GetComponent<UnityEngine.AI.NavMeshAgent>();

        exitTime = ghostBT.exitTime + Time.time;
    }

    public override NodeState Evaluate()
    {
        Debug.Log(ghostBT.start);
        if (ghostBT.start) //Si esta en la salida
        {
            if (exitTime <= Time.time) //Si ha pasado el tiempo de espera
            {
                agent.destination = ghostBT.points[0].transform.position; //Se dirige al primer punto
                ghostBT.start = false; //Deja de estar en la salida
                state = NodeState.SUCCESS;
            }
            else
            { //Espera a su tiempo de salida
                state = NodeState.RUNNING;
            }
        }
        else
        { //No esta en la salida
            exitTime = ghostBT.exitTime + Time.time;
            state = NodeState.FAILURE;
        }
        return state;
    }
}
