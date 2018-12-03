using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour 
{
    #region Fields & Properties
    [SerializeField]
    private GameOver gameOver = null;
	#endregion

	#region Methods
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            this.gameOver.StopGame();
        }
    }
    #endregion
}
