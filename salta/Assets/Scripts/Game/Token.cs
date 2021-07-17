using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectTweener))]
[RequireComponent(typeof(MaterialSetter))]
public class Token : MonoBehaviour
{
    [SerializeField] public TokenType tokenType;

    [SerializeField] public int valency;
    private MaterialSetter materialSetter;

    public Board board { protected get; set; }

    public Vector2Int occupied { get; set; }
    public TeamType player { get; set; }

   
    public bool hasMoved { get; private set; }
    public List<Vector2Int> availableMoves;

    public List<Vector2Int> availableJumps;

    private ObjectTweener tweener;

  

    private Vector2Int[] moveDirections = new Vector2Int[]{
        new Vector2Int(1,-1),
        new Vector2Int(1,1),
        new Vector2Int(-1,1),
        new Vector2Int(-1,-1),
    };
    

    public void selectAvailableSquares() {        
        this.availableMoves.Clear();
        this.availableJumps.Clear();
        foreach(var dir in this.moveDirections) {
            for (int i = 1; i < Board.GAMEFIELD_SIZE; i++) {
                Vector2Int nextCoords = this.occupied + dir * i;               
                Token token = this.board.getTokenOnField(nextCoords);
                if(!board.checkIfCoordsOnBoard(nextCoords)) {
                    break;
                }
                if (token == null)
                { //player can regulary move
                    this.addMove(nextCoords);                 
                    break;
                } else if (token.isSamePlayer(this)) {                  
                    break;
                
                } else if(!token.isSamePlayer(this)) { //salta jump
                    //normal playmode can only jump over 1 token
                    //TODO: nur noch vorne jumpen                   
                    if(token.player == TeamType.Player2 && (dir == new Vector2Int(1,1) || dir == new Vector2Int(-1,1)) ||
                    token.player == TeamType.Player1 && (dir == new Vector2Int(-1,-1) || dir == new Vector2Int(1,-1))) {                      
                        Vector2Int jumpCoords = this.occupied + dir * (i + 1);
                        Token secondToken = this.board.getTokenOnField(jumpCoords);
                        if(board.checkIfCoordsOnBoard(jumpCoords) && secondToken == null) {                           
                            this.addJump(jumpCoords);                        
                        }
                    }                    
                    break;
                } 
            }
        }        
    }

    private void Awake() {
        this.availableMoves = new List<Vector2Int>();
        this.availableJumps = new List<Vector2Int>();
        this.tweener = GetComponent<ObjectTweener>();
        this.materialSetter = GetComponent<MaterialSetter>();
        this.hasMoved = false;
    }

    public void SetMaterial(Material pMaterial) {
        this.materialSetter.SetSingleMaterial(pMaterial);
    }

    public bool isSamePlayer(Token pToken) {
        return this.player == pToken.player;
    }

    public bool canMoveTo(Vector2Int pCoords) {
        return this.availableMoves.Contains(pCoords) && this.availableJumps.Count == 0;
    }

    public bool canJumpTo(Vector2Int pCoords) {
        return this.availableJumps.Contains(pCoords);
    }
    public bool mustJump() {
        return this.availableJumps.Count > 0;
    }

    public virtual void movePice(MovementType pMovementType, Vector2Int pCoords) {
        Vector3 targetPosition = this.board.calcPositionFromCoords(pCoords);
        this.occupied = pCoords;
        this.hasMoved = true;
        if(pMovementType == MovementType.Move) {
            tweener.moveTo(transform, targetPosition);
        } 
        else if (pMovementType == MovementType.Jump) {
            tweener.jumpTo(transform, targetPosition);
        }

    }
    protected void addMove(Vector2Int pCoords) {
        this.availableMoves.Add(pCoords);
    }
    protected void addJump(Vector2Int pCoords) {
        this.availableJumps.Add(pCoords);              
    }
    public void setData(Vector2Int pCoords, TeamType pPlayer, Board pBoard) {
        this.player = pPlayer;
        this.occupied = pCoords;
        this.board = pBoard;
        transform.position = pBoard.calcPositionFromCoords(pCoords);
    }

    public int getTokenTypeValue() {
        switch (this.tokenType) {
            case TokenType.Sun:
                return 0;   
            case TokenType.Moon:
                return 1;  
            case TokenType.Star:
                return 2;           
            default:
                return -1;                
        }
    }
    
}
