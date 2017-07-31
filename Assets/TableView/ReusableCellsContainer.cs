using System;
using System.Collections.Generic;
using UnityEngine;

namespace Brolton.GUI
{
	public class ReusableCellsContainer
    {
        #region Property

		public LinkedList<TableViewCell> ReusableCells;

        #endregion

        #region Public

		public void Init()
		{
			ReusableCells = new LinkedList<TableViewCell>();
		}

        public void AddReusableCell(TableViewCell cell)
        {
            ReusableCells.AddLast(cell);
            cell.gameObject.SetActive(false);
        }

        public TableViewCell GetReusableCell()
        {
            if (ReusableCells.Count == 0) return null;

			TableViewCell reusableCell = ReusableCells.First.Value;
            reusableCell.gameObject.SetActive(true);
			ReusableCells.RemoveFirst();

            return reusableCell;
        }

        #endregion
    }
}