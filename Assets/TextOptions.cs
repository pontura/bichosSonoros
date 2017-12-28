using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextOptions : MonoBehaviour {

//	Data data = Data.Instance;
	Text t;
	// Use this for initialization
	void Start () {
		t = GetComponent<Text>();
		t.text = Data.Instance.config.URL_SERVER;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
