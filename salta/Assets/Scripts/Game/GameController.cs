using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TokenCreator))]
    public class GameController : MonoBehaviour
{
    private enum GameState { Init, Play, Finished };
    [SerializeField] private BoardLayout startingBoardLayout;
    [SerializeField] public Overlay overlay;
    [SerializeField] private RestartScreenManager restartScreenManager;

    private int count = 0;

    private GameState gameState;

    private TokenCreator tokenCreator;
    private Player whitePlayer;
    private Player blackPlayer;

    private Player activePlayer;
    [SerializeField] private Board board;
   
    private void Awake() {
        this.setDependencies();
        this.createPlayers();
    }
    private void setDependencies() {
        this.tokenCreator = GetComponent<TokenCreator>();
    }
    private void createPlayers() {
        this.whitePlayer = new Player(TeamType.Player1, this.board);
        this.whitePlayer.name = "Grün";
        this.blackPlayer = new Player(TeamType.Player2, this.board);
        this.blackPlayer.name = "Rot";
        this.activePlayer = this.whitePlayer;
    }
    void Start()
    {
        this.startNewGame(); 
    }
    
    private void startNewGame() {
        this.restartScreenManager.hideScreen();
        this.setGameState(GameState.Init);
        this.board.setDependencies(this);
        this.createTokenFromLayout(this.startingBoardLayout);
        this.activePlayer = this.whitePlayer;
        this.overlay.displayPlayer(this.activePlayer);
        this.activePlayer.generateAllAvailableMoves();
        this.setGameState(GameState.Play);
    }

    private void createTokenFromLayout(BoardLayout pBoardLayout) {
        for (int i = 0; i < pBoardLayout.getLength(); i++)
        {
            Vector2Int coords = pBoardLayout.GetSquareCoordsAtIndex(i);
            TeamType player = pBoardLayout.getPlayerAtIndex(i);
            TokenType type = pBoardLayout.getTokenAtIndex(i);
            int amount = pBoardLayout.getAmountAtIndex(i);
            this.createToken(coords, player, type, amount);
        }
    }

    private void createToken(Vector2Int pCoords, TeamType pPlayer, TokenType pType, int pAmount)
    {       
        Token token = tokenCreator.createToken(pType, pCoords, pAmount).GetComponent<Token>();
        token.setData(pCoords, pPlayer, board);

        
        Material playerMaterial = tokenCreator.getPlayerMaterial(pPlayer);
        token.SetMaterial(playerMaterial);

        this.board.setTokenOnBoard(pCoords, token);
        this.activePlayer.addToken(token);
    }

    public bool isActiveTeam(TeamType pTeamType) {
        return this.activePlayer.teamType == pTeamType;
    }
    private void changeTeam() {
        if(this.activePlayer == this.whitePlayer) {
            this.activePlayer = this.blackPlayer;
        } 
        else {
            this.activePlayer = this.whitePlayer;
        }
        this.overlay.displayPlayer(this.activePlayer);
        this.count++;
        this.overlay.displayCount(this.count);
    }
    private Player getPassivePlayer() {
        return this.activePlayer == this.whitePlayer ? this.blackPlayer : this.whitePlayer;
    }
    public void endTurn() {
        //calculate all new available moves for the next round
        this.activePlayer.generateAllAvailableMoves();
        this.getPassivePlayer().generateAllAvailableMoves();
        if(this.checkIfGameFinished()) {
            this.endGame();
        } else {
            this.changeTeam();
        }
    }    
    private bool checkIfGameFinished() {        
        return this.board.checkIfGameFinished(this.activePlayer);
    }
    private void endGame() {
        Debug.Log("GAME END!");
        int remainingMoves = this.countRemainingMoves();
        restartScreenManager.onGameFinished(activePlayer, remainingMoves);
        this.setGameState(GameState.Finished);
        
    }
    public void restartGame() {
        this.whitePlayer.activeTokens.ForEach(t => Destroy(t.gameObject));
        this.blackPlayer.activeTokens.ForEach(t => Destroy(t.gameObject));
        board.onGameRestarted();
        this.whitePlayer.onGameRestarted();
        this.blackPlayer.onGameRestarted();
        this.startNewGame();
    }
    private int countRemainingMoves() {
        return 10;
    }
    public bool isGamePlaying() {
        return this.gameState == GameState.Play;
    }
    private void setGameState(GameState pState) {
        this.gameState = pState;
    }
}
