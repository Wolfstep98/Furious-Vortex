using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour 
{
    #region Fields & Properties
    [SerializeField]
    private BallController controller = null;
    public BallController Controller { set { this.controller = value; } }
	#endregion

	#region Methods
	#region Initializers
	// Use this for initialization
	void Awake () 
	{
		
	}
    #endregion
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            int normalX = Mathf.RoundToInt(collision.contacts[0].normal.x);
            int normalY = Mathf.RoundToInt(collision.contacts[0].normal.y);
            if (Mathf.Abs(normalX) == 1)
            {
                Vector3 temp = this.controller.Direction;
                temp.x *= -1;
                this.controller.Direction = temp;
            }
            if (Mathf.Abs(normalY) == 1)
            {
                Vector3 temp = this.controller.Direction;
                temp.y *= -1;
                this.controller.Direction = temp;
            }
        }
    }
    #endregion
}
