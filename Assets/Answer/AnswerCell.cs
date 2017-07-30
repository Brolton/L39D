using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nekki.SF2.GUI;
using LudumDare39;

public class AnswerCell : TableViewCell
{
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
    }
}
