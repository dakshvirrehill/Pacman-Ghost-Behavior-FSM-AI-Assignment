using AStarPathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClydeChaseStateBehaviour : ChaseStateBehaviour
{
    public int mMaxBlocks;
    public Vector2 mMainScatterPos;

    protected override void UpdatePath()
    {
        Vector2 aPacmanCurrentPos = new Vector2(mController.PacMan.transform.position.x, mController.PacMan.transform.position.y);
        List<Vector3> _path = new List<Vector3>();
        PathFinding.Instance.generatePath(mGhostTransform.position, aPacmanCurrentPos, _path);
        if(_path.Count > mMaxBlocks)
        {
            mController.moveToLocation = aPacmanCurrentPos;
            mController.moveComplete();
        }
        else
        {
            mController.moveToLocation = mMainScatterPos;
            mController.moveComplete();
        }
        mCurrentTimer = 0.0f;
    }

}

