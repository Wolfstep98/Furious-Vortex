using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexManager : MonoBehaviour 
{
    #region Fields & Properties
    [Header("References")]
    [SerializeField]
    private VortexInput input = null;
    [SerializeField]
    private VortexController controller = null;
    #endregion

    #region Methods
    #region Initializers
    private void Awake () 
	{
        this.Initialise();
	}

    private void Initialise()
    {
#if UNITY_EDITOR
        if (this.input == null)
            Debug.LogError("[Missing Reference] - input is missing !");
        if (this.controller == null)
            Debug.LogError("[Missing Reference] - controller is missing !");
#endif
    }
#endregion

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
