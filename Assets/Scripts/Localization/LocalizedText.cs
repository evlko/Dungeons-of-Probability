using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LocalizedText : MonoBehaviour {
	public string key;
	public bool RealtimeUpdate;
	Text txt;
	void Start()
	{
		txt = this.GetComponent<Text>();
		txt.text = LocalizationManager.instance.GetLocalizedValue(key);
	}

	private void Update()
	{
		if (RealtimeUpdate)
		{
			txt.text = LocalizationManager.instance.GetLocalizedValue(key);
		}
	}
}
