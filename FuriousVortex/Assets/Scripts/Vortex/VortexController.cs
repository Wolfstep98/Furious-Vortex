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
    [SerializeField]
    private bool isOrbitActive = false;

    [Header("Orbit")]
    [SerializeField]
    private float vortexRelativeToBall = 0.0f;
    [SerializeField]
    private float ballRelativeToVortex = 0.0f;
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
    [SerializeField]
    private BallInput ballInput = null;
    [SerializeField]
    private GameOver gameOver = null;
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
        this.InstantiateVortex();
    }

    private void InstantiateVortex()
    {
        this.vortexInstance = GameObject.Instantiate<GameObject>(this.vortexPrefab, Vector3.zero, Quaternion.identity);
        this.vortexInstance.SetActive(false);
        this.vortexInstance.GetComponent<Vortex>().GameOver = this.gameOver;
    }
    #endregion

    // Update is called once per frame
    public void CustomUpdate()
    {
        if(this.isActive)
        {
            //Compute current ball side and check if it passes the lineOrbit
            this.ballRelativeToVortex = Calculus.PointSideRelativeToALine(this.vortexInstance.transform.position, this.vortexInstance.transform.position + this.orbitLine.normalized, this.ballController.Rigidbody.position);

            Side currentBallSide;
            if (this.ballRelativeToVortex > 0)
                currentBallSide = Side.Left;
            else if (this.ballRelativeToVortex < 0)
                currentBallSide = Side.Right;
            else
                currentBallSide = Side.Collinear;

            if (currentBallSide != this.initialBallSide && !this.isOrbitActive)
            {
                if (Vector2.Distance(this.ballController.Rigidbody.position, this.vortexInstance.transform.position) <= 1.35f)
                {
                    //Start Orbit
                    Debug.Log("Start Orbit" + this.ballController.Rigidbody.transform.position);
                    this.isOrbitActive = true;
                    this.ballInput.StartOrbit(this.vortexInstance.transform.position);
                }
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
            if(this.isOrbitActive)
            {
                this.isOrbitActive = false;
                this.ballInput.StopOrbit();
            }
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

        this.vortexRelativeToBall = Calculus.PointSideRelativeToALine(ballPosition, ballPosition + ballVelocity, this.vortexInstance.transform.position);

        Side vortexSide;
        if (this.vortexRelativeToBall > 0)
            vortexSide = Side.Left;
        else if (this.vortexRelativeToBall < 0)
            vortexSide = Side.Right;
        else
            vortexSide = Side.Collinear;
        Debug.Log("Vortex Side : " + vortexSide);

        //Find the perpendicular line of the ball velocity with a good direction.
        //Invert side of the vortex because the point need to be on the other side.
        Vector3 line = Vector3.zero;
        if (vortexSide == Side.Left)
            line = Calculus.PerpendicularVector2(ballVelocity, Side.Right);
        else if(vortexSide == Side.Right)
            line = Calculus.PerpendicularVector2(ballVelocity, Side.Left);
        else
            line = Calculus.PerpendicularVector2(ballVelocity, Side.Right);

        //Find on which side vortex line orbit the ball is.
        this.ballRelativeToVortex = Calculus.PointSideRelativeToALine(this.vortexInstance.transform.position, this.vortexInstance.transform.position + line, ballPosition);
        Side ballSide; 
        if (this.ballRelativeToVortex > 0)
            ballSide = Side.Left;
        else if (this.ballRelativeToVortex < 0)
            ballSide = Side.Right;
        else
            ballSide = Side.Collinear;

        this.initialBallSide = ballSide;
        this.orbitLine = line.normalized;
    }

    #endregion

    #region Debug
    private void OnDrawGizmosSelected()
    {
        if(this.isActive)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawRay(this.vortexInstance.transform.position, this.orbitLine.normalized * 5.0f);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(this.ballController.Rigidbody.transform.position, this.orbitLine.normalized * 5.0f);
        }
    }
    #endregion

    #endregion
}
