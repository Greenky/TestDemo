using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
	public float speed = 0.5f; // speed of ball;
	public float deley = 3f; // deley to reset params
	float _pathProgress;
	bool _inSecondPart;
	bool _inMove;
	Vector3 _startPos;
	Vector3 _middlePos;
	Vector3 _endPos;

	void Awake()
    {
		_startPos = transform.position;
		_endPos = transform.position;
		_pathProgress = 0;
		_inSecondPart = false;
		_inMove = false;

		// middle pos of path using Bezier curve algorithm
		_middlePos = new Vector3(0, -1.5f, 0);
	}

    void Update()
    {
		if (_inMove)
			_inMove = MoveInCurve(_startPos, _endPos); // returns need to move or not
	}

	private bool MoveInCurve(Vector3 start, Vector3 end)
	{
		if (transform.position == end) // in the end of curve
		{
			Invoke("ResetAll", deley);
			return false;
		}
		else  // not in the end of curve
		{
			Vector3 m1;
			Vector3 m2;

			// Bezier curve algorithm
			_pathProgress += Time.deltaTime * speed;
			m1 = Vector3.Lerp(_startPos, _middlePos, _pathProgress);
			m2 = Vector3.Lerp(_middlePos, _endPos, _pathProgress);
			transform.position = Vector3.Lerp(m1, m2, _pathProgress);
			return true;
		}
	}

	private void ResetAll()
	{
		GameObject.Find("GameLogic").GetComponent<GameLogic>().ResetParameters();
		Destroy(gameObject);
	}

	public void StartMove(Vector3 start, Vector3 end)
	{
		_startPos = start;
		_endPos = end;

		// middle pos between start and end, 'y'-component is the same
		_middlePos.x = (start.x + end.x) / 2;
		_middlePos.z = (start.z + end.z) / 2;

		_inMove = true;
	}


}
