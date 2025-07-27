using UnityEngine;

/// <summary>
/// Handles mouse events for piece selection buttons
/// </summary>
public class PieceButtonHandler : MonoBehaviour
{
    private TesterScript _testerScript;
    private int _pieceIndex;

    /// <summary>
    /// Initialize the button handler with reference to main script and piece index
    /// </summary>
    /// <param name="tester">Reference to the TesterScript</param>
    /// <param name="index">Index of the piece this button represents</param>
    public void Initialize(TesterScript tester, int index)
    {
        _testerScript = tester;
        _pieceIndex = index;
    }

    /// <summary>
    /// Called when mouse enters the collider
    /// </summary>
    private void OnMouseEnter()
    {
        // Visual feedback - highlight the button
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.yellow;
        }
    }

    /// <summary>
    /// Called when mouse exits the collider
    /// </summary>
    private void OnMouseExit()
    {
        // Reset visual feedback
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }

    /// <summary>
    /// Called when mouse clicks on the collider
    /// </summary>
    private void OnMouseDown()
    {
        if (_testerScript != null)
        {
            _testerScript.SelectPiece(_pieceIndex);
        }
    }
}