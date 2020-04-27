using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

	//Made this script so I could implement the whole UI heirarchy as a singleton.

	private static UIManager instance; 

	 void Awake() {

        if (instance != null)
        {

            Destroy(gameObject);

        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        
    }

    // Start is called before the first frame update
    void Start()
    {
    	
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
