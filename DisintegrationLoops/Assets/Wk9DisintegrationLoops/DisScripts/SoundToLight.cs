using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundToLight : MonoBehaviour {

	public SpectrumAnalysis spectrum;

	Light myLight;

	public float dampening = 0.1f;

	public float maxIntensity = 5f;
	public float minIntensity = 1f;

	// Use this for initialization
	void Start () {
		myLight = gameObject.GetComponent<Light> ();
	}
	
	// Update is called once per frame
	void Update () {
		float energy = Mathf.Clamp( spectrum.GetEnergyFrequencyRange (1000f, 2000f), 0, 1f);

		float previousIntensity = myLight.intensity;

		myLight.intensity = Mathf.Lerp (previousIntensity, minIntensity + (energy * maxIntensity), dampening); 



	}
}
