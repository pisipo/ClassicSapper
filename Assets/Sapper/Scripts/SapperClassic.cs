using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SapperClassic : MonoBehaviour
{
    public static event Action<bool> GameOverEvent;
    private static SapperClassic _instance;

    public int DeminedMinesCount
    {
        get;
        set;
    }

    public int MinesLeftCount
    {
        get;
        set;
    }

    public int OpenedCellsCount
    {
        get; set;
    }
    public static SapperClassic Instance
    {
        get
        {
            return _instance;
        }
    }
    public  FieldClassic field;
    public bool IsPaused
    {
        get;
        set;
    }		
	void Start()
	{
	    DeminedMinesCount = 0;
	    OpenedCellsCount = 0;
        
	    _instance = this;
	    GestureController.OnCellShortTap += CellShortTapHandler;
        GestureController.OnCellLongTap += CellLongTapHandler;
        
	}

    
   /* void OnTap(TapGestureRecognizer gesture)
    {
        Debug.Log("Tap");
    }*/

    void CellShortTapHandler(CellClassic cell)
    {
        if (cell.IsFlag)
            return;
        else if (cell.IsClose)
        {
            var result = field.OpenCell(cell);
            if(result<0)
                GameOver(false);
            else if(field.CheckForWin())
                    GameOver(true);
        }
    }

    void CellLongTapHandler(CellClassic cell)
    {
#if UNITY_ANDROID || UNITY_IPHONE
        Handheld.Vibrate();
#endif
        if (cell.IsClose)
            cell.IsFlag = !cell.IsFlag;
        else
        {
            if (field.DemineNearMines(cell) < 0)
            {
                GameOver(false);
            }
            else if(field.CheckForWin())
            {
                GameOver(true); 
            }

        }
    }

    public void GameOver(bool IsWin)
    {
        //Application.LoadLevel(Application.loadedLevel);
        field.OpenAllCells();
        GameOverEvent(IsWin);
    }

    public void StarGame(int rowCount,int colCount,int minesPersentage)
    {

        field = new FieldClassic(rowCount, colCount);
        field.SetMines(minesPersentage);
        field.SetNearMinesCount();  
    }

    void OnDisable()
    {
        print("ONSAPPERDISABLE");
        _instance = null;
        GestureController.OnCellShortTap -= CellShortTapHandler;
        GestureController.OnCellLongTap -= CellLongTapHandler;
    }
}
