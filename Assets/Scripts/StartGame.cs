using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//I created this class for a quick start menu button. We can delete when I refactor into sceneTransition

public class StartGame : MonoBehaviour
{
	public void Startgame(string sceneName){
		Application.LoadLevel(sceneName);
	}
}
