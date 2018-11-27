using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour 
{
    #region Fields & Properties
    [Header("References")]
    [SerializeField]
    private BallInput input = null;
    [SerializeField]
    private BallController controller = null;
    #endregion

    #region Methods
    #region Initializers
    // Use this for initialization
    void Awake () 
	{
		
	}
	#endregion
	
	// Update is called once per frame
	private void Update () 
	{
        //Input
        this.input.CustomUpdate();

        //Controller
        this.controller.CustomUpdate();
	}

    private void FixedUpdate()
    {
        //Controller
        this.controller.CustomFixedUpdate();
    }
    #endregion
}
