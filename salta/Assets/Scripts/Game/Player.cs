using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player {
    public TeamType teamType { get; set; }

    public Board board { get; set; }

    public List<Token> activeTokens { get; private set; }

    public string name{ get; set; }

    public Player(TeamType pTeamType, Board pBoard) {
        this.teamType = pTeamType;
        this.board = pBoard;
        this.activeTokens = new List<Token>();
    }

    public void onGameRestarted() {
        this.activeTokens.Clear();
    }

    public void addToken(Token pToken) {
        if(!this.activeTokens.Contains(pToken)) {
            this.activeTokens.Add(pToken);
        }
    }

    public void removeToken(Token pToken) {
        if(this.activeTokens.Contains(pToken)) {
            this.activeTokens.Remove(pToken);
        }
    }

    public void generateAllAvailableMoves() {
        foreach (var token in this.activeTokens) 
        {
            if(this.board.hasToken(token)) {
                token.selectAvailableSquares();
            }
        }
    }
}