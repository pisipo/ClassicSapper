using UnityEngine;
using System.Collections;

public class CellViewClassic : MonoBehaviour
{
    public UISprite flag;
    public UISprite question;
    public UISprite close;
    public UISprite open;
    public UISprite mine;
    public UILabel NearMinesCountText;
    private CellClassic _model;

    public CellClassic Model
    {
        get
        {
            return _model;
        }
        set
        {
            _model = value;
            _model.UpdateViewEvent += OnUpdateView;
        }
    }

    private void OnUpdateView()
    {
        //DEBUG
        //if (Model.IsMine) Model.IsFlag = true;




        collider.enabled = Model.IsEnable;
        flag.gameObject.SetActive(Model.IsFlag);
        if (Model.IsClose == false) {
            close.gameObject.SetActive(false);
            open.gameObject.SetActive(true);
            NearMinesCountText.text = Model.NearMinesCount.ToString();
            NearMinesCountText.gameObject.SetActive(true);
        }
        if (Model.IsDemine)
        {
            close.gameObject.SetActive(false);
            flag.gameObject.SetActive(false);
            open.gameObject.SetActive(true);
            NearMinesCountText.gameObject.SetActive(false);
            mine.gameObject.SetActive(true);
        }


    }

}
  
