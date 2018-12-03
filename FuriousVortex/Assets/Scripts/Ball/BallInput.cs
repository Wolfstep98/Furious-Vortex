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
    private CustomCamera customCam = null;
    [SerializeField]
    private ScoreManager scoreManager = null;
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
        if (this.customCam == null)
            Debug.LogError("[Missing Reference] - customCam is missing !");
        if (this.spawnPoint == null)
            Debug.LogError("[Missing Reference] - spawnPoint is missing !");
#endif

        this.InstantiateBall();
    }

    private void InstantiateBall()
    {
        this.ballInstance = GameObject.Instantiate<GameObject>(this.ballPrefab, this.spawnPoint.position, Quaternion.identity);
        this.ballInstance.GetComponent<Ball>().Controller = this.controller;
        this.controller.SetupBall(this.ballInstance.GetComponent<Rigidbody2D>());
        this.customCam.Target = this.ballInstance.transform;
        this.scoreManager.Player = this.ballInstance.transform;
    }
    #endregion

    public void StartOrbit(Vector3 orbit)
    {
        this.controller.StartOrbit();
        this.controller.SetOrbit(orbit);
    }

    public void StopOrbit()
    {
        this.controller.StopOrbit();
    }

    public void CustomUpdate () 
	{
		
	}
	#endregion
}
