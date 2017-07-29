using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Nekki.SF2.GUI
{
    public interface ITableView
    {
        ITableViewDataSource DataSource { get; set; }
        ITableViewDelegate Delegate { get; set; }

		GameObject CellPrefab { get; set; }

        Range VisibleRange { get; }
        float ContentSize { get; }
        float Position { get; }

        TableViewCell ReusableCellForRow(int row);
        TableViewCell CellForRow(int row);
        float PositionForRow(int row);
        void ReloadData();

        void SetPosition(float newPosition, float time);
    }
}