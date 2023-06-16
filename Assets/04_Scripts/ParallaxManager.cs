using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
	[SerializeField] private Transform mainCameraPosition;
	[SerializeField] private float scrollSpeed = 1f;
	private float directionX;
	[SerializeField] private float offsetX;
	private float currentSpeedPlayer;
	[SerializeField] private Player_Controller player;
	[SerializeField] private Transform checkBackScreen;

	private void Awake()
	{
		offsetX = GetComponent<Renderer>().bounds.size.x;
	}

	private void Update()
	{
		ParallaxMove();
		ResetPosition();
		
	}

	private void ParallaxMove()
    {
		currentSpeedPlayer = player.GetMovementSpeed();
		directionX = -currentSpeedPlayer * scrollSpeed * Time.deltaTime;

		transform.position = new Vector2(transform.position.x + directionX, transform.position.y);
	}

	private void ResetPosition()
    {
		if (transform.position.x - mainCameraPosition.position.x < -offsetX)
		{
			transform.position = new Vector2(mainCameraPosition.position.x + (offsetX * 1), transform.position.y);
		}
        else if (mainCameraPosition.position.x - transform.position.x  < -offsetX)
        {
			transform.position = new Vector2(mainCameraPosition.position.x - (offsetX * 1), transform.position.y);
		}
        
    }
}
