using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkyFrightStateBehaviour : FrightStateBehaviour
{
    public Vector2[] mOscilPoints;
    int mCurIx = -1;

    override public void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)
    {
        base.OnStateEnter(pFSM, pStateInfo, pLayerIndex);
        mController.pathCompletedEvent.AddListener(ChangePath);
        mCurIx = -1;
        ChangePath();
    }

    //override public void OnStateUpdate(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    override public void OnStateExit(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(pFSM, stateInfo, layerIndex);
        mController.pathCompletedEvent.RemoveListener(ChangePath);
    }

    void ChangePath()
    {
        mCurIx = (mCurIx + 1) % mOscilPoints.Length;
        mController.moveToLocation = mOscilPoints[mCurIx];
        mController.moveComplete();
    }

}

