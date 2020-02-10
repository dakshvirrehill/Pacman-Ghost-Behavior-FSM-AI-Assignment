using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyChaseStateBehaviour : GhostBehaviour
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
        else
        {
            if(mController.moveToLocation.x != mController.PacMan.transform.position.x)
            {
                mController.moveToLocation = new Vector2(mController.PacMan.transform.position.x, mController.PacMan.transform.position.y);
                mController.moveComplete();
            }
        }
    }

    override public void OnStateExit(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StateExit();
    }

}

