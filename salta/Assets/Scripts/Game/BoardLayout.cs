using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Board/Layout")]
public class BoardLayout : ScriptableObject
{
    [Serializable]
    private class BoardSquareSetup
    {
        public Vector2Int position;
        public TokenType tokenType;

        public int amount;
        public TeamType player;
    }

    [SerializeField] private BoardSquareSetup[] boardSquares;

    public int getLength()
    {
        return boardSquares.Length;
    }


    public Vector2Int GetSquareCoordsAtIndex(int index)
    {
        return new Vector2Int(boardSquares[index].position.x, boardSquares[index].position.y);
    }
    public TokenType getTokenAtIndex(int index)
    {
        return boardSquares[index].tokenType;
    }
    public TeamType getPlayerAtIndex(int index)
    {
        return boardSquares[index].player;
    }
    public int getAmountAtIndex(int index) {
        return boardSquares[index].amount;
    }

}