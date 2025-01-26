using UnityEngine;
using UnityEngine.SceneManagement; // To manage scene loading

public class CreditsButton : MonoBehaviour
{
    // Method called when something enters the trigger collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is the player
        if (collision.CompareTag("Player"))
        {
            // Load the credits scene
            Debug.Log("Player touched the star! Starting credits...");
            SceneManager.LoadScene("SampleScene2"); // Replace "Credits" with your credits scene name
        }
    }
}