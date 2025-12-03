using UnityEngine;

public class simplePuzzle : MonoBehaviour
{

    private int currentRotationState = 0;

    void Start()
    {

        float initialZ = transform.localEulerAngles.z;

        initialZ = initialZ % 360f;


        currentRotationState = Mathf.RoundToInt(initialZ / 90f) % 4;
    }

    private void OnMouseDown()
    {
        if (!PuzzleGameController.youWin)
        {

            currentRotationState = (currentRotationState + 1) % 4;

            float targetZ = currentRotationState * 90f;


            transform.localEulerAngles = new Vector3(
                transform.localEulerAngles.x,
                transform.localEulerAngles.y,
                targetZ
            );


            Debug.Log("Rotasi diubah ke: " + targetZ + " derajat.");
        }
    }
}