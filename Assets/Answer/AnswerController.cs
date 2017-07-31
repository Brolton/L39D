using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Brolton.GUI;
using Brolton.Utils;

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

        [SerializeField]
        TimerController _timer;

        int _questionNumber = 1;

        public void Init(ChatController chatController)
        {
            _chatController = chatController;
            _tableView.CellPrefab = _answerCellPrefab;
            _tableView.Init(this, this);
            _tableView.GetComponent<TableViewScroll>().enabled = false;

            _inputField.enabled = false;
            _inputField.onValueChanged.AddListener(OnInputChanged);
            _inputField.onEndEdit.AddListener(OnEndEdit);
        }

        #region ITableViewDataSource

        public int NumberOfRowsInTableView(TableView tableView)
        {
            return _availableAnswers.Count;
        }

        public float SizeForRowInTableView(TableView tableView, int row)
        {
            return 86;
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
            _inputField.enabled = true;
            _inputField.text = "";
            _inputField.ActivateInputField();
            _timer.gameObject.SetActive(true);

            int numberOfSendedQuestions = AppController.Instance.MessagesController.GetNumberOfSendedQuestions();
//            float timeForOneSymbol = Settings.StartTimeForOneSymbol - (_questionNumber - 1) * Settings.DeltaTimeForOneSymbol;
            float timeForOneSymbol = Settings.StartTimeForOneSymbol * Mathf.Pow(Settings.DeltaTimeForOneSymbol, numberOfSendedQuestions - 1);
            if (timeForOneSymbol < Settings.MinTimeForOneSymbol)
            {
                timeForOneSymbol = Settings.MinTimeForOneSymbol;
            }
            _timer.StartTimer(_choosedAnswer.text.Length * timeForOneSymbol);
        }

        #endregion

        public void SetAnswers(List<Answer> answers, int questionNumber)
        {
            _questionNumber = questionNumber;

            _tableView.transform.parent.gameObject.SetActive(true);
            _choosedAnswer.transform.parent.gameObject.SetActive(false);
            _timer.gameObject.SetActive(false);

            _availableAnswers = answers;
            _tableView.ReloadData();
            _tableView.ScrollToCell (answers.Count / 2);
        }

        void OnInputChanged(string str)
        {
            // Ok or Error?
            if (_availableAnswers[_choosedAnswerId].text.Contains(str))
            {
                _inputField.GetComponent<Image>().color = Color.white;
            }
            else
            {
                _inputField.GetComponent<Image>().color = Color.red;
            }
        }

        public void OnEndEdit(string str)
        {
            if (_availableAnswers[_choosedAnswerId].text != str)
            {
                _inputField.ActivateInputField();
                return;
            }

            _timer.StopTimer();
            _timer.gameObject.SetActive(false);
            _availableAnswers[_choosedAnswerId].ParentQuestion.SetAnswerId(_choosedAnswerId);
            _availableAnswers[_choosedAnswerId].ParentQuestion.SetAnswerText(_inputField.text);
            _inputField.text = "";
            _inputField.enabled = false;
            _chatController.ReloadCurrentDialog();
            int currentContactId = AppController.Instance.GetCurrentContactID();
            AppController.Instance.SetTimerForContact(currentContactId);
        }
    }
}

