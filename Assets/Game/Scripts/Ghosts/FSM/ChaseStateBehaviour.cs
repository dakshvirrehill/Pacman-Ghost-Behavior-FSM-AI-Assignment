using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseStateBehaviour : GhostBehaviour
{
    public float mChasePathUpdateTime;
    protected float mCurrentTimer;


    override public void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)
    {
        SetupComponentReferences(pFSM);
        if (mReverseDirection)
        {
            ReverseDirection();
        }
        mCurrentTimer = 0.0f;
        mController.pathCompletedEvent.AddListener(UpdatePath);
        mController.currSpeed = mController.speed;
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
            mCurrentTimer += Time.deltaTime;
            if (mCurrentTimer >= mChasePathUpdateTime)
            {
                UpdatePath();
            }
        }
    }

    override public void OnStateExit(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        StateExit();
        mController.pathCompletedEvent.RemoveListener(UpdatePath);
    }




    protected virtual void UpdatePath()
    {

    }

}
