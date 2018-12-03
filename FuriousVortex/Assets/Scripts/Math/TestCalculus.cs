using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCalculus : MonoBehaviour 
{
    #region Fields & Properties
    [SerializeField]
    private float side = 0.0f;

    [SerializeField]
    private Transform point = null;
    [SerializeField]
    private Transform line = null;

    [SerializeField]
    private Text text = null;
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
        this.side = Calculus.PointSideRelativeToALine(Vector3.zero, this.line.position, this.point.position);
        Debug.DrawRay(Vector3.zero, this.line.position, Color.black);
        this.text.text = this.side.ToString();
	}
    #endregion
}
