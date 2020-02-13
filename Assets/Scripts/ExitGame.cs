using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I created this class for a quick exit game button.

public class ExitGame : MonoBehaviour
{
	public void Exitgame(string sceneName){
    UnityEditor.EditorApplication.isPlaying = false;
		Application.Quit();
	}
}
