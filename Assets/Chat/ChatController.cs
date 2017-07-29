using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nekki.SF2.GUI;

namespace LudumDare39
{
    public class ChatController : MonoBehaviour
    {
        [SerializeField]
        TableView _tableView;
        [SerializeField]
        GameObject _msgCellPrefab;
        [SerializeField]
        TableViewController _msgsCtrl;

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
            _msgsCtrl = new TableViewController (_tableView, _msgCellPrefab);
            _tableView.onSelectCell.AddListener (OnScrollTable);
            _tableView.onSelectCell.AddListener (OnChooseCurrentCell);

            _tableView.ScrollToCell (_tableView.NumberOfRows () - 1);
        }

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
    }
}