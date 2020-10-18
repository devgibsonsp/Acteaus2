using System.Collections;	
using System.Collections.Generic;
using UnityEngine;

/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * *
 *	Building fader controls the fading of the outside of buildings, so that the interior of the buildings can    *
 *  be displayed                                                                                                 *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

public class BuildingFader : MonoBehaviour {

	private bool fading;
	public Color fadeColor;
	public Color defaultColor;
	private Material buildingMat;
	
	// Use this for initialization
	void Start () {
		fading = false;
		buildingMat = GetComponent<Renderer>().material;
		defaultColor = buildingMat.GetColor("_Color");
	}
	public void SetFadeStatus(bool f){
		fading = f;
	}
	void Update(){
		if(fading){
			buildingMat.SetColor("_Color", fadeColor);
		} else {
			buildingMat.SetColor("_Color", defaultColor);
		}
	}
	

}
