using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexController : MonoBehaviour
{
    #region Fields & Properties
    [Header("Parameters")]
    [SerializeField]
    private bool isActive = false;
    public bool IsActive { get { return this.isActive; } }

    [Header("Orbit")]
    [SerializeField]
    private Vector3 orbitLine = Vector3.zero;
    [SerializeField]
    private Side initialBallSide = Side.Left;

    [Header("References")]
    [SerializeField]
    private GameObject vortexInstance = null;
    [SerializeField]
    private GameObject vortexPrefab = null;
    [SerializeField]
    private BallController ballController = null;
    #endregion

    #region Methods
    #region Initializers
    private void Awake()
    {
        this.Initialize();
    }

    private void Initialize()
    {
#if UNITY_EDITOR
        if (this.vortexPrefab == null)
            Debug.LogError("[Missing Reference] - vortexPrefab is missing !");
        if (this.ballController == null)
            Debug.LogError("[Missing Reference] - ballController is missing !");
#endif
        this.InstantiateBall();
    }

    private void InstantiateBall()
    {
        this.vortexInstance = GameObject.Instantiate<GameObject>(this.vortexPrefab, Vector3.zero, Quaternion.identity);
        this.vortexInstance.SetActive(false);
    }
    #endregion

    // Update is called once per frame
    public void CustomUpdate()
    {
        if(this.isActive)
        {
            //Compute current ball side and check if it passes the lineOrbit
            Side currentBallSide = (Side)Calculus.PointSideRelativeToALine(this.vortexInstance.transform.position, this.vortexInstance.transform.position + this.orbitLine, this.ballController.Rigidbody.position);
            if(currentBallSide != this.initialBallSide)
            {
                //Start Orbit
                Debug.Log("Start Orbit");
            }
        }
    }

    public void CustomFixedUpdate()
    {

    }

    public void ActivateVortex()
    {
        if(!this.isActive)
        {
            this.vortexInstance.SetActive(true);
            this.isActive = true;
            this.ComputeOrbitLine();
        }
        else
        {
            Debug.LogWarning("[Vortex Controller] - Vortex is alreayd active.");
        }
    }

    public void DeactivateVortex()
    {
        if (this.isActive)
        {
            this.vortexInstance.SetActive(false);
            this.isActive = false;
        }
        else
        {
            Debug.LogWarning("[Vortex Controller] - Vortex is alreayd inactive.");
        }
    }

    public void UpdateVortexPosition(Vector3 position)
    {
        this.vortexInstance.transform.position = position;
    }

    #region Orbit
    private void ComputeOrbitLine()
    {
        //Find on which side of the ball velocity the vortex is.
        Vector3 ballPosition = this.ballController.Rigidbody.position;
        Vector3 ballVelocity = this.ballController.Rigidbody.velocity.normalized;

        Side vortexSide = (Side)Calculus.PointSideRelativeToALine(ballPosition, ballPosition + ballVelocity, this.vortexInstance.transform.position); //OK
        Debug.Log("Vortex Side : " + vortexSide);

        //Find the perpendicular line of the ball velocity with a good direction.
        //Invert side of the vortex because the point need to be on the other side.
        Vector3 line = Vector3.zero;
        if (vortexSide == Side.Left)
            line = Calculus.PerpendicularVector2(ballVelocity, Side.Right);
        else
            line = Calculus.PerpendicularVector2(ballVelocity, Side.Left);

        //Find on which side vortex line orbit the ball is.
        Side ballSide = (Side)Calculus.PointSideRelativeToALine(this.vortexInstance.transform.position, this.vortexInstance.transform.position + line, ballPosition);

        this.initialBallSide = ballSide;
        this.orbitLine = line;
    }

    #endregion

    #region Debug
    private void OnDrawGizmosSelected()
    {
        if(this.isActive)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawRay(this.vortexInstance.transform.position, this.orbitLine * 5.0f);
        }
    }
    #endregion

    #endregion
}
