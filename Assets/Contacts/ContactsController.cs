using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nekki.SF2.GUI;

public class ContactsController : MonoBehaviour, ITableViewDataSource, ITableViewDelegate
{
    [SerializeField]
    TableView _tableView;
    [SerializeField]
    GameObject _contactCellPrefab;

    private List<string> _contacts = new List<string>();

    public void Init()
    {
        GetContacts();

        _tableView.CellPrefab = _contactCellPrefab;
        _tableView.Init(this, this);

//        _tableView.onSelectCell.AddListener (OnScrollTable);
//        _tableView.onSelectCell.AddListener (OnChooseCurrentCell);

        _tableView.ScrollToCell (_tableView.NumberOfRows () - 1);

//        LoadTablePosition(_tableView);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void GetContacts()
    {
//        List<Question> Questions = MessageController.AllQuestions;
//        for (int i = 0; i < Questions.Count; i++)
//        {
//            _messages.Add(Questions[i]);
//        }
        for (int i = 1; i <= 5; i++)
        {
            _contacts.Add ("Name " + i);
        }
    }

    #region ITableViewDataSource

    public int NumberOfRowsInTableView(TableView tableView)
    {
        return _contacts.Count;
    }

    public float SizeForRowInTableView(TableView tableView, int row)
    {
        return 102;
    }

    public TableViewCell CellForRowInTableView(TableView tableView, int row)
    {
        TableViewCell cell = tableView.ReusableCellForRow(row);
        ContactCell contactCell = cell.GetComponent<ContactCell>();
        //            displayItem.BaseSize = Constants.SEAL_SIZE;
        //            displayItem.IconPanelActive = false;
        //            ItemInfo itemInfo = _sealsItems[row].ItemInfo;
        //            displayItem.SetItemInfo(itemInfo);
        //              displayItem->setActive(true);
        contactCell.Init(_contacts[row]);

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
//        _tableView.ScrollToCell (row, 0.5f);
        //          Log.Write("TableViewDidSelectCellForRow : " + row);
    }

    #endregion
}
