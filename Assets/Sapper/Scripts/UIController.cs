using UnityEngine;
using System.Collections;

public class UIController : MonoBehaviour
{
    [SerializeField]private GameObject _difficultChooseMenu;
    [SerializeField] private GameObject _easyButton;
    [SerializeField]
    private GameObject _mediumButton;
    [SerializeField]
    private GameObject _hardButton;
    [SerializeField]
    private GameObject _resetButton;

    [SerializeField] private UILabel _minesLeft;
    [SerializeField] private UILabel _deminedMines;
    [SerializeField]
    private UILabel _openedCells;

    [SerializeField]
    private UILabel _gameOverText;

    void Update()
    {
        if (SapperClassic.Instance.field != null)
        {
            _deminedMines.text = SapperClassic.Instance.field.DeminedCells.ToString();
        
            _openedCells.text = SapperClassic.Instance.field.OpenedCells.ToString();
          
             _minesLeft.text = (SapperClassic.Instance.field.MinesCount - SapperClassic.Instance.field.DeminedCells).ToString();
          
        }

    }
    void Start()
    {
        print("UICONTROLLERSTART");
        _difficultChooseMenu.SetActive(true);
        GestureController.OnGUITap += GUITapHandler;
        SapperClassic.GameOverEvent += OnGameOver;

    }
    public void Easy()
    {
        SapperClassic.Instance.StarGame(10,10,8);
        _difficultChooseMenu.SetActive(false);
    }
    public void Medium()
    {
        SapperClassic.Instance.StarGame(15, 15, 15);
        _difficultChooseMenu.SetActive(false);
    }
    public void Hard()
    {
        SapperClassic.Instance.StarGame(20, 20, 20);
        _difficultChooseMenu.SetActive(false);
    }

    public void Reset()
    {
        //_difficulChooseMenu.SetActive(true);
        Application.LoadLevel(Application.loadedLevel);
    }

    void GUITapHandler(GameObject sender)
    {
        if (sender==_easyButton)Easy();
        if (sender == _mediumButton) Medium();
        if (sender == _hardButton) Hard();
        if (sender == _resetButton) Reset();
    }

    void OnDisable()
    {
        GestureController.OnGUITap -= GUITapHandler;
        SapperClassic.GameOverEvent -= OnGameOver;
    }

    private void OnGameOver(bool isWin)
    {
        _gameOverText.gameObject.SetActive(true);
        if (isWin)
        {
            _gameOverText.text = "Holy cow, I'm good!";
         
        }

        else
        {
            _gameOverText.text = "Damn, those mines again  burn up my ride!";

        }
    }
}
