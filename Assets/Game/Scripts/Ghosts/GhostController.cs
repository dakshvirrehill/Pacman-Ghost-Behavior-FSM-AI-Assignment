using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using AStarPathfinding;
using UnityEngine.Events;

public class GhostController : MonoBehaviour 
{
	public Vector2 ReturnLocation = new Vector2(0, 0);

	private Animator _animator;
    public Transform PacMan;
	public Vector2 moveToLocation;
	public float speed;


	void Start()
	{
		_animator = GetComponent<Animator>();
		GameDirector.Instance.GameStateChanged.AddListener(GameStateChanged);

		Move();
	}

    private bool pathCompleted = false;
    public UnityEvent pathCompletedEvent = new UnityEvent();

    public void Move()
	{
		List<Vector3> _path = new List<Vector3>();
		PathFinding.Instance.generatePath(transform.position, moveToLocation, _path);
		if (_path.Count >= 2)
		{
            pathCompleted = false;
			iTween.MoveTo(gameObject, iTween.Hash("position", new Vector3(_path[1].x, _path[1].y, 0),
													"speed", speed,
													"easetype", "linear",
													"oncomplete", "moveComplete"));
		}
		else
		{
            if (pathCompleted == false)
            {
                pathCompleted = true;
                pathCompletedEvent.Invoke();
            }

            StartCoroutine(WaitToMove());
		}
	}

	public IEnumerator WaitToMove()
	{
		yield return new WaitForSeconds(1);
		Move();
	}

	public void moveComplete()
	{
		if (GameDirector.Instance.gameOver == false)
		{
			Move();
		}
	}

	public void Kill()
	{
		_animator.SetBool("IsDead", true);
	}

	public void GameStateChanged(GameDirector.States _state)
	{
		switch (_state)
		{
			case GameDirector.States.enState_Normal:
				_animator.SetBool("IsGhost", false);
				break;

			case GameDirector.States.enState_PacmanInvincible:
				_animator.SetBool("IsGhost", true);
				break;

			case GameDirector.States.enState_GameOver:
				break;
		}
	}
}
