using System;
using UnityEngine;
using System.Collections;

public class GestureController : MonoBehaviour
{
    [SerializeField]private Camera GUICamera;
    public static event Action<CellClassic> OnCellShortTap;
    public static event Action<CellClassic> OnCellLongTap;
    public static event Action<CellClassic> OnCellDoubleTap;
    public static event Action<Vector2> OnDrag;
    public static event Action<GameObject> OnGUITap; 

    private void Start()
    {
        GetComponent<TapGestureRecognizer>().OnTap += OnTap;
        GetComponent<LongPressGestureRecognizer>().OnLongPress += OnLongTap;
    }

    private void OnTap(TapGestureRecognizer gesture)
    {
        //print("TAP");
        var GO = PickObject(gesture.Position);
        if (GO)
        {
           // Debug.Log("ShortTap" + GO.name);
            if (GO.tag == "classic_cell")
                if (OnCellShortTap != null)
                    OnCellShortTap(GO.GetComponent<CellViewClassic>().Model);
            if (GO.tag == "GUI")
                if (OnGUITap != null)
                    OnGUITap(GO);
        }
    }

    private void OnLongTap(LongPressGestureRecognizer gesture)
    {
       // print("LONGTAP");
        var GO = PickObject(gesture.Position);
        if (GO)
        {
            //Debug.Log("LongTap" + GO.name);
            if (GO.tag == "classic_cell")
                if (OnCellLongTap != null)
                    OnCellLongTap(GO.GetComponent<CellViewClassic>().Model);
            if(GO.tag=="GUI")
                if (OnGUITap != null)
                    OnGUITap(GO);
        }
    }

    private void OnDraging(DragGestureRecognizer gesture)
    {
        OnDrag(gesture.MoveDelta);
    }

    #region Utils

    // Convert from screen-space coordinates to world-space coordinates on the Z = 0 plane
    public static Vector3 GetWorldPos(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        // we solve for intersection with z = 0 plane
        float t = -ray.origin.z/ray.direction.z;

        return ray.GetPoint(t);
    }

    // Return the GameObject at the given screen position, or null if no valid object was found
    public GameObject PickObject(Vector2 screenPos)
    {
        Ray ray = GUICamera.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
            if(hit.collider.gameObject.tag=="GUI")
                return hit.collider.gameObject;
        ray = Camera.main.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out hit))
            return hit.collider.gameObject;

        return null;
    }

    #endregion
#region OnDisable

    void OnDisable()
    {
        GetComponent<TapGestureRecognizer>().OnTap -= OnTap;
        GetComponent<LongPressGestureRecognizer>().OnLongPress -= OnLongTap;
    }
#endregion
}