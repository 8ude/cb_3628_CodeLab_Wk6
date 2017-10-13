using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisToneTriggerPlane : MonoBehaviour {
	public Vector3 startPosition;
	public Vector3 endPosition;

	Rigidbody thisBody;

	public float speed;

	DisGameManager gameManager;


	// Use this for initialization
	void Start () {
		thisBody = GetComponent<Rigidbody> ();

		transform.position = startPosition;

		gameManager = FindObjectOfType<DisGameManager> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (!gameManager.paused) {
			Vector3 prevPosition = transform.position;
			thisBody.MovePosition (new Vector3 (prevPosition.x + (speed * Time.fixedDeltaTime), prevPosition.y, prevPosition.z));

			if (transform.position.x >= endPosition.x) {

				transform.position = startPosition;

			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "ToneBox"  & !gameManager.paused) {

			other.BroadcastMessage ("PlayTone", SendMessageOptions.DontRequireReceiver);

		}
	}
}
