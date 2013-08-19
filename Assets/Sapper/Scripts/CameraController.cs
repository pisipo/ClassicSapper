using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private Transform Field;
    private void Start()
    {
        Field=GameObject.Find("Field").transform;
        //GestureController.OnDrag += DragCamera;
        FingerGestures.OnDragMove += DragMoveHandler;
        FingerGestures.OnPinchMove += PinchMoveHandler;
        
    }

    /* void DragCamera(Vector2 moveDelta)
    {
      transform.Translate(new Vector3(-moveDelta.x,-moveDelta.y,0)); 
    }*/

    private void DragMoveHandler(Vector2 fingerPos, Vector2 moveDelta)
    {
        camera.transform.Translate(new Vector3(-moveDelta.x*0.0025f, -moveDelta.y*0.0025f, 0));
    }

    private void PinchMoveHandler(Vector2 fingerPos1, Vector2 fingerPos2, float delta)
    {
        print("PINCHMOVE"+delta.ToString());
        Field.localScale += new Vector3(0.001f*delta,0.001f*delta, 0);
        Field.localScale = new Vector3(Mathf.Clamp(Field.localScale.x, 0.5f, 1f), Mathf.Clamp(Field.localScale.y, 0.5f, 1f), 0);
    }

    private void OnDisable()
    {
        FingerGestures.OnDragMove -= DragMoveHandler;
        FingerGestures.OnPinchMove -= PinchMoveHandler;
    }

    
    
}