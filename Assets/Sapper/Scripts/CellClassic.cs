using System;
using UnityEngine;
using System.Collections;

public class CellClassic
{
    public event Action UpdateViewEvent;
    public int Row { get; set; }

    public int Column { get; set; }

    public bool IsFlag
    {
        get { return _isFlag; }
        set
        {
            _isFlag = value;
            UpdateView();
        }
    }

    private bool _isEnable;
    public bool IsMine
    {
        get { return _isMine; }
        set
        {
            _isMine = value;
            UpdateView();
        }
    }

    public bool IsDemine
    {
        get { return _isDemine; }
        set
        {
            _isDemine = value;
            UpdateView();
        }
    }
    public bool IsClose
    {
        get { return _isClose; }
        set
        {
            _isClose = value;
            UpdateView();
        }
    }

    public int NearMinesCount
    {
        get { return _nearMinesCount; }
        set { _nearMinesCount = value; }
    }

    public bool IsEnable
    {
        get { return _isEnable; }
        set
        {
            _isEnable = value; 
            UpdateView();
        }
    }

    private bool _isMine;
    private bool _isFlag;
    private bool _isClose;
    private int _nearMinesCount;
    private bool _isDemine;
    public CellClassic(int row, int col)
    {
        Row = row;
        Column = col;
        IsClose = true;
        IsFlag = false;
        IsEnable = true;
    }

    private void UpdateView()
    {
        if (UpdateViewEvent != null)
        {
            UpdateViewEvent();
        }
    }

//    private CellViewClassic view;

}