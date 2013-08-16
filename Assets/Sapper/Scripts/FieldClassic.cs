using System.Collections;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class FieldClassic
{   
    private CellClassic[,] _field;
    //private FieldClassic _instance;
    private int _minesCount;
    private int _openedCells;
    private int _deminedCells; 
    













    public FieldClassic(int rowCount,int colCount)
    {
        MinesCount = 0;
        OpenedCells = 0;
        DeminedCells = 0;
        if(_field==null)
            _field=new CellClassic[rowCount,colCount];
        else
        {
            return;
        }
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                _field[i,j]=new CellClassic(i,j);
                CreateCellGameObject(_field[i,j]);
            }
        }
    }

    public int MinesCount
    {
        get { return _minesCount; }
        set { _minesCount = value; }
    }

    public int OpenedCells
    {
        get { return _openedCells; }
        set { _openedCells = value; }
    }

    public int DeminedCells
    {
        get { return _deminedCells; }
        set { _deminedCells = value; }
    }

    public void SetMines(int chanseOfMine)
    {
        for (int i = 0; i < _field.GetLength(0); i++)
        {
            for (int j = 0; j < _field.GetLength(1); j++)
            {
                if (UnityEngine.Random.Range(0, 100) <= chanseOfMine)
                {
                    _field[i, j].IsMine = true;
                    MinesCount++;
                }
                else
                    _field[i, j].IsMine = false;
            }
        }
    }
    public void SetNearMinesCount()
    {
        for (int i = 0; i < _field.GetLength(0); i++)
        {
            for (int j = 0; j < _field.GetLength(1); j++)
            {
                try { if (_field[i + 1, j].IsMine == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i - 1, j].IsMine == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i, j + 1].IsMine == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i, j - 1].IsMine == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i + 1, j + 1].IsMine == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i - 1, j + 1].IsMine == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i - 1, j - 1].IsMine == true)_field[i, j].NearMinesCount++; }
                catch { }
                try { if (_field[i + 1, j - 1].IsMine == true)_field[i, j].NearMinesCount++; }
                catch { }
            }
        }
    }
    public int SearchForFreeNearCells(CellClassic cell)
    {
        //int delay=1;
        for (int i = cell.Row - 1; i < cell.Row + 2; i++)
        {
            for (int j = cell.Column - 1; j < cell.Column + 2; j++)
            {
                try
                {
                    if (_field[i, j].IsClose == true)
                    {
                        _field[i, j].IsClose = false;
                        _field[i, j].IsFlag = false;
                        OpenedCells++;
                        if (_field[i, j].NearMinesCount == 0)
                        {
                            SearchForFreeNearCells(_field[i, j]);  
                        }
                    }
                   
                }
                catch { }
            }
        }
        return OpenedCells;
    }

    public int DemineNearMines(CellClassic cell)
    {
        int nearMinesCount = 0;
        int nearFlagsCount = 0;
        if (cell.NearMinesCount == 0)
        {
           
            Debug.Log("There are no mines around");
            return 0;
        }
        for (int i = cell.Row - 1; i < cell.Row + 2; i++)
        {
            for (int j = cell.Column - 1; j < cell.Column + 2; j++)
            {
                try
                {
                    if (_field[i, j].IsMine&&!_field[i,j].IsDemine) nearMinesCount++;
                    if (_field[i, j].IsFlag/*||_field[i,j].IsDemine*/) nearFlagsCount++;
                    if (nearFlagsCount != nearMinesCount)
                    {
                        Debug.Log("Flags not much mines count" + nearFlagsCount.ToString() + " " + nearMinesCount.ToString());
                        return 0;
                    }
                }
                catch { }
            }
        }
        for (int i = cell.Row - 1; i < cell.Row + 2; i++)
        {
            for (int j = cell.Column - 1; j < cell.Column + 2; j++)
            {
                try
                {
                    if(!_field[i,j].IsClose || _field[i,j].IsDemine)
                        continue;
                    if (!_field[i, j].IsMine)
                    {
                        _field[i, j].IsClose = false;
                        OpenedCells++;
                    }
                    else
                    {
                        if (!_field[i, j].IsFlag)
                        {
                            return -1;
                        }
                        else
                        {
                            _field[i, j].IsDemine = true;
                            _field[i, j].IsFlag = false;

                        }
                    }
                }
                catch { }
            }
        }
        DeminedCells += nearMinesCount;
        return nearMinesCount;
    }
    public int OpenCell(CellClassic cell)
    {
        cell.IsClose = false;
        OpenedCells++;
        if (cell.IsMine)
        {
            return -1;
        }
        else if (cell.NearMinesCount == 0)
        {
            cell.IsClose = false;
            return 1+SearchForFreeNearCells(cell);
        }
        else
        {
            cell.IsClose = false;
            return 1;
        }
    }

    public bool CheckForWin()
    {
       // if (_field.GetLength(0)*_field.GetLength(1) - OpenedCells == MinesCount)
        if(_deminedCells==MinesCount)
        {
            return true;
        }
        return false;
    }
    public void OpenAllCells()
    {
        for (int i = 0; i < _field.GetLength(0); i++)
        {
            for (int j = 0; j < _field.GetLength(1); j++)
            {
                _field[i, j].IsClose = false;
                _field[i, j].IsEnable = false;
                if (_field[i, j].IsMine)
                _field[i, j].IsDemine = true;

            }

        }
    }

    void CreateCellGameObject(CellClassic cell)
    {
       //var cellGO= GameObject.Instantiate(Resources.Load("prefabs/game/classic/cell")) as GameObject;
        var cellGO=NGUITools.AddChild( GameObject.Find("Field").gameObject,Resources.Load("prefabs/game/classic/cell"))
       cellGO.GetComponent<CellViewClassic>().Model = cell;
        cellGO.name = cell.Row.ToString() + "_" + cell.Column.ToString() + "_" + "cell";
       cellGO.transform.parent = GameObject.Find("Field").transform;
        var cellSprite = cellGO.GetComponent<CellViewClassic>().close;
       //cellGO.transform.localPosition = new Vector3(cell.Row * cellSprite.renderer.bounds.size.x, cell.Column * cellSprite.renderer.bounds.size.y, 0);
       // cellSprite.border
    }
}
