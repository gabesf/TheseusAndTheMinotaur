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

    public int theseusStartingPositionX = 0;
    public int theseusStartingPositionY = 0;

    public Vector2 minotaurStartingPosition;
    private GridUnitHandler[,] mazeGrid;
    private bool theseusTurn;

    public List<MazeDataSO> mazes;
    // Start is called before the first frame update
    void Start()
    {
        gamestate = GameState.LoadingMaze;

        //gamestate = GameState.EditingMaze;

        Debug.Log(theseusController.transform.name);

        InputManagerActions.OnSelectMazeById += HandleOnSelectMazeById;

    }

    private void HandleOnSelectMazeById(int mazeId)
    {
        Debug.Log("Maze selected " + mazeId);
        LoadMaze(mazes[mazeId]);
    }

    // Update is called once per frame
    void Update()
    {
        //Could use a state machine for this, but for the sake of simplicity and not remembering by heart, will use a switch.
        switch (gamestate)
        {
            case GameState.EditingMaze:

                break;
            
            case GameState.PositioningEntities:
                theseusController.SetPosition(theseusStartingPositionX, theseusStartingPositionY);
                minotaurController.SetPosition(0, 0);
                gamestate = GameState.TheseusTurn;
                break;

            case GameState.TheseusTurn:
                
                if(CheckForInput() == true)
                {
                    if(CheckIfGameIsFinished() == true)
                    {
                        gamestate = GameState.TheseusVictory;
                    } else
                    {
                        gamestate = GameState.MinotaurTurn;
                    }
                    
                }
                //wait for input
                //perform move
                break;

            case GameState.MinotaurTurn:
                Debug.Log("MinotaurTurn");
                minotaurController.HandleTurn();

                if(CheckIfGameIsFinished() == true)
                {
                    gamestate = GameState.MinotaurVictory;
                } else
                {
                    gamestate = GameState.TheseusTurn;
                }
                //check best move 
                //perform move
                break;

 
            case GameState.TheseusVictory:
                Debug.Log("Game Is Finished Theseu Escaped");
 
                break;
            case GameState.MinotaurVictory:
                Debug.Log("Game Is Finished Minotaur Got Theseu");
                //restart
                break;
            default:
                break;
        }

    }

    private bool CheckIfGameIsFinished()
    {
        if(theseusController.CheckIfWon() == true || minotaurController.CheckIfWon())
        {
            return true;
        }

        return false;
    }

    private bool CheckForInput()
    {
        bool validMove = false;
        if (Input.GetKeyDown(KeyCode.E))
        {
            gamestate = GameState.EditingMaze;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            validMove = theseusController.TryToMove(RelativePosition.Up);
            Debug.Log($"Tried to move up and was {validMove}");
        } 
        
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            validMove = theseusController.TryToMove(RelativePosition.Down);
            Debug.Log($"Tried to move down and was {validMove}");
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            validMove = theseusController.TryToMove(RelativePosition.Left);
            Debug.Log($"Tried to move left and was {validMove}");

        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            validMove = theseusController.TryToMove(RelativePosition.Right);
            Debug.Log($"Tried to move right and was {validMove}");

        }



        return validMove;
    }

    private void LoadMaze(MazeDataSO mazeDataSO)
    {
        Debug.Log("Will call Build Maze inside GameManager ");
        mazeBuilder.BuildMaze(mazeDataSO);
        mazeGrid = mazeBuilder.MazeGrid;
        Debug.Log("Inside GameManager " + mazeGrid[0, 0].transform.name);
        //theseusController.SetMaze(mazeGrid);
        //minotaurController.SetMaze(mazeGrid);
    }

    
}
