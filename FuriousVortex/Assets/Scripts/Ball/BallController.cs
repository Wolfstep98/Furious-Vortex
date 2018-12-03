using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour 
{
    #region Fields & Properties
    [Header("Parameters")]
    [SerializeField]
    private bool isOrbiting = false;
    [SerializeField]
    private float speed = 10.0f;
    [SerializeField]
    private float acceleration = 0.0f;
    [SerializeField]
    private Vector3 direction = Vector3.zero;
    public Vector3 Direction { get { return this.direction; } set { this.direction = value; } }
    [SerializeField]
    private Vector3 orbitCenter = Vector3.zero;
    [SerializeField]
    private Side ballSide = Side.Right;

    [Header("Orbit")]
    [SerializeField]
    private float radius = 0.0f;

    [Header("References")]
    [SerializeField]
    new private Rigidbody2D rigidbody = null;
    public Rigidbody2D Rigidbody { get { return this.rigidbody; } }
    [SerializeField]
    private GameOver gameOver = null;
	#endregion

	#region Methods
	#region Initializers
	private void Awake () 
	{
        this.Initialize();	
	}

    private void Initialize()
    {
#if UNITY_EDITOR

#endif
    }

    public void SetupBall(Rigidbody2D rigidbody)
    {
        this.rigidbody = rigidbody;
    }

    private void Start()
    {
        this.LaunchBall(Vector3.up);
    }

    public void LaunchBall(Vector3 direction)
    {
        this.direction = direction;
        this.rigidbody.velocity = direction * this.speed;
    }
    #endregion

    #region Orbit
    public void StartOrbit()
    {
        this.isOrbiting = true;
    }

    public void SetOrbit(Vector3 orbit)
    {
        this.orbitCenter = orbit;
        this.radius = Vector2.Distance(this.rigidbody.transform.position, orbit);
        this.acceleration = (this.rigidbody.velocity.magnitude * this.rigidbody.velocity.magnitude) / this.radius;

        Vector3 right = Calculus.PerpendicularVector2((this.orbitCenter - this.rigidbody.transform.position).normalized, Side.Right);
        Debug.DrawRay(this.rigidbody.transform.position, right, Color.cyan, 5.0f);
        if(Vector3.Angle(this.rigidbody.velocity, right) < 15.0f)
        {
            this.ballSide = Side.Right;
        }
        else
        {
            this.ballSide = Side.Left;
        }
    }

    public void StopOrbit()
    {
        this.isOrbiting = false;
    }
    #endregion

    public void CustomUpdate () 
	{
        if(this.isOrbiting)
        {
            this.direction = Calculus.PerpendicularVector2((this.orbitCenter - this.rigidbody.transform.position).normalized, this.ballSide);
        }
	}

    public void CustomFixedUpdate()
    {
        this.rigidbody.velocity = this.direction * this.speed;
        if(this.isOrbiting)
        {
            this.rigidbody.AddForce((this.orbitCenter - this.rigidbody.transform.position) * this.acceleration, ForceMode2D.Force);
        }
    }

    public void AddVelocity(Vector3 direction, float force)
    {
        this.direction += (direction * force * Time.deltaTime);
    }

    public void UpdateDirection(Vector3 direction)
    {
        this.direction = direction.normalized;
    }


    #region Debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(this.rigidbody.transform.position, this.direction * this.speed);
        if (this.isOrbiting)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(this.rigidbody.transform.position, (this.orbitCenter - this.rigidbody.transform.position) * this.acceleration);
        }
    }
    #endregion
    #endregion
}
