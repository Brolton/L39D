using System;
using System.Collections.Generic;
using UnityEngine;
using Nekki.SF2.GUI;
//using Nekki.SF2.Core.API;
//using Nekki.SF2.Core.API.Items;
//using Nekki.SF2.Core.Utils;
//using Nekki.SF2.GUI.Shop;
//using Nekki.Utils;


namespace LudumDare39
{
    public class TableViewController: ITableViewDataSource, ITableViewDelegate
    {
        TableView _tableView;
        private List<Question> _messages = new List<Question>();

        public TableViewController(TableView tableView, GameObject cellPrefab)
        {
            _tableView = tableView;
            GetMessages();

            tableView.CellPrefab = cellPrefab;
            tableView.Init(this, this);
        }

        #region ITableViewDataSource

        public int NumberOfRowsInTableView(TableView tableView)
        {
            return _messages.Count;
        }

        public float SizeForRowInTableView(TableView tableView, int row)
        {
            return 102;
        }

        public TableViewCell CellForRowInTableView(TableView tableView, int row)
        {
            TableViewCell cell = tableView.ReusableCellForRow(row);
            MessageCell msgCell = cell.GetComponent<MessageCell>();
//            displayItem.BaseSize = Constants.SEAL_SIZE;
//            displayItem.IconPanelActive = false;
//            ItemInfo itemInfo = _sealsItems[row].ItemInfo;
//            displayItem.SetItemInfo(itemInfo);
            //              displayItem->setActive(true);
            msgCell.UpdateMsg(_messages[row].text);

            return cell;
        }

        #endregion

        #region ITableViewDelegate

        public void TableViewDidHighlightCellForRow(TableView tableView, int row)
        {
            //          Log.Write("TableViewDidHighlightCellForRow : " + row);
        }

        public void TableViewDidSelectCellForRow(TableView tableView, int row)
        {
            _tableView.ScrollToCell (row, 0.5f);
            //          Log.Write("TableViewDidSelectCellForRow : " + row);
        }

        #endregion

        void GetMessages()
        {
//            Roster roster = ListSF.Roster;
//            List<UserItem> items = roster.UserItems.GetUserItemsByType(ItemInfo.TYPE_ITEM_SEALS);
//            foreach (UserItem item in items) {
//                if (item.Count != 0) {
//                    _sealsItems.Add (item);
//                }
//            }
            List<Question> Questions = MessageController.AllQuestions;
            for (int i = 0; i < Questions.Count; i++)
            {
                _messages.Add(Questions[i]);
            }
        }

//        public void ScrollToSeal(string sealName)
//        {
//            int cellIndex = -1;
//            for (int i = 0, iMax = _sealsItems.Count; i < iMax; i++) {
//                string cellName = _sealsItems[i].Name;
//                if (cellName == sealName) {
//                    cellIndex = i;
//                    break;
//                }
//            }
//            if (cellIndex > -1) {
//                _tableView.ScrollToCell(cellIndex);
//            }
//        }
    }
}