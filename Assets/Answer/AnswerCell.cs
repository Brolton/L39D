using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nekki.SF2.GUI;
using LudumDare39;

public class AnswerCell : TableViewCell
{
    [SerializeField]
    Image _background;
    [SerializeField]
    UnityEngine.UI.Text _text;

    #region TableViewCell

    public override void SetHighlighted()
    {
        //          print("CellSetHighlighted : " + RowNumber);
    }

    public override void SetSelected()
    {
        //          print("CellSetSelected : " + RowNumber);
    }

    public override void Display()
    {
        //          text.text = "Row " + RowNumber;
    }

    #endregion

    public void UpdateAnswer(Answer answer)
    {
        _text.text = answer.text;
        
//        switch (answer.color)
//        {
//            case AnswerColor.Red:
//                _background.color = new Color(120 / 255.0f, 22 / 255.0f, 22 / 255.0f);
//                break;
//            case AnswerColor.Yellow:
//                _background.color = new Color(22 / 255.0f, 38 / 255.0f, 120 / 255.0f);
//                break;
//            case AnswerColor.Green:
//                _background.color = new Color(51 / 255.0f, 128 / 255.0f, 10 / 255.0f);
//                break;
//            default:
//                _background.color = Color.white;
//                break;
//        }
    }
}
