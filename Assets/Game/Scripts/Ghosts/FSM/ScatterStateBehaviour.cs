using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScatterStateBehaviour : GhostBehaviour
{
    public Vector2[] mScatterPositions;

    protected int mScatterIx = 0;


    override public void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)
    {
        SetupComponentReferences(pFSM);
        mController.pathCompletedEvent.AddListener(ScatterPathCompleted);
        if(mReverseDirection)
        {
            ReverseDirection();
        }
        mScatterIx = 0;
        mController.currSpeed = mController.speed;
    }

    override public void OnStateUpdate(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(mController.mIsChasing)
        {
            mReverseDirection = true;
            pFSM.SetTrigger(mController.mChase);
        }
    }

    override public void OnStateExit(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mController.pathCompletedEvent.RemoveListener(ScatterPathCompleted);
        StateExit();
    }

    protected virtual void ScatterPathCompleted()
    {
        if(mScatterIx >= mScatterPositions.Length)
        {
            mScatterIx = 0;
        }
        mController.moveToLocation = mScatterPositions[mScatterIx];
        mController.moveComplete();
        mScatterIx++;
    }


}

