using UnityEngine;
using UnityEngine.SceneManagement; // To manage scene loading

public class end : MonoBehaviour
{
    // Method called when something enters the trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.CompareTag("Player"))
        {
            // Load the game scene
            Debug.Log("Player touched the star! Starting game...");
            SceneManager.LoadScene("EndScene"); // Replace "GameScene" with your game scene name
        }
    }
}