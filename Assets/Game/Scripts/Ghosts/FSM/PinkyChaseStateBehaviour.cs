using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyChaseStateBehaviour : GhostBehaviour
{

    override public void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)
    {
        SetupComponentReferences(pFSM);
        if (mReverseDirection)
        {
            ReverseDirection();
        }

    }

    override public void OnStateUpdate(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!mController.mIsChasing)
        {
            mReverseDirection = true;
            pFSM.SetTrigger(mController.mScatter);
        }

    }

    override public void OnStateExit(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StateExit();
    }

}

