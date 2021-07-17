using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[RequireComponent(typeof(FieldSelectorCreator))]
public class Board : MonoBehaviour
{
    [SerializeField] private Transform bottomLeftFieldPos;

    [SerializeField] private BoardLayout winBoardLayout;

    [SerializeField] private float fieldSize;

    [SerializeField] private BoardBuilder boardBuilder;

    [SerializeField] private SoundController soundController;
   
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
        Token token = getTokenOnField(coords);       
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
                this.soundController.playSalta();
                this.gameController.overlay.showSalta(this.gameController.getActivePlayer());               
            }
            else if(this.selectedToken.canJumpTo(coords)) {
                //make jump                   
                soundController.playJump();                  
                this.onSelectedTokenMoved(MovementType.Jump, coords, this.selectedToken);
            }
            else if(this.selectedToken.canMoveTo(coords)) {
                //normal move
                soundController.playMove(); 
                this.onSelectedTokenMoved(MovementType.Move, coords, this.selectedToken);
            }            
        } else {
            //first, select token that should be moved
            //Debug.Log("wähle stein aus, welcher bewegt werden soll");            
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
    }
    private void selectToken(Token pToken) {       
        this.selectedToken = pToken;
        this.boardBuilder.selectField(pToken.occupied);
        List<Vector2Int> availableMoves = this.selectedToken.availableMoves;
        List<Vector2Int> availableJumps = this.selectedToken.availableJumps;
        List<Vector2Int> allFields = new List<Vector2Int>(); //be careful on copy list, because of reference        
        allFields.AddRange(availableJumps);
        allFields.AddRange(availableMoves);      
        this.showFieldSelectors(allFields);      
    }
    private void deselectToken() {
        this.boardBuilder.deselectField();
        this.selectedToken = null;     
        this.fieldSelectorCreator.clearSelectors();
    }
    private void showFieldSelectors(List<Vector2Int> pSelection) {
        Dictionary<Vector3, Color> fieldData = new Dictionary<Vector3, Color>();    
        for (int i = 0; i < pSelection.Count; i++)
        {           
            Vector3 position = this.calcPositionFromCoords(pSelection[i]);
            bool isFieldFree = this.getTokenOnField(pSelection[i]) == null;
            bool isJumpField = this.selectedToken.availableJumps.Contains(pSelection[i]);
            bool mustJump = this.getTokenThatCanJump() != null;            
            if(isFieldFree) {
                if (mustJump && !isJumpField) {
                    fieldData.Add(position, Color.red);
                } else {
                    fieldData.Add(position, Color.green);
                }                
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
        foreach (Token token in this.gameField) {   
            if(token == null || token.player != pPlayer.teamType) {
                continue;
            }

            Vector2Int endPosition = this.calculateEndPositonOfToken(token);            
            
            if(token.occupied != endPosition) {
                return false;
            }            
        }
        return true;

        /* ALTERNATIVE BERECHNUNG DER VORHERIGEN VERSION */
        /*
        TeamType teamType = pPlayer.teamType;
       
        int[] ys;
        if (teamType == TeamType.Player1){
            ys = new int[] {9,8,7};
        } else {
            ys = new int[] {0,1,2};
        }
        
        int countY = 0;
        foreach (int y in ys) {
            int startX = y % 2;
            int countX = 0;
            for (var x = startX; x < GAMEFIELD_SIZE; x += 2) {
                int blackFieldIndex = x / 2;               
                Token token = this.getTokenOnField(new Vector2Int(x, y));     

                if(token == null) {
                    return false;
                }     
                if(token.player != teamType) {
                    return false;
                }  
                int tokenTypeValue = token.getTokenTypeValue();
                int valency = token.valency;                
                //check value of sun, moon, star
                if(countY != tokenTypeValue) {
                    return false;
                }                
                //check valency of specific token type
                if(teamType == TeamType.Player1) {                    
                    if(countX != (valency - 1)) {                        
                        return false;
                    }
                } else {
                    //inverse valency for other player
                    if(5 - valency != countX) {
                        return false;
                    }
                }                 
                countX++;
            }
            countY++;
        }            
       
        return true;
        */
    }

    public bool hasToken(Token pToken) {
        for (int x = 0; x < GAMEFIELD_SIZE; x++) {
            for (int y = 0; y < GAMEFIELD_SIZE; y++) {
                if(this.gameField[x,y] == pToken) {
                    return true;
                }
            }
        }
        return false;
    }
    public List<Token> getTokenNotInGoal(Player pPlayer) {
        List<Token> tokenList = new List<Token>();
        for (int x = 0; x < GAMEFIELD_SIZE; x++) {
            for (int y = 0; y < GAMEFIELD_SIZE; y++) {
                Token token = this.gameField[x, y];            
                if(token != null && token.player == pPlayer.teamType) {
                    int maxY;
                    int minY;
                    if(pPlayer.teamType == TeamType.Player1) {
                        maxY = 6;
                        minY = 0;
                    } else {
                        maxY = 9;
                        minY = 3;
                    }
                    if(token.occupied.y >= minY && token.occupied.y <= maxY) {
                        tokenList.Add(token);
                    }
                }
            }
        }
        return tokenList;
    }
    public Vector2Int calculateEndPositonOfToken(Token pToken) {
        TokenType tokenType = pToken.tokenType;
        TeamType tokenTeam = pToken.player;
        int tokenAmount = pToken.valency;

        for (int i = 0; i < this.winBoardLayout.getLength(); i++)
        {                       
            TeamType player = this.winBoardLayout.getPlayerAtIndex(i);
            TokenType type = this.winBoardLayout.getTokenAtIndex(i);
            int amount = this.winBoardLayout.getAmountAtIndex(i);
            
            if(tokenType == type && tokenTeam == player && tokenAmount == amount) {
                Vector2Int coords = this.winBoardLayout.GetSquareCoordsAtIndex(i);
                return coords;
            }  
        }
        return new Vector2Int(-1,-1);
    }
    public PathNode[,] getPathNodeGrid() {
        PathNode[,] grid = new PathNode[GAMEFIELD_SIZE, GAMEFIELD_SIZE];
        for (int x = 0; x < GAMEFIELD_SIZE; x++) {
            for (int y = 0; y < GAMEFIELD_SIZE; y++) {
                Token token = this.gameField[x, y];
                PathNode pathNode = new PathNode(x,y);
                if(token != null) {
                    pathNode.isWalkable = false;
                }
                grid[x, y] = pathNode;
            }
        }
        return grid;
    }

}
 