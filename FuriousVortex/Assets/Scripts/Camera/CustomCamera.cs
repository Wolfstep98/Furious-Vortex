using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : MonoBehaviour 
{
    #region Fields & Properties
    [Header("Parameters")]
    [SerializeField]
    private bool isLocked = false;
    [SerializeField]
    private bool isGoingBack = false;

    [SerializeField]
    private float goBackTime = 0.5f;
    [SerializeField]
    private float goBackTimer = 0.0f;
    [SerializeField]
    private Vector2 initialPos = Vector2.zero;

    [SerializeField]
    private Vector2 lastPosition = Vector2.zero;
    [SerializeField]
    private Vector2 position = Vector2.zero;
    [SerializeField]
    private Vector2 offset = Vector2.zero;
    [SerializeField]
    private Vector2 maxLimit = Vector2.zero;
    [SerializeField]
    private Vector2 minLimit = Vector2.zero;

    [Header("References")]
    [SerializeField]
    private Transform target = null;
    public Transform Target { get { return this.target; } set { this.target = value; } }
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
        if (!this.isLocked && !this.isGoingBack)
        {
            this.ComputeNextCamPosition();
            Vector3 pos = new Vector3(this.position.x, this.position.y, -10.0f);
            this.transform.position = pos;
            this.lastPosition = this.position;
        }
        else if(this.isGoingBack)
        {
            this.ComputeNextCamPosition();
            this.goBackTimer += Time.deltaTime;
            if(this.goBackTimer >= this.goBackTime)
            {
                this.isGoingBack = false;
                this.goBackTimer = this.goBackTime;
            }
            Vector2 tempPos = Vector2.Lerp(this.initialPos, this.position, (this.goBackTimer / this.goBackTime));
            Vector3 pos = new Vector3(tempPos.x, tempPos.y, -10.0f);
            this.transform.position = pos;
        }
	}

    private void ComputeNextCamPosition()
    {
        Vector2 playerPos = target.position;
        Vector2 pos = playerPos + this.offset;
        pos = ClampXAxis(pos);
        pos = ClampYAxis(pos);
        this.position = pos;
    }

    public Vector2 ClampXAxis(Vector2 vec)
    {
        if (vec.x < this.minLimit.x)
            vec.x = this.minLimit.x;
        else if (vec.x > this.maxLimit.x)
            vec.x = this.maxLimit.x;
        return vec;
    }

    public Vector2 ClampYAxis(Vector2 vec)
    {
        if (vec.y < this.minLimit.y)
            vec.y = this.minLimit.y;
        else if (vec.y > this.maxLimit.y)
            vec.y= this.maxLimit.y;
        return vec;
    }

    public void LockCam()
    {
        this.isLocked = true;
        this.initialPos = this.position;
    }

    public void UnlockCam()
    {
        this.isLocked = false;
        this.isGoingBack = true;
        this.goBackTimer = 0.0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine( new Vector2(this.minLimit.x, this.minLimit.y), new Vector2(this.maxLimit.x, this.minLimit.y));
        Gizmos.DrawLine(new Vector2(this.minLimit.x, this.minLimit.y), new Vector2(this.minLimit.x, this.maxLimit.y));
        Gizmos.DrawLine(new Vector2(this.maxLimit.x, this.maxLimit.y), new Vector2(this.minLimit.x, this.maxLimit.y));
        Gizmos.DrawLine(new Vector2(this.maxLimit.x, this.maxLimit.y), new Vector2(this.maxLimit.x, this.minLimit.y));
    }
    #endregion
}
