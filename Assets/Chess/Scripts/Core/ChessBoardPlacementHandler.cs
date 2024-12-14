using System;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;
using Chess.Scripts.Core;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public sealed class ChessBoardPlacementHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] _rowsArray;
    [SerializeField] private GameObject _highlightPrefab;
    private GameObject[,] _chessBoard;

    internal static ChessBoardPlacementHandler Instance;

    private void Awake()
    {
        Instance = this;
        GenerateArray();
    }

    private void GenerateArray()
    {
        _chessBoard = new GameObject[8, 8];
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                _chessBoard[i, j] = _rowsArray[i].transform.GetChild(j).gameObject;
            }
        }
    }

    internal GameObject GetTile(int i, int j)
    {

        try
        {
            return _chessBoard[i, j];
        }
        catch (Exception)
        {
            Debug.LogError("Invalid row or column GetTile.");
            return null;
        }
    }

    internal void Highlight(int row, int col)
    {
        //GameObject tile = GetTile(row, col);
        var tile = GetTile(row, col).transform;

        if (tile == null)
        {
            Debug.LogError("Invalid row or column Highlight.");
            return;
        }

        //Instantiate(_highlightPrefab, tile.transform.position, Quaternion.identity, tile.transform);
        GameObject game = ObjectPooling.SharedInstance.GetPooledObject();
        game.GetComponent<ChessPlayerPlacementHandler>().row = row;
        game.GetComponent<ChessPlayerPlacementHandler>().column = col;

        if (game != null)
        {
            game.SetActive(true);
            game.transform.position = tile.position;
        }
        else
        {
            Debug.LogError("Error: No pooled object available.");
        }
    }


    void HighlightPiece(GameObject piece, Color color)
    {
        // Assuming you have a method to change the color of the piece
        Renderer renderer = piece.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }

    internal void ClearHighlights()
    {
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                var tile = GetTile(i, j);
                if (tile.transform.childCount <= 0) continue;
                foreach (Transform childTransform in tile.transform)
                {
                    Destroy(childTransform.gameObject);
                }
            }
        }
    }
  
    #region Highlight Testing

    // private void Start() {
    //     StartCoroutine(Testing());
    // }

    // private IEnumerator Testing() {
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(1f);
    //
    //     ClearHighlights();
    //     Highlight(2, 7);
    //     Highlight(2, 6);
    //     Highlight(2, 5);
    //     Highlight(2, 4);
    //     yield return new WaitForSeconds(1f);
    //
    //     ClearHighlights();
    //     Highlight(7, 7);
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(1f);
    // }

    #endregion
}