using UnityEngine;
using System.Collections;

public class ActiveStateToggler : MonoBehaviour {

 	public GameObject target;
 	public Button button;
 	public bool isActiveOnStart = false;

	void Start()
	{
		if(button) button.onClick.AddListener(ToggleObjectActive);
		target.SetActive(isActiveOnStart);
	}
	
	public void ToggleActive () {
		target.SetActive (!target.activeSelf);
	}
}
