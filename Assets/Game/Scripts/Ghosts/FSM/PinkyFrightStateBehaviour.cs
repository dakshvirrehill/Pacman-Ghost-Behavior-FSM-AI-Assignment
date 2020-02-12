using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkyFrightStateBehaviour : FrightStateBehaviour
{

    override public void OnStateEnter(Animator pFSM, AnimatorStateInfo pStateInfo, int pLayerIndex)
    {
        base.OnStateEnter(pFSM, pStateInfo, pLayerIndex);
        mController.pathCompletedEvent.AddListener(UpdatePath);
        UpdatePath();
    }

    //override public void OnStateUpdate(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    override public void OnStateExit(Animator pFSM, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(pFSM, stateInfo, layerIndex);
        mController.pathCompletedEvent.RemoveListener(UpdatePath);
    }

    void UpdatePath()
    {
        mController.moveToLocation = new Vector2(mController.PacMan.transform.position.x, mController.PacMan.transform.position.y);
        mController.moveComplete();
    }

}

