using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nekki.SF2.GUI;

namespace LudumDare39
{
    public class AnswerController : MonoBehaviour, ITableViewDataSource, ITableViewDelegate
    {
        [SerializeField]
        TableView _tableView;
        [SerializeField]
        GameObject _answerCellPrefab;

        [SerializeField]
        InputField _inputField;

        [SerializeField]
        UnityEngine.UI.Text _choosedAnswer;
        int _choosedAnswerId = -1;

        private List<Answer> _availableAnswers = new List<Answer>();

        ChatController _chatController;

        public void Init(ChatController chatController)
        {
            _chatController = chatController;
            _tableView.CellPrefab = _answerCellPrefab;
            _tableView.Init(this, this);
            _tableView.GetComponent<TableViewScroll>().enabled = false;

            _inputField.onEndEdit.AddListener(OnEndEdit);
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
            _choosedAnswerId = row;
            _tableView.transform.parent.gameObject.SetActive(false);
            _choosedAnswer.transform.parent.gameObject.SetActive(true);
            _choosedAnswer.text = _availableAnswers[row].text;
        }

        #endregion

        public void SetAnswers(List<Answer> answers)
        {
            _tableView.transform.parent.gameObject.SetActive(true);
            _choosedAnswer.transform.parent.gameObject.SetActive(false);

            _availableAnswers = answers;
            _tableView.ReloadData();
            _tableView.ScrollToCell ((answers.Count ) / 2);
        }

        public void OnEndEdit(string str)
        {
            _availableAnswers[_choosedAnswerId].ParentQuestion.SetAnswerId(_choosedAnswerId);
            _availableAnswers[_choosedAnswerId].ParentQuestion.SetAnswerText(_inputField.text);
            _inputField.text = "";
            _chatController.ReloadCurrentDialog();
            int currentContactId = AppController.Instance.GetCurrentContactID();
            AppController.Instance.SetTimerForContact(currentContactId);
        }
    }
}

