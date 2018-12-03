using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour 
{
    #region Fields & Properties
    [SerializeField]
    private GameObject ui = null;
	#endregion

	#region Methods
	#region Initializers
	// Use this for initialization
	void Awake () 
	{
		
	}
	#endregion
	
	public void StopGame()
    {
        Debug.Log("GameOver");
        this.ui.SetActive(true);
    }
	#endregion
}
