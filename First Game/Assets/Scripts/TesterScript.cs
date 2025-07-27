using UnityEngine;

public class TesterScript : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject[] pieceButtons;
    private int _pieceIndex = -1;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            OpenPanel();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelPlacement();
        }

        PlaceSquare();
    }

    /// <summary>
    /// Opens the building pieces selection panel
    /// </summary>
    private void OpenPanel()
    {
        // Create a panel to display the building pieces
        panel = new GameObject("Building Pieces Panel");
        panel.transform.SetParent(transform);

        // Add images of building pieces to the panel
        Sprite[] pieceSprites = new Sprite[]
        {
            Resources.Load<Sprite>("piece1"),
            Resources.Load<Sprite>("piece2"),
            Resources.Load<Sprite>("piece3")
        };

        pieceButtons = new GameObject[pieceSprites.Length];

        for (int i = 0; i < pieceSprites.Length; i++)
        {
            pieceButtons[i] = new GameObject("Piece Button");
            pieceButtons[i].transform.SetParent(panel.transform);
            SpriteRenderer spriteRenderer = pieceButtons[i].AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = pieceSprites[i];

            // Add collider for mouse detection
            BoxCollider2D collider2d = pieceButtons[i].AddComponent<BoxCollider2D>();

            // Add the mouse handler component
            PieceButtonHandler handler = pieceButtons[i].AddComponent<PieceButtonHandler>();
            handler.Initialize(this, i);
        }

        // Create an empty GameObject to hold the instance of the placed item
        GameObject placedItem = new GameObject("Placed Item");
        placedItem.transform.SetParent(transform);
    }

    /// <summary>
    /// Called by PieceButtonHandler when a piece is selected
    /// </summary>
    public void SelectPiece(int pieceIndex)
    {
        _pieceIndex = pieceIndex;
        Debug.Log($"Selected piece: {pieceIndex}");
    }

    /// <summary>
    /// Handles placing the selected piece at mouse position
    /// </summary>
    private void PlaceSquare()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Ensure z is 0 for 2D

            // Check if a piece has been selected
            if (_pieceIndex != -1)
            {
                // Create an instance of the placed item
                GameObject placedItemInstance = Instantiate(Resources.Load<GameObject>("placed_item"));
                placedItemInstance.transform.position = mousePosition;
                // Deactivate the panel
                if (panel != null)
                {
                    panel.SetActive(false);
                }

                // Reset selection
                _pieceIndex = -1;
            }
        }
    }

    /// <summary>
    /// Cancels current placement and destroys placed items
    /// </summary>
    private void CancelPlacement()
    {
        // Cancel the current placement if any
        GameObject[] placedSquares = GameObject.FindGameObjectsWithTag("placed_item");
        foreach (GameObject square in placedSquares)
        {
            Destroy(square);
        }

        // Reset selection
        _pieceIndex = -1;
    }
}