using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexInput : MonoBehaviour 
{
    #region Fields & Properties
    [Header("References")]
    [SerializeField]
    private VortexController controller = null;
    [SerializeField]
    private Camera mainCamera = null;
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
        if (this.controller == null)
            Debug.LogError("[Missing Reference] - controller is missing !");
        if (this.mainCamera == null)
            Debug.LogError("[Missing Reference] - mainCamera is missing !");
#endif
    }
	#endregion
	
	public void CustomUpdate () 
	{
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 vortexPos = this.mainCamera.ScreenToWorldPoint(Input.mousePosition);
            vortexPos.z = 0.0f;
            this.controller.UpdateVortexPosition(vortexPos);

            this.controller.ActivateVortex();

        }
        else if (Input.GetMouseButtonUp(0))
        {
            this.controller.DeactivateVortex();
        }  
#else
        if(Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];

            if(touch.phase == TouchPhase.Began)
            {
                this.controller.ActivateVortex();
                this.controller.UpdateVortexPosition(this.mainCamera.ScreenToWorldPoint(touch.position));
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                this.controller.DeactivateVortex();
            }
        }
#endif
    }
#endregion
}
