﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nekki.SF2.GUI;

namespace LudumDare39
{
    public class AnswerController : MonoBehaviour, ITableViewDataSource, ITableViewDelegate
    {
        [SerializeField]
        TableView _tableView;
        [SerializeField]
        GameObject _answerCellPrefab;

        private List<Answer> _availableAnswers = new List<Answer>();

        public void Init()
        {
            _tableView.CellPrefab = _answerCellPrefab;
            _tableView.Init(this, this);
        }

        #region ITableViewDataSource

        public int NumberOfRowsInTableView(TableView tableView)
        {
            return _availableAnswers.Count;
        }

        public float SizeForRowInTableView(TableView tableView, int row)
        {
            return 102;
        }

        public TableViewCell CellForRowInTableView(TableView tableView, int row)
        {
            TableViewCell cell = tableView.ReusableCellForRow(row);
            AnswerCell answerCell = cell.GetComponent<AnswerCell>();
            //            displayItem.BaseSize = Constants.SEAL_SIZE;
            //            displayItem.IconPanelActive = false;
            //            ItemInfo itemInfo = _sealsItems[row].ItemInfo;
            //            displayItem.SetItemInfo(itemInfo);
            //              displayItem->setActive(true);
            answerCell.UpdateAnswer(_availableAnswers[row]);

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

        public void SetAnswers(List<Answer> answers)
        {
            _availableAnswers = answers;
            _tableView.ReloadData();
        }
    }
}
