using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisGameManager : MonoBehaviour {

	public Button uiButton;
	public GameObject ToneCubePrefab;

	public AudioClip[] Tones;

	public int maxCubes;

	int numCubes = 0;
	public bool paused = false;

	public Canvas pauseScreen;
    public static ShuffleBag<AudioClip> toneCue;


    void Awake() {
		if (!IsValidToneCue()) {
            toneCue = new ShuffleBag<AudioClip>();

			AddTonesToCue();
		}
    }

	// Use this for initialization
	void Start () {

		PauseGame ();
		
	}

	// Update is called once per frame
	void Update () {
		
		numCubes = FindObjectsOfType<DisSoundBlock> ().Length;

		if (Input.GetKeyDown (KeyCode.Space)) {
			SpawnCube();
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			PauseGame ();
		}


	}

	public void SpawnCube () {

		int toneInt = Random.Range (0, Tones.Length);

		GameObject newToneCube = Instantiate (ToneCubePrefab, new Vector3 (Random.Range (-10f, 10f), 0.5f, Random.Range (-10f, 10f)), Quaternion.identity);
        newToneCube.GetComponent<DisSoundBlock>().thisOrigClip = toneCue.Next();
        Debug.Log(toneCue.Cursor);
		newToneCube.GetComponent<Renderer>().material.SetColor("_LowColor", new Color((float)toneInt * (1f/(float)Tones.Length), (float)toneInt * (1f/(float)Tones.Length), (float)toneInt * (1f/(float)Tones.Length)));

	}

    bool IsValidToneCue() {
        return toneCue != null;
    }

	void PauseGame() {
		if (paused) {
			pauseScreen.enabled = false;
			paused = !paused;
			//Time.timeScale = 1.0f;
		} else {
			pauseScreen.enabled = true;
			paused = !paused;
			//Time.timeScale = 0.0f;
		}
	}

    void AddTonesToCue() {
        foreach(AudioClip toneClip in Tones) {
            toneCue.Add(toneClip);
        }   
    }

}
