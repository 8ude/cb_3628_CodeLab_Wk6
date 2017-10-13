using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DisSoundBlock : MonoBehaviour {

	AudioSource thisSource;
	public AudioClip thisOrigClip;
	public AudioClip currentClip;

	Vector3 screenPoint, offset;

	float[] samples;

	public float degradeAmt = 0.1f;

	public int playCount;

	public float distance = 15;

	MusicToVertex mtvScript;

	// Use this for initializationi
	void Start () {

		#if UNITY_WEBGL

		//Need to redo this for web version, as GetData doesn't work 
		//(though SetData does, for some fucked reason)

		thisSource = GetComponent<AudioSource> ();
		mtvScript = GetComponent<MusicToVertex> ();
		samples = new float[thisOrigClip.samples * thisOrigClip.channels];
		thisOrigClip.GetData (samples, 0);
		currentClip = AudioClip.Create("newClip", thisOrigClip.samples, thisOrigClip.channels, thisOrigClip.frequency, false);
		currentClip.SetData(samples, 0);
		thisSource.clip = thisOrigClip;

		#else

		thisSource = GetComponent<AudioSource> ();
		mtvScript = GetComponent<MusicToVertex> ();
		samples = new float[thisOrigClip.samples * thisOrigClip.channels];
		thisOrigClip.GetData (samples, 0);
		currentClip = AudioClip.Create("newClip", thisOrigClip.samples, thisOrigClip.channels, thisOrigClip.frequency, false);
		currentClip.SetData(samples, 0);
		thisSource.clip = thisOrigClip;

		#endif
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayTone () {

		if (!thisSource.isPlaying) {
			//Debug.Log ("playing");
			thisSource.Play ();
			playCount++;
			Invoke ("DegradeAudio", thisSource.clip.length);

			DOTween.To(() => mtvScript.thisLacunarity, x => mtvScript.thisLacunarity = x, degradeAmt * 5.0f, 3.0f);
		}

	}

	void DegradeAudio() {

		for (int i = 0; i < samples.Length - 100; i +=100) {
			if (samples [i] != 0f) {
				if (i != 0 && samples [i - 1] == 0) {
					//double degredation probability if previous sample was degraded
					if (Random.value < degradeAmt * 2) {
						for (int j = 0; j < 100; j++) {
							samples [i + j] = 0f;
						}
					}
				} else if (Random.value < degradeAmt) {
					for (int j = 0; j < 100; j++) {
						samples [i + j] = 0f;
					}
				}
			}
		}

		thisSource.volume = 1.0f - degradeAmt;

		degradeAmt += Mathf.Clamp(degradeAmt, 0.01f, 0.1f);
		Mathf.Clamp (degradeAmt, 0.0f, 4.0f);

		if (degradeAmt >= 0.8f) {
			Destroy (gameObject);
		}

		currentClip.SetData (samples, 0);
		thisSource.clip = currentClip;


	}

	void OnMouseDown(){
		screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
		offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag(){
		Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
		Vector3 groundPosition = Vector3.ProjectOnPlane (cursorPosition, Vector3.up);
		transform.position = new Vector3(cursorPosition.x, 0.5f, cursorPosition.z);
	}


}
