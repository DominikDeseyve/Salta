using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellManager : MonoBehaviour
{
    public Image mOutlineImage;

    [HideInInspector]
    public Vector2Int mBoardPosition = Vector2Int.zero;
    [HideInInspector]
    public BoardManager boardManager = null;

    [HideInInspector]
    public RectTransform rectTransform = null;
    // Start is called before the first frame update
    public void Setup(Vector2Int newBoardPosition, BoardManager newBoard)
    {
        mBoardPosition = newBoardPosition;
        boardManager = newBoard;

        rectTransform = GetComponent<RectTransform>();
    }


}
