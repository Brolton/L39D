using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using DG.Tweening;
//using Brolton.Utils;

namespace Brolton.GUI
{
    public enum TableViewOrientation
    {
        Vertical,
        Horizontal
    }

    public class TableView : MonoBehaviour, ITableView
    {
        #region Property

        [SerializeField]
        private TableViewOrientation tableViewOrientation = TableViewOrientation.Vertical;

        [SerializeField]
        private float _spacing = 0.0f;
        public float Spacing {
            get { return _spacing; }
            set {
                _spacing = value;
                if (_cellSizes != null) {
                    _cellSizes.Spacing = _spacing;
                }
            }
        }

        private float _startPadding = 0;
        private float _endPadding = 0;

        [SerializeField] private RectOffset padding;

        [SerializeField] private bool inertia = true;
        [SerializeField] private float elasticity = 0.1f;
        [SerializeField] private float scrollSensitivity = 1.0f;
        [SerializeField] private float decelerationRate = 0.135f;

        [SerializeField] private bool scrollToHighlighted = true;

        private ITableViewDataSource _dataSource;
        public ITableViewDataSource DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; requiresReload = true; }
        }

        private ITableViewDelegate _tableViewDelegate;
        public ITableViewDelegate Delegate
        {
            get { return _tableViewDelegate; }
            set { _tableViewDelegate = value; }
        }

        private GameObject _cellPrefab;
        public GameObject CellPrefab
        {
            get { return _cellPrefab; }
            set { _cellPrefab = value; }
        }

        public Range VisibleRange
        {
            get { return visibleCells.IndexesRange; }
        }

        public float ContentSize
        {
            get { return tableViewScroll.Size - tableViewSize; }
        }

        private float _currentPosition;
        public float Position
        {
            get { return _currentPosition; }
        }
        private float _lastRefreshPosition = 0;

        private CellSizes _cellSizes;
        public VisibleCells visibleCells { get; private set; }
        private ReusableCellsContainer reusableCells;

        private TableViewScroll tableViewScroll;
        public TableViewScroll Scroll { get { return tableViewScroll; } }

        private bool isEmpty;
        private bool requiresReload;
        private bool requiresRefresh;

        private bool isVertical
        {
            get { return (tableViewOrientation == TableViewOrientation.Vertical); }
        }

        private float tableViewSize
        {
            get
            {
                Rect rect = (this.transform as RectTransform).rect;
                return isVertical ? rect.height : rect.width;
            }
        }

        private Tween _tween = null;

        bool _isDragging = false;

        #endregion


        #region Lifecycle

        public void Init(ITableViewDataSource dataSource, ITableViewDelegate tableViewDelegate)
        {
            _dataSource = dataSource;
            _tableViewDelegate = tableViewDelegate;

            isEmpty = true;

            _cellSizes = new CellSizes();
            _cellSizes.Spacing = _spacing;

            visibleCells = new VisibleCells();

            reusableCells = new ReusableCellsContainer();
            reusableCells.Init ();

            tableViewScroll = this.gameObject.AddComponent<TableViewScroll>();
            tableViewScroll.Init ();
            tableViewScroll.SetOrientation(tableViewOrientation);
            tableViewScroll.elasticity = elasticity;
            tableViewScroll.movementType = ScrollRect.MovementType.Elastic;// SFScrollRect.MovementType.SF2;
            tableViewScroll.inertia = inertia;
            tableViewScroll.decelerationRate = decelerationRate;
            tableViewScroll.scrollSensitivity = scrollSensitivity;
            tableViewScroll.onValueChanged.AddListener(ScrollViewValueChanged);
            tableViewScroll.onDragBegin.AddListener (OnDragBegin);
            tableViewScroll.onDragEnd.AddListener (OnDragEnd);

            _startPadding = (int)(tableViewSize / 2);
            _endPadding = (int)(tableViewSize / 2);

            this.gameObject.AddComponent<RectMask2D>();
            this.gameObject.AddComponent<CanvasRenderer>();

            ReloadData();
        }

        void Update()
        {
            if (requiresReload)
            {
                ReloadData();
            }
            CheckSelectedCell();
        }

        void LateUpdate()
        {
            if (requiresRefresh)
            {
                RefreshVisibleCells();
            }
        }

        #endregion


        #region Public

        public TableViewCell ReusableCellForRow(int row)
        {
            TableViewCell cell = reusableCells.GetReusableCell();

            if (cell == null)
            {
                cell = CreateCellFromPrefab(row);
            }

            return cell;
        }

        public TableViewCell CellForRow(int row)
        {
            return visibleCells.GetCellAtIndex(row);
        }

        public float PositionForRow(int row)
        {
            if (row < 0 || row > NumberOfRows () - 1) {
                return 0;
            }
            return _cellSizes.GetCumulativeRowSize(row) - _cellSizes.SizeForRow(row) / 2 + _startPadding;
        }

        public void ReloadData()
        {
            MoveAllCellsToReusable();
            SelectedCell = null;
            int numberOfRows = NumberOfRows ();
            _cellSizes.SetRowsCount(numberOfRows);
            isEmpty = (numberOfRows == 0);

            if (isEmpty) return;

            for (int i = 0; i < numberOfRows; i++)
            {
                float rowSize = _dataSource.SizeForRowInTableView(this, i);
                _cellSizes.SetSizeForRow(rowSize, i);
            }

            tableViewScroll.SizeDelta = _startPadding + _cellSizes.GetCumulativeRowSize(numberOfRows - 1) + _endPadding;

            CreateCells();
            requiresReload = false;
        }

        public void ScrollToCell(int row, float time = 0.0f)
        {
            float newPosition = PositionForRow(row);
            SetPosition (newPosition, time);
        }

        public void SetPosition(float newPosition, float time = 0.0f)
        {
            KillTween ();
            if (_isDragging) {
                return;
            }

            if (!gameObject.activeSelf || time <= 0.0f) {
                SetPosition (newPosition);
            } else {
                _tween = DOTween.To(()=> _currentPosition, (x) => {SetPosition(x);}, newPosition, time);
            }
        }

        private void SetPosition(float newPosition)
        {
            if (isEmpty) { return; }

            newPosition = Mathf.Clamp(newPosition, PositionForRow(0), PositionForRow(_cellSizes.RowsCount - 1));

            if (_currentPosition != newPosition)
            {
                requiresRefresh = true;
                _currentPosition = newPosition;
                float realScrollPos = newPosition - tableViewSize / 2; // because we need that center of row will be on center of table
                float normalizedPosition = realScrollPos / ContentSize;
                float relativeScroll = 0;

                if (isVertical)
                {
                    relativeScroll = 1 - normalizedPosition;
                }
                else
                {
                    relativeScroll = normalizedPosition;
                }

                tableViewScroll.SetNormalizedPosition(relativeScroll);
            }
        }

        private void KillTween ()
        {
            if (_tween != null) {
                _tween.Kill ();
                _tween = null;
            }
        }

        #endregion


        #region Private

        private TableViewCell CreateCellFromPrefab(int row)
        {
            if (CellPrefab == null) return null;

            TableViewCell cell = GameObject.Instantiate(CellPrefab, tableViewScroll.content, false).GetComponent<TableViewCell>();
            cell.RowNumber = row;

            return ConfigureCellWithRowAtEnd(cell, row, true);
        }

        private void ScrollViewValueChanged(Vector2 newScrollValue)
        {
            float relativeScroll = 0;

            if (isVertical)
            {
                relativeScroll = 1 - newScrollValue.y;
            }
            else
            {
                relativeScroll = newScrollValue.x;
            }

            _currentPosition = relativeScroll * ContentSize + tableViewSize / 2;
            requiresRefresh = true;
        }

        private void CreateCells()
        {
            MoveAllCellsToReusable();
            SetInitialVisibleCells();
        }

        private void MoveAllCellsToReusable()
        {
            while (visibleCells.Count > 0)
            {
                MoveCellToReusable(false);
            }

            visibleCells.IndexesRange = new Range(0, 0);
        }

        private void DestroyAllCells()
        {
            if (reusableCells != null) {
                foreach (TableViewCell cell in reusableCells.ReusableCells) {
                    Destroy (cell.gameObject);
                }
                reusableCells.ReusableCells.Clear ();
            }

            if (visibleCells != null) {
                foreach (KeyValuePair<int, TableViewCell> cell in visibleCells.cells) {
                    Destroy (cell.Value.gameObject);
                }
                visibleCells.IndexesRange = new Range (0, 0);
                visibleCells.cells.Clear ();
            }
        }

        private Range CurrentVisibleCellsRange()
        {
            float startPosition = Math.Max(_currentPosition - tableViewSize * 1.5f, PositionForRow(0));
            float endPosition = Math.Min(_currentPosition + tableViewSize * 1.5f, PositionForRow(_cellSizes.RowsCount - 1));

            int startIndex = FindIndexOfRowAtPosition(startPosition);
            int endIndex = FindIndexOfRowAtPosition(endPosition);

            int cellsCount = endIndex - startIndex + 1;

            return new Range(startIndex, cellsCount);
        }

        public int FindIndexOfRowAtPosition(float position)
        {
            return FindIndexOfRowAtPosition(position, 0, _cellSizes.RowsCount - 1);
        }

        public int FindIndexOfRowAtPosition(float position, int startIndex, int endIndex)
        {
            if (startIndex >= endIndex) return startIndex;

            if (endIndex - startIndex == 1) // needed pos. between neigbor cells
            {
                float distanceToStartIndex = Mathf.Abs(position - PositionForRow (startIndex));
                float distanceToEndIndex = Mathf.Abs(position - PositionForRow (endIndex));
                if (distanceToStartIndex <= distanceToEndIndex) {
                    return startIndex;
                } else {
                    return endIndex;
                }
            }

            int midIndex = (startIndex + endIndex) / 2;
            float midRowPos = PositionForRow (midIndex);
            if (midRowPos >= position)
            {
                return FindIndexOfRowAtPosition(position, startIndex, midIndex);
            }
            else
            {
                return FindIndexOfRowAtPosition(position, midIndex, endIndex);
            }
        }

        private void SetInitialVisibleCells()
        {
            Range currentRange = CurrentVisibleCellsRange();

            for (int i = 0; i < currentRange.count; i++)
            {
                CreateCell(currentRange.from + i, true);
            }

            visibleCells.IndexesRange = currentRange;
        }

        private void RefreshVisibleCells()
        {
            requiresRefresh = false;

            if (isEmpty) return;

            if (Mathf.Abs (_currentPosition - _lastRefreshPosition) < _cellSizes.SizeForRow (0) / 2 + _spacing / 2) {
                return;
            }
            _lastRefreshPosition = _currentPosition;

            Range previousRange = visibleCells.IndexesRange;
            Range currentRange = CurrentVisibleCellsRange();

            if (currentRange.from > previousRange.Last() || currentRange.Last() < previousRange.from)
            {
                CreateCells();
                return;
            }

            if (!previousRange.Equals (currentRange)) 
            {
                RemoveCellsIfNeededWithRanges (previousRange, currentRange);
                CreateCellsIfNeededWithRanges (previousRange, currentRange);

                visibleCells.IndexesRange = currentRange;
            }
        }

        private void RemoveCellsIfNeededWithRanges(Range previousRange, Range currentRange)
        {
            for (int i = previousRange.from; i < currentRange.from; i++)
            {
                MoveCellToReusable(false);
            }

            for (int i = currentRange.Last(); i < previousRange.Last(); i++)
            {
                MoveCellToReusable(true);
            }
        }

        private void CreateCellsIfNeededWithRanges(Range previousRange, Range currentRange)
        {
            for (int i = previousRange.from - 1; i >= currentRange.from; i--)
            {
                CreateCell(i, false);
            }

            for (int i = previousRange.Last() + 1; i <= currentRange.Last(); i++)
            {
                CreateCell(i, true);
            }
        }

        private void CreateCell(int row, bool atEnd)
        {
            TableViewCell cell = _dataSource.CellForRowInTableView(this, row);
            cell = ConfigureCellWithRowAtEnd(cell, row, atEnd);
        }

        private TableViewCell ConfigureCellWithRowAtEnd(TableViewCell cell, int row, bool atEnd)
        {
            cell.RowNumber = row;

            cell.DidHighlightEvent.RemoveListener(CellDidHighlight);
            cell.DidHighlightEvent.AddListener(CellDidHighlight);

            cell.DidSelectEvent.RemoveListener(CellDidSelect);
            cell.DidSelectEvent.AddListener(CellDidSelect);

            visibleCells.SetCellAtIndex(row, cell);

            if (atEnd)
            {
                cell.transform.SetSiblingIndex(tableViewScroll.content.childCount - 1);
            }
            else
            {
                cell.transform.SetSiblingIndex(0);
            }

            if (!isVertical) {
                cell.transform.SetLocalPositionX (-PositionForRow (row));
            } else {
                cell.transform.SetLocalPositionY (-PositionForRow (row));
            }

            return cell;
        }

        private void MoveCellToReusable(bool last)
        {
            int row = last ? visibleCells.IndexesRange.Last() : visibleCells.IndexesRange.from;
            TableViewCell removedCell = visibleCells.GetCellAtIndex(row);
            
            removedCell.DidHighlightEvent.RemoveAllListeners();
            removedCell.DidSelectEvent.RemoveAllListeners();

            reusableCells.AddReusableCell(removedCell);
            visibleCells.RemoveCellAtIndex(row);
            visibleCells.IndexesRange.count -= 1;

            if (!last)
            {
                visibleCells.IndexesRange.from += 1;
            }

            // move to overboard
            if (!isVertical) {
                removedCell.transform.SetLocalPositionX (_cellSizes.SizeForRow(row));
            } else {
                removedCell.transform.SetLocalPositionY (_cellSizes.SizeForRow(row));
            }
        }

        private void CellDidHighlight(int row)
        {
            if (_tableViewDelegate != null)
            {
                _tableViewDelegate.TableViewDidHighlightCellForRow(this, row);
            }

            if (scrollToHighlighted)
            {
//                ScrollToCell (row);
            }
        }

        private void CellDidSelect(int row)
        {
            if (_tableViewDelegate != null)
            {
                _tableViewDelegate.TableViewDidSelectCellForRow(this, row);
            }
        }

        #endregion


        #region BaseScrollContent

        [Serializable]
        public class SelectCellEvent : UnityEvent<TableViewCell>
        {
        }

        [SerializeField]
        public SelectCellEvent onSelectCell = new SelectCellEvent();

        public TableViewCell SelectedCell { get; protected set; }

        [SerializeField]
        private float _MinScrollVelocity = 0;
        public float MinScrollVelocity { get { return _MinScrollVelocity; } set { _MinScrollVelocity = value; } }

        private void CheckSelectedCell()
        {
            if (SelectedCell != null) {
                float distanceToSelected = Mathf.Abs (_currentPosition - PositionForRow (SelectedCell.RowNumber));
                if (distanceToSelected <= _cellSizes.SizeForRow (SelectedCell.RowNumber) / 2 + _spacing / 2) {
                    return; // selected cell not changed
                }
            }

            TableViewCell nearestToCenter = GetCurrentCellOnCenter();

            if (nearestToCenter != SelectedCell)
            {
                SelectedCell = nearestToCenter;
                onSelectCell.Invoke(SelectedCell);
            }
        }

        public int GetCurrentCellRow()
        {
            if (SelectedCell != null) {
                return SelectedCell.RowNumber;
            } else {
                return 0;
            }
        }

        private TableViewCell GetCurrentCellOnCenter()
        {
            TableViewCell nearestCell = null;
            float nearestItemDistance = float.MaxValue;

            foreach (KeyValuePair<int, TableViewCell> cell in visibleCells.cells)
            {
                float distance = Mathf.Abs(_currentPosition - PositionForRow(cell.Key));
                if (distance < nearestItemDistance)
                {
                    nearestItemDistance = distance;
                    nearestCell = cell.Value;
                }
            }

            return nearestCell;
        }

        public int GetNearestCellRow(float deltaPos)
        {
            // Current cell on center
            TableViewCell centerCell = SelectedCell;
            int centerCellRow = centerCell.RowNumber;

            if (deltaPos == 0)
                return centerCellRow;

            float newPos = _currentPosition + deltaPos;

            int nearestCellRow = centerCellRow;
            float nearestCellPos = PositionForRow(centerCellRow);

            if (deltaPos > 0) {
                if (centerCellRow == NumberOfRows () - 1) {
                    return centerCellRow;
                }

                for (int i = centerCellRow + 1; i < NumberOfRows (); i++) {
                    float currentCellPos = PositionForRow (i);

                    if (Mathf.Abs (newPos - currentCellPos) < Mathf.Abs (newPos - nearestCellPos)) {
                        nearestCellPos = currentCellPos;
                        nearestCellRow = i;
                    } else {
                        // если больше, то дальше смотреть смысла нет
                        break;
                    }
                }
            } else {
                if (centerCellRow == 0) {
                    return centerCellRow;
                }

                for (int i = centerCellRow - 1; i >= 0; i--) {
                    float currentCellPos = PositionForRow (i);

                    if (Mathf.Abs (newPos - currentCellPos) < Mathf.Abs (newPos - nearestCellPos)) {
                        nearestCellPos = currentCellPos;
                        nearestCellRow = i;
                    } else {
                        // если больше, то дальше смотреть смысла нет
                        break;
                    }
                }
            }


            return nearestCellRow;
        }

        public int NumberOfRows()
        {
            return _dataSource.NumberOfRowsInTableView(this);
        }

        private void OnDragBegin()
        {
            _isDragging = true;
            KillTween ();
        }

        private void OnDragEnd()
        {
            _isDragging = false;
            // Если стояли на месте
            if (Mathf.Abs(tableViewScroll.velocity.magnitude) == 0) {
                return;
            }

//            Log.Write ("OnDragEnd y=" + tableViewScroll.velocity.y.ToString ());

            float s = 0;
            if (isVertical) {
                s = tableViewScroll.velocity.y / 2;
            } else {
                s = tableViewScroll.velocity.x / 2;
            }

            int nearesCellRow = SelectedCell.RowNumber;
            if (Math.Abs (s) >= Math.Abs (MinScrollVelocity / 2)) {
                nearesCellRow = GetNearestCellRow (s);
            }

            tableViewScroll.velocity = new Vector2 ();

            // Пеpесчитываем вpемя, чтобы не скpоллить слишком медленно к кpайним элементам
            float time = Mathf.Min(0.5f, Mathf.Abs(Mathf.Ceil(s / tableViewScroll.velocity.magnitude)));

            ScrollToCell (nearesCellRow, time);
        }

        #endregion
    }
}