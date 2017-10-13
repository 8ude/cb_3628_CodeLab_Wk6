using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (SpectrumAnalysis))]
public class MusicToVertex : MonoBehaviour {

	Transform parentTransform;

	Mesh thisMesh;

	[SerializeField] SpectrumAnalysis thisAnalyzer;

	public Material audioReactiveMaterial;

	//float lastSpectrumPoint;
	float emissionLevel;
	Color thisOffset;


	Renderer renderer;

	public float displacementStrength = 20f;
	public float returnSmoothing = 0.01f;
	//public Color originalLowColor;
	public float thisLacunarity;
	//float originalDisplacement;

	// Use this for initialization
	void Start () {

		renderer = GetComponent<Renderer> ();
		//GetAllMeshRenderersInChildren (transform.parent.gameObject);
		 

	
		thisLacunarity = audioReactiveMaterial.GetFloat ("_Lacunarity");
		thisOffset = audioReactiveMaterial.GetColor ("_Offset");
		//originalLowColor = audioReactiveMaterial.GetColor ("_LowColor");
		//Debug.Log (renderer.gameObject.name);

		//parentTransform = gameObject.transform.parent;
		//Debug.Log (parentTransform.name);
		thisAnalyzer = gameObject.GetComponent<SpectrumAnalysis> ();
		//thisMesh = gameObject.GetComponent<MeshFilter>().mesh;

		//listOfMeshRenderers = gameObject.GetComponentsInChildren<Renderer> ();

	

	}

	// Update is called once per frame
	void FixedUpdate () {
		float spectrumPointOne = thisAnalyzer.bandBuffer [1];
		float spectrumPointTwo = thisAnalyzer.bandBuffer [2];
		float spectrumPointThree = thisAnalyzer.bandBuffer [3];


		//renderer.material.SetFloat ("_Lacunarity", originalLacunarity + spectrumPointOne * displacementStrength);
		renderer.material.SetColor ("_Offset", new Color (
			thisOffset.r + spectrumPointOne * displacementStrength * 0.1f, 
			thisOffset.g + spectrumPointOne * displacementStrength * 0.1f, 
			thisOffset.b + spectrumPointTwo * displacementStrength * 0.1f, 0f));
		//thisRenderer.material.SetColor ("_LowColor", new Color(originalLowColor.r + spectrumPointOne, originalLowColor.g + spectrumPointTwo,
		//	originalLowColor.b + spectrumPointThree, 1.0f));
		renderer.material.SetFloat ("_Displacement", spectrumPointOne * displacementStrength);
		thisOffset = renderer.material.GetColor ("_Offset");
		renderer.material.SetFloat ("_Lacunarity", thisLacunarity);


	}

	/*

	void GetAllMeshRenderersInChildren(GameObject targetObject)
	{
		// Get mesh renderers in children also.
		if (targetObject.transform.childCount > 0)
		{
			for (int i = 0; i < targetObject.transform.childCount; i++)
			{
				GetAllMeshRenderersInChildren(targetObject.transform.GetChild(i).gameObject);
			}
		}
		if (targetObject.GetComponent<Renderer>() != null)
		{
			
			listOfMeshRenderers.Add(targetObject.GetComponent<Renderer>());
		}
	}
	*/

}
