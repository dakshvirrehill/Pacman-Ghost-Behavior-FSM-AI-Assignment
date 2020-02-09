using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfHouseStateBehaviour : GhostBehaviour
{
    public float mCollectedPelletPercent;
    public Vector2 mOutPosition;
    bool mMoving = false;

    override public void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)
    {
        SetupComponentReferences(pFSM);
        mController.pathCompletedEvent.AddListener(OutOfHousePathCompleted);
        mMoving = false;
    }

    override public void OnStateUpdate(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!mMoving)
        {
            float aCurColPercent = (float) GameDirector.Instance.mPelletsConsumed / (float) GameDirector.Instance.mTotalPellets;
            if(aCurColPercent >= mCollectedPelletPercent)
            {
                mMoving = true;
                mController.moveToLocation = mOutPosition;
                mController.moveComplete();
                if(mController.mPreviousState != mController.mCurrentState)
                {
                    mController.mCurrentState = mController.mPreviousState;
                }
            }
        }
    }

    public void OutOfHousePathCompleted()
    {
        if (mController.mCurrentState == GhostController.State.Chase)
        {
            mFSM.SetTrigger(mController.mChase);
        }
        else
        {
            mController.mPreviousState = mController.mCurrentState;
            mController.mCurrentState = GhostController.State.Scatter;
            mFSM.SetTrigger(mController.mScatter);
        }
        mMoving = false;
    }

    override public void OnStateExit(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        mController.pathCompletedEvent.RemoveListener(OutOfHousePathCompleted);
    }

}

