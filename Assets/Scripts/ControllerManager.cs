using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // Required for UI components

public class ControllerManager : MonoBehaviour
{
    public List<PlayerBase> players = new List<PlayerBase>();

    // Delay time between jumps for each player
    public float jumpDelay = 0.2f;  // Delay between jumps (adjust to your preference)

    // Reference to the button in the Canvas
    public Button jumpButton;

    // A flag to track if jump action is currently being processed
    private bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        // Check if the button is assigned, then add listener
        if (jumpButton != null)
        {
            jumpButton.onClick.AddListener(OnJumpButtonClicked);  // Add listener to button
        }
        else
        {
            Debug.LogError("Jump button not assigned!");
        }

        Debug.Log("ControllerManager started, number of players: " + players.Count);
    }

    // Method called when the button is clicked
    public void OnJumpButtonClicked()
    {
        if (!isJumping)
        {
            isJumping = true;  // Start the jump sequence
            StartCoroutine(JumpForAllPlayersWithDelay());
        }
    }

    // Coroutine to trigger jumps for all players with a delay between them
    IEnumerator JumpForAllPlayersWithDelay()
    {
        // Sort players so that the player furthest to the right (highest x value) is first in the list
        SortPlayersByPosition();

        for (int i = 0; i < players.Count; i++)
        {
            // Trigger the jump for the current player
            players[i].Jump();

            // Wait for the delay before triggering the next player's jump
            yield return new WaitForSeconds(jumpDelay);
        }

        // Once all players have jumped, reset the flag so that the jump action can be triggered again
        isJumping = false;
    }

    // Method to sort players by their x position (descending order)
    private void SortPlayersByPosition()
    {
        players.Sort((a, b) => b.transform.position.x.CompareTo(a.transform.position.x));
    }

    // Method to add a specific PlayerBase to the list
    public void AddPlayer(PlayerBase player)
    {
        if (player != null && !players.Contains(player))
        {
            players.Add(player);
            Debug.Log("Player added: " + player.name);
        }
        else
        {
            Debug.LogWarning("Player is null or already in the list!");
        }
    }
}
