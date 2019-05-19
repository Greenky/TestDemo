using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
	public enum ColumnColors { gray, green, red };
	[SerializeField] private GameObject ballPrefub; // Ball to move between columns (prefub)
	private Transform startColumn;
	private Transform endColumn;

	void Start()
    {
		if (ballPrefub == null)
			Debug.LogError("No Ball Prefub");
		ResetParameters();
    }


    void Update()
    {
		// Quit in ESC
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();

		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // ray from camera to screen pont
			if (Physics.Raycast(ray, out hit) && hit.transform.tag == "Column")
			{
				if (startColumn == null) // check first hit
				{
					// find start column
					startColumn = hit.transform;

					// change ints color to green
					ColorController column = startColumn.gameObject.GetComponent<ColorController>();
					column.ChangeColor((int)ColumnColors.green);
				}
				else if (endColumn == null && startColumn != hit.transform) // check second hit (second column not the same with first
				{
					// find end column
					endColumn = hit.transform;

					// change ints color to red
					ColorController column = endColumn.gameObject.GetComponent<ColorController>();
					column.ChangeColor((int)ColumnColors.red);

					// Find spownpoints in columns
					Transform startPoint = startColumn.GetChild(0).transform;
					Transform endPoint = endColumn.GetChild(0).transform;

					GameObject newBall = Instantiate(ballPrefub, startPoint.position, Quaternion.identity); // create ball in first spawnpoint
					newBall.GetComponent<BallMovement>().StartMove(startPoint.position, endPoint.position); // start the ball movement
				}
			}
		}
	}

	public void ResetParameters()
	{
		// reset columns colors
		if (startColumn)
			startColumn.GetComponent<ColorController>().ChangeColor((int)ColumnColors.gray);
		if (endColumn)
			endColumn.GetComponent<ColorController>().ChangeColor((int)ColumnColors.gray);
		
		// clear columns
		startColumn = null;
		endColumn = null;
	}
}
