using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttractionBehaviour : MonoBehaviour 
{
    #region Fields & Properties
    [SerializeField]
    private float attractionForce = 10.0f;

    [SerializeField]
    private BallController ballController = null;
    #endregion

    #region Methods
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            this.ballController.AddVelocity((this.transform.position - collision.transform.position), this.attractionForce);
        }
    }
    #endregion
}
