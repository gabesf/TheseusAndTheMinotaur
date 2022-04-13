using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        LoadingMaze,
        PositioningEntities,
        TheseusTurn,
        MinotaurTurn,
        CheckForEndGame,
        TheseusVictory,
        MinotaurVictory,
        EditingMaze
    }

    public static GameState gamestate;

    public MazeBuilder mazeBuilder;
    public TheseusController theseusController;
    public MinotaurController minotaurController;

    private GridUnitHandler[,] mazeGrid;
    public List<MazeDataSO> mazes;
    public Vector2 minotaurStartingPosition;
    public int theseusStartingPositionX = 0;
    public int theseusStartingPositionY = 0;
    private int currentMazeId;


    bool firstFrame = true;

    void Start()
    {
        gamestate = GameState.LoadingMaze;
        RegisterListeners();
        
    }

    #region Event Handling
    private void RegisterListeners()
    {
        InputManagerActions.OnSelectMazeById += HandleOnSelectMazeById;
        InputManagerActions.OnEditState += HandleOnEditState;
        GameActions.OnMazeIsBuilt += HandleMazeIsBuilt;
        GameActions.OnRestartInput += HandleOnRestartInput;
    }

    private void OnDisable()
    {
        InputManagerActions.OnSelectMazeById -= HandleOnSelectMazeById;
        InputManagerActions.OnEditState -= HandleOnEditState;
        GameActions.OnMazeIsBuilt -= HandleMazeIsBuilt;
        GameActions.OnRestartInput -= HandleOnRestartInput;
    }

    private void HandleOnRestartInput()
    {
        InputManagerActions.OnSelectMazeById.Invoke(currentMazeId);
    }

    private void HandleMazeIsBuilt(GridUnitHandler[,] mazeGrid)
    {
        this.mazeGrid = mazeGrid;
        gamestate = GameState.TheseusTurn;
        //Debug.Log("Inside GameManager " + mazeGrid[0, 0].transform.name);
    }
    private void HandleOnSelectMazeById(int mazeId)
    {
        Debug.Log("Maze selected " + mazeId);
        currentMazeId = mazeId;
        LoadMaze(mazes[mazeId]);
    }

    private void HandleOnEditState(bool value)
    {
        Debug.Log("Handle EditState Changed " + value);
        if (value == true)
        {
            gamestate = GameState.EditingMaze;
        }
        if (value == false)
        {
            InputManagerActions.OnSelectMazeById.Invoke(currentMazeId);
            Debug.Log("Must Load Maze " + currentMazeId);
            //LoadMaze(mazes[currentMazeId]);
        }
    }

    #endregion



    #region Gameplay

    void Update()
    {
        if(firstFrame == true)
        {
            InputManagerActions.OnSelectMazeById.Invoke(0);
            firstFrame = false;
        }
        switch (gamestate)
        {
            case GameState.TheseusTurn:
                ProcessTheseusTurn();
                break;

            case GameState.MinotaurTurn:
                ProcessMinotaurTurn();
                break;


            case GameState.TheseusVictory:
                //Debug.Log("Game Is Finished Theseu Escaped");

                break;
            case GameState.MinotaurVictory:
                //Debug.Log("Game Is Finished Minotaur Got Theseu");
                break;
            default:
                break;
        }
    }


    private void ProcessTheseusTurn()
    {
        if (CheckForValidMove() == true)
        {
            if (CheckIfGameIsFinished() == true)
            {
                gamestate = GameState.TheseusVictory;
            }
            else
            {
                gamestate = GameState.MinotaurTurn;
            }
        }
    }

    private void ProcessMinotaurTurn()
    {
        minotaurController.HandleTurn();
        if (CheckIfGameIsFinished() == true)
        {
            gamestate = GameState.MinotaurVictory;
        }
        else
        {
            gamestate = GameState.TheseusTurn;
        }
    }

    private bool CheckIfGameIsFinished()
    {
        if (theseusController.CheckIfWon() == true || minotaurController.CheckIfWon())
        {
            return true;
        }
        return false;
    }

    private bool CheckForValidMove()
    {
        bool validMove = false;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            validMove = theseusController.TryToMove(RelativePosition.Up);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            validMove = theseusController.TryToMove(RelativePosition.Down);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            validMove = theseusController.TryToMove(RelativePosition.Left);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            validMove = theseusController.TryToMove(RelativePosition.Right);
        }

        //If theseus should wait, sending a valid move without a TryToMove call is enough
        else if (Input.GetKeyDown(KeyCode.W))
        {
            validMove = true;
        }

        //If undo is desired, sending a validMove = false will avoid state changing, so is still Theseus round 
        else if (Input.GetKeyDown(KeyCode.U))
        {
            theseusController.UndoMove();
            minotaurController.UndoMove();
            validMove = false;
        }
        return validMove;
    }

    #endregion


    #region MazeRelatedUtils
    private void LoadMaze(MazeDataSO mazeDataSO)
    {
        mazeBuilder.BuildMaze(mazeDataSO);
    }

    internal MazeDataSO GetCurrentMazeData()
    {
        return mazes[currentMazeId];
    }
    #endregion











}
