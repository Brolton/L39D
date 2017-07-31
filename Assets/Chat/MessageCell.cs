using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Brolton.GUI;

namespace LudumDare39
{
    public class MessageCell : TableViewCell
    {
        [SerializeField]
        GameObject _aiCloud;
        [SerializeField]
        UnityEngine.UI.Text _aiText;

        [SerializeField]
        GameObject _playerCloud;
        [SerializeField]
        UnityEngine.UI.Text _playerText;

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
            _aiCloud.gameObject.SetActive(!msgData.IsMine);
            _playerCloud.gameObject.SetActive(msgData.IsMine);

            if (!msgData.IsMine)
            {
                _aiText.text = msgData.Text;
                _playerText.text = "";
            }
            else
            {
                _aiText.text = "";
                _playerText.text = msgData.Text;
            }
        }
    }
}

