using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkyFrightStateBehaviour : FrightStateBehaviour
{
    public Vector2 mMainScatterPosition;
    public float mMindChangeTimer;
    float mCurrentTimer;
    bool mPacman = false;


    override public void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)
    {
        base.OnStateEnter(pFSM, pStateInfo, pLayerIndex);
        mCurrentTimer = 0.0f;
        mController.pathCompletedEvent.AddListener(UpdatePath);
        mPacman = false;
        UpdatePath();
    }

    override public void OnStateUpdate(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(pFSM, stateInfo, layerIndex);
        mCurrentTimer += Time.deltaTime;
        if(mCurrentTimer >= mMindChangeTimer)
        {
            mPacman = !mPacman;
            UpdatePath();
        }

    }

    override public void OnStateExit(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(pFSM, stateInfo, layerIndex);
        mController.pathCompletedEvent.RemoveListener(UpdatePath);
    }

    void UpdatePath()
    {
        mCurrentTimer = 0.0f;
        if (mPacman)
        {
            mController.moveToLocation = new Vector2(mController.PacMan.transform.position.x, mController.PacMan.transform.position.y);
        }
        else
        {
            mController.moveToLocation = mMainScatterPosition;
        }
        mController.moveComplete();
    }

}

