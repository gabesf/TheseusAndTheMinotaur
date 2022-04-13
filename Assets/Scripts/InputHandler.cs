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

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            InputManagerActions.OnSelectMazeById.Invoke(3);
        }
    }

}
