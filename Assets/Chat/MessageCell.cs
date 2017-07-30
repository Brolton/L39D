using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nekki.SF2.GUI;

namespace LudumDare39
{
    public class MessageCell : TableViewCell
    {
        const int POS_X = 38;

        [SerializeField]
        UnityEngine.UI.Text _text;
        [SerializeField]
        Image _cloud;

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

        public void UpdateMsg(MessageData msgData)
        {
            _text.text = msgData.Text;

            if (msgData.IsMine)
            {
                _cloud.color = Color.white;
                _cloud.transform.SetLocalPositionX(POS_X);
            }
            else
            {
                _cloud.color = Color.green;
                _cloud.transform.SetLocalPositionX(-POS_X);
            }
        }
    }
}

