using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfHouseStateBehaviour : GhostBehaviour
{
    public float mWaitTime;
    float mCurTimer = 0;
    override public void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)
    {
        SetupComponentReferences(pFSM);
        mCurTimer = 0.0f;
    }

    override public void OnStateUpdate(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mCurTimer -= Time.deltaTime;
        if(mCurTimer <= 0.0f)
        {
            pFSM.SetTrigger("Scatter");
            mCurTimer = 0.0f;
        }
    }

    override public void OnStateExit(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}

