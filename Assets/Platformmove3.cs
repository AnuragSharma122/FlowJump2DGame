using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformmove3 : MonoBehaviour
{
	float dirX, moveSpeed = 10f;
	bool moveRight = true;

	// Update is called once per frame
	void Update()
	{
		if (transform.position.x > 374)
			moveRight = false;
		if (transform.position.x < 314)
			moveRight = true;

		if (moveRight)
			transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
		else
			transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);
	}
}
