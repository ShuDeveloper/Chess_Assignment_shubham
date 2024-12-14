using Chess.Scripts.Core;
using System.Collections.Generic;
using UnityEngine;

public class HighlightMoves : MonoBehaviour
{
    private GameObject playerSelected;
    public List<GameObject> blackPlayers;
    bool highlight = false;


    void Update()
    {
        // Create a ray from the camera to the mouse position
        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);

        if (Input.GetMouseButtonDown(0))
        {
            // Disable previous highlights
            if (highlight)
            {
                ObjectPooling.SharedInstance.isDisable();
                highlight = false;
            }
            else
            {
                if (hit.collider != null)
                {
                    playerSelected = hit.collider.gameObject;
                    ChessPlayerPlacementHandler handler = hit.transform.GetComponent<ChessPlayerPlacementHandler>();
                    //print(hit.collider.gameObject.name);               
                    Highlight(hit.transform.name, handler.row, handler.column);
                    highlight = true;
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            highlight = false;

            if (hit.collider != null && playerSelected != null)
            {
                ChessPlayerPlacementHandler PlayerIndex = playerSelected.GetComponent<ChessPlayerPlacementHandler>();
                ChessPlayerPlacementHandler highlightIndex = hit.collider.gameObject.GetComponent<ChessPlayerPlacementHandler>();

                if (hit.collider.gameObject.tag == "HightLight")
                {
                    //Update the selected player of position and row/column
                    playerSelected.transform.position = hit.collider.gameObject.transform.position;
                    PlayerIndex.row = highlightIndex.row;
                    PlayerIndex.column = highlightIndex.column;

                    // Disable highlights
                    ObjectPooling.SharedInstance.isDisable();
                }

            }
        }
    }

    void Highlight(string name, int row, int column)
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
                if (IsPlayerAtPosition(row, column)) break;
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
            { -1, -1 }, { -1, 0 }, { -1, 1 },{ 0, -1 },{ 0, 1 },{ 1, -1 }, { 1, 0 }, { 1, 1 }
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
        bool top_right, top_left, bottom_right, bottom_left;
        top_right = top_left = bottom_left = bottom_right = false;
        // Highlight diagonal moves (like a bishop)
        for (int i = 1; i < 8; i++)
        {
            // Top-right diagonal
            if (row + i < 8 && column + i < 8 && !top_right)
            {
                if (IsPlayerAtPosition(row + i, column + i)) top_right = true;
                ChessBoardPlacementHandler.Instance.Highlight(row + i, column + i);
            }
            // Top-left diagonal
            if (row + i < 8 && column - i >= 0 && !top_left)
            {
                if (IsPlayerAtPosition(row + i, column - i)) top_left = true;
                ChessBoardPlacementHandler.Instance.Highlight(row + i, column - i);
            }
            // Bottom-right diagonal
            if (row - i >= 0 && column + i < 8 && !bottom_right)
            {
                if (IsPlayerAtPosition(row - i, column + i)) bottom_right = true;
                ChessBoardPlacementHandler.Instance.Highlight(row - i, column + i);
            }
            // Bottom-left diagonal
            if (row - i >= 0 && column - i >= 0 && !bottom_left)
            {
                if (IsPlayerAtPosition(row - i, column - i)) bottom_left = true;
                ChessBoardPlacementHandler.Instance.Highlight(row - i, column - i);
            }
        }
        top_right = top_left = bottom_left = bottom_right = false;
    }
    void HighlightRookMoves(int row, int column)
    {
        // Highlight vertical and horizontal moves 
        for (int i = row + 1; i < 8; i++)
        {

            if (IsPlayerAtPosition(i, column)) break;
            ChessBoardPlacementHandler.Instance.Highlight(i, column);

        }
        for (int i = row - 1; i > -1; i--)
        {
            if (IsPlayerAtPosition(i, column)) break;
            ChessBoardPlacementHandler.Instance.Highlight(i, column);

        }

        for (int i = column + 1; i < 8; i++)
        {

            if (IsPlayerAtPosition(row, i)) break;
            ChessBoardPlacementHandler.Instance.Highlight(row, i);

        }
        for (int i = column - 1; i > -1; i--)
        {
            if (IsPlayerAtPosition(row, i)) break;
            ChessBoardPlacementHandler.Instance.Highlight(row, i);

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

    // Method to check if a player is at the specified position by the row and column
    public bool IsPlayerAtPosition(int row, int column)
    {
        bool isOccupied = false;
        foreach (var item in blackPlayers)
        {
            var index = item.GetComponent<ChessPlayerPlacementHandler>();
            if (index.column == column && index.row == row)
            {
                //print("game object name:" + index.name + " , and row:" + index.row + ", column:" + index.column);
                isOccupied = true;
                break;
            }
            else isOccupied = false;
        }
        return isOccupied;
    }

}