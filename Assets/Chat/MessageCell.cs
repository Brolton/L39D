using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nekki.SF2.GUI;

namespace LudumDare39
{
    public class MessageCell : TableViewCell
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

        #region ProfileSliderItem

//        public abstract SubItem GetFirstIcon ();
//        public abstract void UpdateState ();
//        public abstract void Clear ();

        #endregion

        public void UpdateMsg(string text)
        {
            _text.text = text;
        }
    }
}

