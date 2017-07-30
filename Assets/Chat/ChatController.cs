using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nekki.SF2.GUI;

namespace LudumDare39
{
    public class ChatController : MonoBehaviour, ITableViewDataSource, ITableViewDelegate
    {
        [SerializeField]
        TableView _tableView;
        [SerializeField]
        GameObject _msgCellPrefab;

        private List<Question> _messages = new List<Question>();

        public void Init()
        {
            AddSliders();
            LoadTablePosition(_tableView);
        }

        private void OnDestroy()
        {
            _tableView.onSelectCell.RemoveAllListeners();
        }

    	// Use this for initialization
    	void Start ()
        {
    	}
    	
    	// Update is called once per frame
    	void Update () 
        {
    		
    	}

        private void AddSliders()
        {
            _tableView.CellPrefab = _msgCellPrefab;
            _tableView.Init(this, this);

            _tableView.onSelectCell.AddListener (OnScrollTable);
            _tableView.onSelectCell.AddListener (OnChooseCurrentCell);

            _tableView.ScrollToCell (_tableView.NumberOfRows () - 1);
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
            //            _tableView.ScrollToCell (row, 0.5f);
            //          Log.Write("TableViewDidSelectCellForRow : " + row);
        }

        #endregion

//        void GetMessages()
//        {
//            //            Roster roster = ListSF.Roster;
//            //            List<UserItem> items = roster.UserItems.GetUserItemsByType(ItemInfo.TYPE_ITEM_SEALS);
//            //            foreach (UserItem item in items) {
//            //                if (item.Count != 0) {
//            //                    _sealsItems.Add (item);
//            //                }
//            //            }
//            List<Question> Questions = MessageController.AllQuestions;
//            for (int i = 0; i < Questions.Count; i++)
//            {
//                _messages.Add(Questions[i]);
//            }
//        }

        private void LoadTablePosition(TableView table)
        {
            if (table == null)
                return;

    //        string tableName = GetCurrentTableName(table);
    //        if (tableName != "") {
    //            int index = GameUtils.SlidersIndex.GetSliderIndex(tableName);
    //            if (index >= 0 && index < table.NumberOfRows()) {
    //                table.ScrollToCell(index);
    //            }
    //        }
        }

        private void OnScrollTable(object data)
        {
    //        if (_currentTable == null) {
    //            return;
    //        }
    //
    //        SaveSliderPosition();
    //
    //        if (_currentTable == _perksTable) {
    //            SliderItemsTouchEnable();
    //        }
        }

        private void OnChooseCurrentCell(object data)
        {
    //        if (_currentTable == null || (_currentTable != _tricksTable && _currentTable != _achievementsTable && _currentTable != _sealsTable)) {
    //            return;
    //        }
    //
    //        if (_currentTable != _sealsTable) {
    //            ProfileCell cell = (ProfileCell)_currentTable.SelectedCell;
    //
    //            if (cell != null) {
    //                SubItem subItem = cell.GetFirstIcon ();
    //                subItem.Choose ();
    //            }
    //        } else {
    //            ShopTableViewCell item = (ShopTableViewCell)_currentTable.SelectedCell;
    //            if (item != null) {
    //                ItemInfo itemInfo = item.ItemInfo;
    //                _rightPanel.SetItemInfo(itemInfo);
    //            }
    //        }
        }

        public void SetCurrentContact(int contactId)
        {
            _messages = AppController.Instance.MessagesController.GetMessagesByContact(contactId);
            _tableView.ReloadData();
        }
    }
}