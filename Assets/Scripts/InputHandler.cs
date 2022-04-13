using UnityEngine;

public class InputHandler : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            InputManagerActions.OnSelectMazeById.Invoke(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            InputManagerActions.OnSelectMazeById.Invoke(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            InputManagerActions.OnSelectMazeById.Invoke(2);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(GameManager.gamestate != GameManager.GameState.EditingMaze)
            {               
                InputManagerActions.OnEditState.Invoke(true);
            } else
            {
                InputManagerActions.OnEditState.Invoke(false);
            }                       
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            GameActions.OnRestartInput.Invoke();
        }
    }

}
