using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour 
{
    #region Fields & Properties
    [SerializeField]
    private GameOver gameOver = null;
    public GameOver GameOver { set { this.gameOver = value; } }
    #endregion

    #region Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.gameOver.StopGame();
        }
    }
    #endregion
}
