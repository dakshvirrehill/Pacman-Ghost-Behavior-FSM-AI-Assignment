using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyScatterStateBehaviour : ScatterStateBehaviour
{
    public float mSpeedIncPelletPercent;
    public float mChasePathUpdateTime;
    float mCurrentTimer = 0.0f;

    bool mCruiseElroy = false;
    bool mFinalSpeedInc = false;

    override public void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)
    {
        base.OnStateEnter(pFSM, pStateInfo, pLayerIndex);
        mCurrentTimer = 0.0f;
    }

    override public void OnStateUpdate(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (mController.mIsChasing)
        {
            mReverseDirection = true;
            pFSM.SetTrigger(mController.mChase);
        }

        if(!mFinalSpeedInc || !mCruiseElroy)
        {
            float aCurColPercent = (float)GameDirector.Instance.mPelletsConsumed / (float)GameDirector.Instance.mTotalPellets;
            if (aCurColPercent > mSpeedIncPelletPercent && !mCruiseElroy)
            {
                mCruiseElroy = true;
                mController.speed += mController.speed * 0.05f;
                mController.currSpeed = mController.speed;
            }
            else if (aCurColPercent > mSpeedIncPelletPercent * 2 && !mFinalSpeedInc && mCruiseElroy)
            {
                mFinalSpeedInc = true;
                mController.speed += mController.speed * 0.05f;
                mController.currSpeed = mController.speed;
            }
        }

        if(mCruiseElroy)
        {
            mCurrentTimer += Time.deltaTime;
            if (mCurrentTimer >= mChasePathUpdateTime)
            {
                ScatterPathCompleted();
            }
        }

    }


    protected override void ScatterPathCompleted()
    {
        if(!mCruiseElroy)
        {
            base.ScatterPathCompleted();
        }
        else
        {
            if (mController.moveToLocation.x != mController.PacMan.transform.position.x)
            {
                mController.moveToLocation = new Vector2(mController.PacMan.transform.position.x, mController.PacMan.transform.position.y);
                mController.moveComplete();
            }
            mCurrentTimer = 0.0f;
        }
    }
}
