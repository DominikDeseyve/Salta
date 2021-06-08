using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FieldSelectorCreator))]
public class Board : MonoBehaviour
{
    [SerializeField] private Transform bottomLeftFieldPos;

    [SerializeField] private float fieldSize;

    [SerializeField] private BoardBuilder boardBuilder;
   
    private Token[,] gameField;
    private Token selectedToken;


    private FieldSelectorCreator fieldSelectorCreator;   

    private GameController gameController;
    public const int GAMEFIELD_SIZE = 10;

    private void Awake() {
        this.fieldSelectorCreator = GetComponent<FieldSelectorCreator>();
        this.createGameField();
    }
    public  void setDependencies(GameController pGameController) {
        this.gameController = pGameController;
    }

    private void createGameField() {
        this.gameField = new Token[GAMEFIELD_SIZE, GAMEFIELD_SIZE];
    }
    public void onGameRestarted(){
        this.selectedToken = null;
        this.createGameField();
    }
    public Vector3 calcPositionFromCoords(Vector2Int pCoords) {      
        return this.bottomLeftFieldPos.position + new Vector3(pCoords.x * this.fieldSize, 0f, pCoords.y * this.fieldSize);
    }

    private Vector2Int calcCoordsFromPosition(Vector3 pPosition) {

        Debug.Log(transform.InverseTransformPoint(pPosition).x);
        int x = Mathf.FloorToInt(transform.InverseTransformPoint(pPosition).x / this.fieldSize) + GAMEFIELD_SIZE / 2;
        int y = Mathf.FloorToInt(transform.InverseTransformPoint(pPosition).z / this.fieldSize) + GAMEFIELD_SIZE / 2;
        return new Vector2Int(x, y);
    }

    public void onFieldSelected(Vector3 pPosition) {
        if(!this.gameController.isGamePlaying()) {
            Debug.Log("game is not playing");
            return;
        }
        Vector2Int coords = this.calcCoordsFromPosition(pPosition);
        Debug.Log(coords);
        Token token = getTokenOnField(coords);
        Debug.Log(token);
        Debug.Log(this.selectedToken);
        if(this.selectedToken) {
            Token jumpToken = this.getTokenThatCanJump();            
            if(token != null && this.selectedToken == token) {
                this.deselectToken();
            }
            else if(token != null && this.selectedToken != token && this.gameController.isActiveTeam(token.player)) {
                this.deselectToken();
                this.selectToken(token);
            } else if(jumpToken != null && !this.selectedToken.canJumpTo(coords)) {
                //if there is any token that can jump and selected token can not jump --> show salta                
                this.gameController.overlay.showSalta();               
            }
            else if(this.selectedToken.canJumpTo(coords)) {
                //make jump               
                this.onSelectedTokenMoved(MovementType.Jump, coords, this.selectedToken);
            }
            else if(this.selectedToken.canMoveTo(coords)) {
                //normal move
                this.onSelectedTokenMoved(MovementType.Move, coords, this.selectedToken);
            }            
        } else {
            //first, select token that should be moved
            Debug.Log("wähle stein aus, welcher bewegt werden soll");            
            if(token != null && this.gameController.isActiveTeam(token.player)) {
                this.selectToken(token);               
            }
        }
    }
    private Token getTokenThatCanJump() {
        for (int x = 0; x < GAMEFIELD_SIZE; x++) {
            for (int y = 0; y < GAMEFIELD_SIZE; y++) {              
                if (this.gameField[x, y] != null && this.gameController.isActiveTeam(this.gameField[x, y].player)) {
                    //token is from same player
                    if (this.gameField[x, y].availableJumps.Count > 0) {
                        return this.gameField[x, y];
                    }
                }                
            }
        }
        return null;
    }
    private void onSelectedTokenMoved(MovementType pMovementType, Vector2Int pCoords,Token pToken) {
        this.updateBoardOnTokenMove(pCoords, pToken.occupied, pToken, null);
        this.selectedToken.movePice(pMovementType, pCoords);
        this.deselectToken();
        this.gameController.endTurn();
    }
    private void updateBoardOnTokenMove( Vector2Int pNewCoords, Vector2Int pOldCoords, Token pNewToken, Token pOldToken) {
        this.gameField[pOldCoords.x, pOldCoords.y] = pOldToken;
        this.gameField[pNewCoords.x, pNewCoords.y] = pNewToken;
        Debug.Log(this.gameField);
    }
    private void selectToken(Token pToken) {
        Debug.Log("select Token");
        this.selectedToken = pToken;
        this.boardBuilder.selectField(pToken.occupied);
        List<Vector2Int> availableMoves = this.selectedToken.availableMoves;
        availableMoves.AddRange(this.selectedToken.availableJumps);
        this.showFieldSelectors(availableMoves);
        //this.showFieldSelectors(availableJumps);
    }
    private void deselectToken() {
        this.boardBuilder.deselectField();
        this.selectedToken = null;     
        this.fieldSelectorCreator.clearSelectors();
    }
    private void showFieldSelectors(List<Vector2Int> pSelection) {
        List<Vector3> fieldData = new List<Vector3>();
        for (int i = 0; i < pSelection.Count; i++)
        {
            Vector3 position = this.calcPositionFromCoords(pSelection[i]);
            bool isFieldFree = this.getTokenOnField(pSelection[i]) == null;
            if(isFieldFree) {
                fieldData.Add(position);
            }            
        }
        this.fieldSelectorCreator.showSelection(fieldData);
    }
    public void setTokenOnBoard(Vector2Int pCoords, Token pNewToken) {
        if(this.checkIfCoordsOnBoard(pCoords)) {
            this.gameField[pCoords.x, pCoords.y] = pNewToken;
        }

    }

    public Token getTokenOnField(Vector2Int pCoords) {
        if(this.checkIfCoordsOnBoard(pCoords)) {
            return this.gameField[pCoords.x, pCoords.y];
        }
        return null;
    }

    public bool checkIfCoordsOnBoard(Vector2 pCoords) {
        if(pCoords.x < 0 || pCoords.y < 0) {
            return false;
        }
         if(pCoords.x >= GAMEFIELD_SIZE || pCoords.y >= GAMEFIELD_SIZE) {
            return false;
        }

        return true;
    }

    public bool checkIfGameFinished(Player pPlayer) {
        TeamType teamType = pPlayer.teamType;
        //determine start of y 
        int startY;
        if(teamType == TeamType.Player1) {
            startY = 0;
        } else {
            startY = GAMEFIELD_SIZE - 3;
        }
        for (var y = startY; y < startY + 3; y+=1) {
            int startX = y % 2;
            for (var x = startX; x < GAMEFIELD_SIZE; x += 2) {
                Token token = this.getTokenOnField(new Vector2Int(x, y));                               
                if(token == null || token.getValency() != y || token.player != teamType) {
                    return false;
                }
            }
        }            
        return true;
    }

    public bool hasToken(Token pToken) {
        for (int x = 0; x < GAMEFIELD_SIZE; x++)
        {
            for (int y = 0; y < GAMEFIELD_SIZE; y++) {
                if(this.gameField[x,y] == pToken) {
                    return true;
                }
            }
        }
        return false;
    }
}
