using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Brolton.GUI
{
    public class VisibleCells
    {
        #region Property

        public Range IndexesRange;
		public Dictionary<int, TableViewCell> cells { get; private set; }

        public int Count
        {
            get { return cells.Count; }
        }

        #endregion

        #region Public

        public VisibleCells()
        {
            IndexesRange = new Range(0, 0);
            cells = new Dictionary<int, TableViewCell>();
        }

        public TableViewCell GetCellAtIndex(int index)
        {
            TableViewCell cell = null;
            cells.TryGetValue(index, out cell);
            return cell;
        }

        public void SetCellAtIndex(int index, TableViewCell cell)
        {
            cells[index] = cell;
        }

        public void RemoveCellAtIndex(int index)
        {
            cells.Remove(index);
        }

        #endregion
    }
}