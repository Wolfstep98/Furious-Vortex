using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallInput : MonoBehaviour 
{
    #region Fields & Properties
    [Header("References")]
    [SerializeField]
    private BallController controller = null;
    [SerializeField]
    private Transform spawnPoint = null;
    [SerializeField]
    private GameObject ballPrefab = null;
    [SerializeField]
    private GameObject ballInstance = null;
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
        if (this.controller == null)
            Debug.LogError("[Missing Reference] - controller is missing !");
        if (this.spawnPoint == null)
            Debug.LogError("[Missing Reference] - spawnPoint is missing !");
#endif

        this.InstantiateBall();
    }

    private void InstantiateBall()
    {
        this.ballInstance = GameObject.Instantiate<GameObject>(this.ballPrefab, this.spawnPoint.position, Quaternion.identity);
        this.controller.SetupBall(this.ballInstance.GetComponent<Rigidbody2D>());
    }
    #endregion

    public void CustomUpdate () 
	{
		
	}
	#endregion
}
