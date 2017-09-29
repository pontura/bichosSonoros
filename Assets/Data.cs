using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;

public class Data : MonoBehaviour
{
	public builds build;
	public enum builds
	{
		RELEASE,
		DEBUG
	}

    const string PREFAB_PATH = "Data";    
    static Data mInstance = null;

	[HideInInspector]
	public Config config;
	public int bichoID;

    public static Data Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = FindObjectOfType<Data>();
            }
            return mInstance;
        }
    }
    void Awake()
    {
		config = GetComponent<Config> ();

		if(PlayerPrefs.GetString("URL_SERVER") != "")
			Data.Instance.config.URL_SERVER = PlayerPrefs.GetString("URL_SERVER");

		bichoID = 1;
		if(PlayerPrefs.GetInt("bichoID") != 0)
			bichoID = PlayerPrefs.GetInt("bichoID");

		mInstance = this;        
       
        DontDestroyOnLoad(this.gameObject);

    }

}
