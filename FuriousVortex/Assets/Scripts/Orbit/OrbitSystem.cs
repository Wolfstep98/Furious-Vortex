using System;
using UnityEngine;

public class OrbitSystem : MonoBehaviour 
{
    #region Fields & Properties
    [Header("Data")]
    [SerializeField]
    private float radius = 5.0f;
    [SerializeField]
    private float angularMotion = 1.0f;
    [SerializeField]
    private float period = 0.0f;

    [SerializeField]
    private float velocity = 0.0f;
    [SerializeField]
    private float acceleration = 0.0f;

    [Header("Parameters")]
    [SerializeField]
    private float timeScale = 1.0f;
    [SerializeField]
    private float stdGravParam = 0.0f;
    [SerializeField]
    private float distance = 10.0f;
    [SerializeField]
    private float orbitalVelocity = 0.0f;
    [SerializeField]
    private float centripetalAcceleration = 0.0f;
    [SerializeField]
    private Vector2 attraction = Vector2.zero;
    [SerializeField]
    private Vector2 direction = Vector2.zero;

    [Header("References")]
    [SerializeField]
    private Planet planet = null;
    [SerializeField]
    private Satellite satellite = null;
    #endregion

    #region Methods
    #region Initializers
    private void Start()
    {
        this.satellite.Rigidbody.MovePosition(Vector2.right * this.radius);
    }
    #endregion

    private void Update () 
	{
        //this.stdGravParam = this.planet.Gravity * (this.planet.Mass + this.satellite.Mass);

        //this.period = 2.0f * (float)Math.PI * (float)Math.Sqrt((this.radius * this.radius * this.radius) / this.stdGravParam);

        //this.velocity = this.radius * this.angularMotion;

        this.acceleration = (this.velocity * this.velocity) / this.radius;
        //this.acceleration = (this.satellite.Mass * (this.velocity * this.velocity)) / this.radius;

        this.attraction = (this.planet.transform.position - this.satellite.transform.position).normalized * this.acceleration;
        this.direction = Calculus.PerpendicularVector2(this.attraction.normalized, Side.Right).normalized * this.velocity;

        //this.orbitalVelocity = (float)(Math.Sqrt((this.planet.Gravity * (this.planet.Mass + this.satellite.Mass)) / (this.distance * this.distanceMultiplicator)));
        //this.centripetalAcceleration = (this.orbitalVelocity * this.orbitalVelocity) / (this.distance * this.distanceMultiplicator);

        //this.attraction = (this.planet.transform.position - this.satellite.transform.position).normalized * this.centripetalAcceleration;
        //this.direction = Calculus.PerpendicularVector2(this.attraction, Side.Right) * this.orbitalVelocity;
    }

    private void FixedUpdate()
    {
        this.satellite.Rigidbody.velocity = this.direction;
        this.satellite.Rigidbody.AddForce(this.attraction, ForceMode2D.Force);
        //this.satellite.Rigidbody.velocity = this.direction + this.attraction;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawRay(this.satellite.transform.position, this.attraction);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.satellite.transform.position, this.direction);
    }
    #endregion
}
