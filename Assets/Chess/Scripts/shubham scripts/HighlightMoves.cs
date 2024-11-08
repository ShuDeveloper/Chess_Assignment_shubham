using Chess.Scripts.Core;
using UnityEngine;

public class HighlightMoves : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Disable previous highlights
            ObjectPooling.SharedInstance.isDisable();

            // Create a ray from the camera to the mouse position
            Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

            if (hit.collider != null)
            {
                ChessPlayerPlacementHandler handler = hit.transform.GetComponent<ChessPlayerPlacementHandler>();
                print(hit.collider.gameObject.name);
                Hightlight(hit.transform.name, handler.row, handler.column);
            }
        }
    }

    void Hightlight(string name, int row, int column)
    {
        switch (name)
        {
            case "Pawn":
                HighlightPawnMoves(row, column); break;
            case "Rook":
                HighlightRookMoves(row, column); break;
            case "King":
                HighlightKingMoves(row, column); break;
            case "Queen":
                HighlightQueenMoves(row, column); break;
            case "Bishop":
                HighlightBishopMoves(row, column); break;
            case "Knight":
                HighlightKnightMoves(row, column); break;

            default: 
                Debug.LogError("Unknown piece name: " + name); 
                break;
        }       

    }
    void HighlightPawnMoves(int row, int column)
    {
        if (row == 1)
        {
            for (int i = 0; i < 2; i++)
            {
                row++;
                ChessBoardPlacementHandler.Instance.Highlight(row, column);
            }
        }
        else
        {
            row++;
            ChessBoardPlacementHandler.Instance.Highlight(row, column);
        }
    }
    void HighlightKingMoves(int row, int column)
    {
        // Define the possible moves for the king
        int[,] moves = new int[,]
        {
        { -1, -1 }, { -1, 0 }, { -1, 1 },
        { 0, -1 },           { 0, 1 },
        { 1, -1 }, { 1, 0 }, { 1, 1 }
        };

        // Iterate through each possible move
        for (int i = 0; i < moves.GetLength(0); i++)
        {
            int newRow = row + moves[i, 0];
            int newCol = column + moves[i, 1];

            // Check if the new position is within the bounds of the chessboard
            if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
            {
                // Highlight the valid move
                ChessBoardPlacementHandler.Instance.Highlight(newRow, newCol);
            }
        }
    }
    void HighlightQueenMoves(int row, int column)
    {
        
        HighlightRookMoves(row, column);        
        HighlightBishopMoves(row, column);
        
    }
    void HighlightBishopMoves(int row, int column)
    {

        // Highlight diagonal moves (like a bishop)
        for (int i = 1; i < 8; i++)
        {
            // Top-right diagonal
            if (row + i < 8 && column + i < 8)
            {
                ChessBoardPlacementHandler.Instance.Highlight(row + i, column + i);
            }
            // Top-left diagonal
            if (row + i < 8 && column - i >= 0)
            {
                ChessBoardPlacementHandler.Instance.Highlight(row + i, column - i);
            }
            // Bottom-right diagonal
            if (row - i >= 0 && column + i < 8)
            {
                ChessBoardPlacementHandler.Instance.Highlight(row - i, column + i);
            }
            // Bottom-left diagonal
            if (row - i >= 0 && column - i >= 0)
            {
                ChessBoardPlacementHandler.Instance.Highlight(row - i, column - i);
            }
        }
    }
    void HighlightRookMoves(int row, int column)
    {
        // Highlight vertical and horizontal moves 
        for (int i = 0; i < 8; i++)
        {
            if (i != row) // Skip the current position
            {
                ChessBoardPlacementHandler.Instance.Highlight(i, column);
            }
            if (i != column) // Skip the current position
            {
                ChessBoardPlacementHandler.Instance.Highlight(row, i);
            }
        }
    }
    void HighlightKnightMoves(int row, int column)
    {
        // Define the possible moves for the knight
        int[,] moves = new int[,]
        {
        { 2, 1 }, { 2, -1 }, { -2, 1 }, { -2, -1 },
        { 1, 2 }, { 1, -2 }, { -1, 2 }, { -1, -2 }
        };

        // Iterate through each possible move
        for (int i = 0; i < moves.GetLength(0); i++)
        {
            int newRow = row + moves[i, 0];
            int newCol = column + moves[i, 1];

            // Check if the new position is within the bounds of the chessboard
            if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
            {
                // Highlight the valid move
                ChessBoardPlacementHandler.Instance.Highlight(newRow, newCol);
            }
        }
    }
}