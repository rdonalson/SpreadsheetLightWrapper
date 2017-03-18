﻿using System;
using System.Collections.Generic;
using System.Globalization;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Ups.Toolkit.SpreadsheetLight.Core.Charts;
using Ups.Toolkit.SpreadsheetLight.Core.Drawing;
using Ups.Toolkit.SpreadsheetLight.Core.misc;
using Ups.Toolkit.SpreadsheetLight.Core.pivottable;
using Ups.Toolkit.SpreadsheetLight.Core.sparkline;
using Ups.Toolkit.SpreadsheetLight.Core.style;
using Ups.Toolkit.SpreadsheetLight.Core.table;
using Ups.Toolkit.SpreadsheetLight.Core.worksheet;
using A = DocumentFormat.OpenXml.Drawing;
using C = DocumentFormat.OpenXml.Drawing.Charts;
using Color = System.Drawing.Color;
using SLFill = Ups.Toolkit.SpreadsheetLight.Core.style.SLFill;
using SLGradientFill = Ups.Toolkit.SpreadsheetLight.Core.style.SLGradientFill;

namespace Ups.Toolkit.SpreadsheetLight.Core
{
    public partial class SLDocument
    {
        /// <summary>
        ///     Creates an instance of SLFont with theme information.
        /// </summary>
        /// <returns>An SLFont with theme information.</returns>
        public SLFont CreateFont()
        {
            return new SLFont(SimpleTheme.MajorLatinFont, SimpleTheme.MinorLatinFont, SimpleTheme.listThemeColors,
                SimpleTheme.listIndexedColors);
        }

        /// <summary>
        ///     Creates an instance of SLPatternFill with theme information.
        /// </summary>
        /// <returns>An SLPatternFill with theme information.</returns>
        public SLPatternFill CreatePatternFill()
        {
            return new SLPatternFill(SimpleTheme.listThemeColors, SimpleTheme.listIndexedColors);
        }

        /// <summary>
        ///     Creates an instance of SLGradientFill with theme information.
        /// </summary>
        /// <returns>An SLGradientFill with theme information.</returns>
        public SLGradientFill CreateGradientFill()
        {
            return new SLGradientFill(SimpleTheme.listThemeColors, SimpleTheme.listIndexedColors);
        }

        /// <summary>
        ///     Creates an instance of SLFill with theme information.
        /// </summary>
        /// <returns>An SLFill with theme information.</returns>
        public SLFill CreateFill()
        {
            return new SLFill(SimpleTheme.listThemeColors, SimpleTheme.listIndexedColors);
        }

        /// <summary>
        ///     Creates an instance of SLBorder with theme information.
        /// </summary>
        /// <returns>An SLBorder with theme information.</returns>
        public SLBorder CreateBorder()
        {
            return new SLBorder(SimpleTheme.listThemeColors, SimpleTheme.listIndexedColors);
        }

        /// <summary>
        ///     Creates an instance of SLRstType with theme information.
        /// </summary>
        /// <returns>An SLRstType with theme information.</returns>
        public SLRstType CreateRstType()
        {
            return new SLRstType(SimpleTheme.MajorLatinFont, SimpleTheme.MinorLatinFont, SimpleTheme.listThemeColors,
                SimpleTheme.listIndexedColors);
        }

        /// <summary>
        ///     Creates an instance of SLStyle with theme information.
        /// </summary>
        /// <returns>An SLStyle with theme information.</returns>
        public SLStyle CreateStyle()
        {
            return new SLStyle(SimpleTheme.MajorLatinFont, SimpleTheme.MinorLatinFont, SimpleTheme.listThemeColors,
                SimpleTheme.listIndexedColors);
        }

        /// <summary>
        ///     Creates an instance of SLComment with theme information.
        /// </summary>
        /// <returns>An SLComment with theme information.</returns>
        public SLComment CreateComment()
        {
            var comm = new SLComment(SimpleTheme.listThemeColors);
            if (DocumentProperties.Creator.Length > 0)
                comm.Author = DocumentProperties.Creator;
            else
                comm.Author = SLConstants.ApplicationName;

            return comm;
        }

        /// <summary>
        ///     Creates an instance of SLDataValidation.
        /// </summary>
        /// <param name="CellReference">The cell reference, such as "A1".</param>
        /// <returns>An SLDataValidation.</returns>
        public SLDataValidation CreateDataValidation(string CellReference)
        {
            var iRowIndex = -1;
            var iColumnIndex = -1;
            if (!SLTool.FormatCellReferenceToRowColumnIndex(CellReference, out iRowIndex, out iColumnIndex))
            {
                iRowIndex = 1;
                iColumnIndex = 1;
            }

            var dv = new SLDataValidation();
            dv.InitialiseDataValidation(iRowIndex, iColumnIndex, iRowIndex, iColumnIndex,
                slwb.WorkbookProperties.Date1904);
            return dv;
        }

        /// <summary>
        ///     Creates an instance of SLDataValidation.
        /// </summary>
        /// <param name="StartCellReference">
        ///     The cell reference of the start cell of the cell range, such as "A1". This is
        ///     typically the top-left cell.
        /// </param>
        /// <param name="EndCellReference">
        ///     The cell reference of the end cell of the cell range, such as "A1". This is typically
        ///     the bottom-right cell.
        /// </param>
        /// <returns>An SLDataValidation.</returns>
        public SLDataValidation CreateDataValidation(string StartCellReference, string EndCellReference)
        {
            var iStartRowIndex = -1;
            var iStartColumnIndex = -1;
            var iEndRowIndex = -1;
            var iEndColumnIndex = -1;
            if (!SLTool.FormatCellReferenceToRowColumnIndex(StartCellReference, out iStartRowIndex,
                    out iStartColumnIndex)
                || !SLTool.FormatCellReferenceToRowColumnIndex(EndCellReference, out iEndRowIndex, out iEndColumnIndex))
            {
                iStartRowIndex = 1;
                iStartColumnIndex = 1;
                iEndRowIndex = 1;
                iEndColumnIndex = 1;
            }

            var dv = new SLDataValidation();
            dv.InitialiseDataValidation(iStartRowIndex, iStartColumnIndex, iEndRowIndex, iEndColumnIndex,
                slwb.WorkbookProperties.Date1904);
            return dv;
        }

        /// <summary>
        ///     Creates an instance of SLDataValidation.
        /// </summary>
        /// <param name="RowIndex">The row index.</param>
        /// <param name="ColumnIndex">The column index.</param>
        /// <returns>An SLDataValidation.</returns>
        public SLDataValidation CreateDataValidation(int RowIndex, int ColumnIndex)
        {
            var dv = new SLDataValidation();
            dv.InitialiseDataValidation(RowIndex, ColumnIndex, RowIndex, ColumnIndex, slwb.WorkbookProperties.Date1904);
            return dv;
        }

        /// <summary>
        ///     Creates an instance of SLDataValidation.
        /// </summary>
        /// <param name="StartRowIndex">The row index of the start row.</param>
        /// <param name="StartColumnIndex">The column index of the start column.</param>
        /// <param name="EndRowIndex">The row index of the end row.</param>
        /// <param name="EndColumnIndex">The column index of the end column.</param>
        /// <returns>An SLDataValidation.</returns>
        public SLDataValidation CreateDataValidation(int StartRowIndex, int StartColumnIndex, int EndRowIndex,
            int EndColumnIndex)
        {
            var dv = new SLDataValidation();
            dv.InitialiseDataValidation(StartRowIndex, StartColumnIndex, EndRowIndex, EndColumnIndex,
                slwb.WorkbookProperties.Date1904);
            return dv;
        }

        /// <summary>
        ///     Creates an instance of SLTable, given cell references of opposite cells in a cell range.
        /// </summary>
        /// <param name="StartCellReference">
        ///     The cell reference of the start cell of the cell range to be in the table, such as
        ///     "A1". This is typically the top-left cell.
        /// </param>
        /// <param name="EndCellReference">
        ///     The cell reference of the end cell of the cell range to be in the table, such as "A1".
        ///     This is typically the bottom-right cell.
        /// </param>
        /// <returns>An SLTable with the required information.</returns>
        public SLTable CreateTable(string StartCellReference, string EndCellReference)
        {
            var iStartRowIndex = -1;
            var iStartColumnIndex = -1;
            var iEndRowIndex = -1;
            var iEndColumnIndex = -1;
            if (
                !SLTool.FormatCellReferenceToRowColumnIndex(StartCellReference, out iStartRowIndex,
                    out iStartColumnIndex))
            {
                iStartRowIndex = -1;
                iStartColumnIndex = -1;
            }
            if (!SLTool.FormatCellReferenceToRowColumnIndex(EndCellReference, out iEndRowIndex, out iEndColumnIndex))
            {
                iEndRowIndex = -1;
                iEndColumnIndex = -1;
            }

            return CreateTable(iStartRowIndex, iStartColumnIndex, iEndRowIndex, iEndColumnIndex);
        }

        /// <summary>
        ///     Creates an instance of SLTable, given row and column indices of opposite cells in a cell range.
        /// </summary>
        /// <param name="StartRowIndex">The row index of the start row. This is typically the top row.</param>
        /// <param name="StartColumnIndex">The column index of the start column. This is typically the left-most column.</param>
        /// <param name="EndRowIndex">The row index of the end row. This is typically the bottom row.</param>
        /// <param name="EndColumnIndex">The column index of the end column. This is typically the right-most column.</param>
        /// <returns>An SLTable with the required information.</returns>
        public SLTable CreateTable(int StartRowIndex, int StartColumnIndex, int EndRowIndex, int EndColumnIndex)
        {
            int iStartRowIndex = 1, iEndRowIndex = 1, iStartColumnIndex = 1, iEndColumnIndex = 1;
            if (StartRowIndex < EndRowIndex)
            {
                iStartRowIndex = StartRowIndex;
                iEndRowIndex = EndRowIndex;
            }
            else
            {
                iStartRowIndex = EndRowIndex;
                iEndRowIndex = StartRowIndex;
            }

            if (StartColumnIndex < EndColumnIndex)
            {
                iStartColumnIndex = StartColumnIndex;
                iEndColumnIndex = EndColumnIndex;
            }
            else
            {
                iStartColumnIndex = EndColumnIndex;
                iEndColumnIndex = StartColumnIndex;
            }

            if (iStartRowIndex < 1) iStartRowIndex = 1;
            if (iStartRowIndex == SLConstants.RowLimit) iStartRowIndex = SLConstants.RowLimit - 1;
            if (iStartColumnIndex < 1) iStartColumnIndex = 1;
            // consider minus 1 in case there's a totals row so there's less checking...
            if (iEndRowIndex > SLConstants.RowLimit) iEndRowIndex = SLConstants.RowLimit;
            if (iEndColumnIndex > SLConstants.ColumnLimit) iEndColumnIndex = SLConstants.ColumnLimit;

            if (iEndRowIndex <= iStartRowIndex) iEndRowIndex = iStartRowIndex + 1;

            var tbl = new SLTable();
            tbl.SetAllNull();

            slwb.RefreshPossibleTableId();
            tbl.Id = slwb.PossibleTableId;
            tbl.DisplayName = string.Format("Table{0}", tbl.Id);
            tbl.Name = tbl.DisplayName;

            tbl.StartRowIndex = iStartRowIndex;
            tbl.StartColumnIndex = iStartColumnIndex;
            tbl.EndRowIndex = iEndRowIndex;
            tbl.EndColumnIndex = iEndColumnIndex;

            tbl.AutoFilter.StartRowIndex = tbl.StartRowIndex;
            tbl.AutoFilter.StartColumnIndex = tbl.StartColumnIndex;
            tbl.AutoFilter.EndRowIndex = tbl.EndRowIndex;
            tbl.AutoFilter.EndColumnIndex = tbl.EndColumnIndex;
            tbl.HasAutoFilter = true;

            SLTableColumn tc;
            uint iColumnId = 1;
            int i, index;
            uint j;
            SLCell c;
            SLCellPoint pt;
            var sHeaderText = string.Empty;
            SharedStringItem ssi;
            var rst = new SLRstType(SLConstants.OfficeThemeMajorLatinFont, SLConstants.OfficeThemeMinorLatinFont,
                new List<Color>(), new List<Color>());
            for (i = tbl.StartColumnIndex; i <= tbl.EndColumnIndex; ++i)
            {
                pt = new SLCellPoint(StartRowIndex, i);
                sHeaderText = string.Empty;
                if (slws.Cells.ContainsKey(pt))
                {
                    c = slws.Cells[pt];
                    if (c.CellText == null)
                        if (c.DataType == CellValues.Number)
                            sHeaderText = c.NumericValue.ToString(CultureInfo.InvariantCulture);
                        else if (c.DataType == CellValues.Boolean) sHeaderText = c.NumericValue > 0.5 ? "TRUE" : "FALSE";
                        else sHeaderText = string.Empty;
                    else
                        sHeaderText = c.CellText;

                    if (c.DataType == CellValues.SharedString)
                    {
                        index = -1;
                        if (c.CellText != null)
                        {
                            if (int.TryParse(c.CellText, out index))
                                index = -1;
                        }
                        else
                        {
                            index = Convert.ToInt32(c.NumericValue);
                        }

                        if ((index >= 0) && (index < listSharedString.Count))
                        {
                            ssi = new SharedStringItem();
                            ssi.InnerXml = listSharedString[index];
                            rst.FromSharedStringItem(ssi);
                            sHeaderText = rst.ToPlainString();
                        }
                    }
                }

                j = iColumnId;
                if (sHeaderText.Length == 0)
                    sHeaderText = string.Format("Column{0}", j);
                while (tbl.TableNames.Contains(sHeaderText))
                {
                    ++j;
                    sHeaderText = string.Format("Column{0}", j);
                }
                tc = new SLTableColumn();
                tc.Id = iColumnId;
                tc.Name = sHeaderText;
                tbl.TableColumns.Add(tc);
                tbl.TableNames.Add(sHeaderText);
                ++iColumnId;
            }

            tbl.TableStyleInfo.ShowFirstColumn = false;
            tbl.TableStyleInfo.ShowLastColumn = false;
            tbl.TableStyleInfo.ShowRowStripes = true;
            tbl.TableStyleInfo.ShowColumnStripes = false;
            tbl.HasTableStyleInfo = true;

            return tbl;
        }

        public SLPivotTable CreatePivotTable(string StartCellReference, string EndCellReference)
        {
            var iStartRowIndex = -1;
            var iStartColumnIndex = -1;
            var iEndRowIndex = -1;
            var iEndColumnIndex = -1;
            if (
                !SLTool.FormatCellReferenceToRowColumnIndex(StartCellReference, out iStartRowIndex,
                    out iStartColumnIndex))
            {
                iStartRowIndex = -1;
                iStartColumnIndex = -1;
            }
            if (!SLTool.FormatCellReferenceToRowColumnIndex(EndCellReference, out iEndRowIndex, out iEndColumnIndex))
            {
                iEndRowIndex = -1;
                iEndColumnIndex = -1;
            }

            return CreatePivotTable(iStartRowIndex, iStartColumnIndex, iEndRowIndex, iEndColumnIndex);
        }

        public SLPivotTable CreatePivotTable(int StartRowIndex, int StartColumnIndex, int EndRowIndex,
            int EndColumnIndex)
        {
            int iStartRowIndex = 1, iEndRowIndex = 1, iStartColumnIndex = 1, iEndColumnIndex = 1;
            if (StartRowIndex < EndRowIndex)
            {
                iStartRowIndex = StartRowIndex;
                iEndRowIndex = EndRowIndex;
            }
            else
            {
                iStartRowIndex = EndRowIndex;
                iEndRowIndex = StartRowIndex;
            }

            if (StartColumnIndex < EndColumnIndex)
            {
                iStartColumnIndex = StartColumnIndex;
                iEndColumnIndex = EndColumnIndex;
            }
            else
            {
                iStartColumnIndex = EndColumnIndex;
                iEndColumnIndex = StartColumnIndex;
            }

            // not checking bounds because we're going to be more stringent on the data source range.

            var pivot = new SLPivotTable();
            slwb.RefreshPossiblePivotTableCacheId();
            pivot.CacheId = slwb.PossiblePivotTableCacheId;
            pivot.Name = slwb.GetNextPossiblePivotTableName();

            pivot.IsDataSourceTable = false;
            pivot.SheetTableName = gsSelectedWorksheetName;

            return pivot;
        }

        /// <summary>
        ///     Creates an instance of SLSparklineGroup, given cell references of opposite cells in a cell range.
        /// </summary>
        /// <param name="StartCellReference">
        ///     The cell reference of the start cell of the cell range to be in the sparkline, such as
        ///     "A1". This is typically the top-left cell.
        /// </param>
        /// <param name="EndCellReference">
        ///     The cell reference of the end cell of the cell range to be in the sparkline, such as
        ///     "A1". This is typically the bottom-right cell.
        /// </param>
        /// <returns>An SLSparklineGroup with the required information.</returns>
        public SLSparklineGroup CreateSparklineGroup(string StartCellReference, string EndCellReference)
        {
            var iStartRowIndex = -1;
            var iStartColumnIndex = -1;
            var iEndRowIndex = -1;
            var iEndColumnIndex = -1;
            if (
                !SLTool.FormatCellReferenceToRowColumnIndex(StartCellReference, out iStartRowIndex,
                    out iStartColumnIndex))
            {
                iStartRowIndex = -1;
                iStartColumnIndex = -1;
            }
            if (!SLTool.FormatCellReferenceToRowColumnIndex(EndCellReference, out iEndRowIndex, out iEndColumnIndex))
            {
                iEndRowIndex = -1;
                iEndColumnIndex = -1;
            }

            return CreateSparklineGroup(gsSelectedWorksheetName, iStartRowIndex, iStartColumnIndex, iEndRowIndex,
                iEndColumnIndex);
        }

        /// <summary>
        ///     Creates an instance of SLSparklineGroup, given cell references of opposite cells in a cell range.
        /// </summary>
        /// <param name="WorksheetName">The name of the source worksheet.</param>
        /// <param name="StartCellReference">
        ///     The cell reference of the start cell of the cell range to be in the sparkline, such as
        ///     "A1". This is typically the top-left cell.
        /// </param>
        /// <param name="EndCellReference">
        ///     The cell reference of the end cell of the cell range to be in the sparkline, such as
        ///     "A1". This is typically the bottom-right cell.
        /// </param>
        /// <returns>An SLSparklineGroup with the required information.</returns>
        public SLSparklineGroup CreateSparklineGroup(string WorksheetName, string StartCellReference,
            string EndCellReference)
        {
            var iStartRowIndex = -1;
            var iStartColumnIndex = -1;
            var iEndRowIndex = -1;
            var iEndColumnIndex = -1;
            if (
                !SLTool.FormatCellReferenceToRowColumnIndex(StartCellReference, out iStartRowIndex,
                    out iStartColumnIndex))
            {
                iStartRowIndex = -1;
                iStartColumnIndex = -1;
            }
            if (!SLTool.FormatCellReferenceToRowColumnIndex(EndCellReference, out iEndRowIndex, out iEndColumnIndex))
            {
                iEndRowIndex = -1;
                iEndColumnIndex = -1;
            }

            return CreateSparklineGroup(WorksheetName, iStartRowIndex, iStartColumnIndex, iEndRowIndex, iEndColumnIndex);
        }

        /// <summary>
        ///     Creates an instance of SLSparklineGroup, given row and column indices of opposite cells in a cell range.
        /// </summary>
        /// <param name="StartRowIndex">The row index of the start row. This is typically the top row.</param>
        /// <param name="StartColumnIndex">The column index of the start column. This is typically the left-most column.</param>
        /// <param name="EndRowIndex">The row index of the end row. This is typically the bottom row.</param>
        /// <param name="EndColumnIndex">The column index of the end column. This is typically the right-most column.</param>
        /// <returns>An SLSparklineGroup with the required information.</returns>
        public SLSparklineGroup CreateSparklineGroup(int StartRowIndex, int StartColumnIndex, int EndRowIndex,
            int EndColumnIndex)
        {
            return CreateSparklineGroup(gsSelectedWorksheetName, StartRowIndex, StartColumnIndex, EndRowIndex,
                EndColumnIndex);
        }

        /// <summary>
        ///     Creates an instance of SLSparklineGroup, given row and column indices of opposite cells in a cell range.
        /// </summary>
        /// <param name="WorksheetName">The name of the source worksheet.</param>
        /// <param name="StartRowIndex">The row index of the start row. This is typically the top row.</param>
        /// <param name="StartColumnIndex">The column index of the start column. This is typically the left-most column.</param>
        /// <param name="EndRowIndex">The row index of the end row. This is typically the bottom row.</param>
        /// <param name="EndColumnIndex">The column index of the end column. This is typically the right-most column.</param>
        /// <returns>An SLSparklineGroup with the required information.</returns>
        public SLSparklineGroup CreateSparklineGroup(string WorksheetName, int StartRowIndex, int StartColumnIndex,
            int EndRowIndex, int EndColumnIndex)
        {
            var spk = new SLSparklineGroup(SimpleTheme.listThemeColors, SimpleTheme.listIndexedColors);

            int iStartRowIndex = 1, iEndRowIndex = 1, iStartColumnIndex = 1, iEndColumnIndex = 1;
            if (StartRowIndex < EndRowIndex)
            {
                iStartRowIndex = StartRowIndex;
                iEndRowIndex = EndRowIndex;
            }
            else
            {
                iStartRowIndex = EndRowIndex;
                iEndRowIndex = StartRowIndex;
            }

            if (StartColumnIndex < EndColumnIndex)
            {
                iStartColumnIndex = StartColumnIndex;
                iEndColumnIndex = EndColumnIndex;
            }
            else
            {
                iStartColumnIndex = EndColumnIndex;
                iEndColumnIndex = StartColumnIndex;
            }

            if (iStartRowIndex < 1) iStartRowIndex = 1;
            if (iStartColumnIndex < 1) iStartColumnIndex = 1;
            if (iEndRowIndex > SLConstants.RowLimit) iEndRowIndex = SLConstants.RowLimit;
            if (iEndColumnIndex > SLConstants.ColumnLimit) iEndColumnIndex = SLConstants.ColumnLimit;

            spk.WorksheetName = WorksheetName;
            spk.StartRowIndex = iStartRowIndex;
            spk.StartColumnIndex = iStartColumnIndex;
            spk.EndRowIndex = iEndRowIndex;
            spk.EndColumnIndex = iEndColumnIndex;

            // this seems to be the "default"
            spk.SetSparklineStyle(SLSparklineStyle.Accent1Darker50Percent);

            return spk;
        }

        /// <summary>
        ///     Creates an instance of SLChart, given cell references of opposite cells in a cell range.
        /// </summary>
        /// <param name="StartCellReference">
        ///     The cell reference of the start cell of the cell range to be in the chart, such as
        ///     "A1". This is typically the top-left cell.
        /// </param>
        /// <param name="EndCellReference">
        ///     The cell reference of the end cell of the cell range to be in the chart, such as "A1".
        ///     This is typically the bottom-right cell.
        /// </param>
        /// <returns>An SLChart with the required information.</returns>
        public SLChart CreateChart(string StartCellReference, string EndCellReference)
        {
            var iStartRowIndex = -1;
            var iStartColumnIndex = -1;
            var iEndRowIndex = -1;
            var iEndColumnIndex = -1;
            if (
                !SLTool.FormatCellReferenceToRowColumnIndex(StartCellReference, out iStartRowIndex,
                    out iStartColumnIndex))
            {
                iStartRowIndex = -1;
                iStartColumnIndex = -1;
            }
            if (!SLTool.FormatCellReferenceToRowColumnIndex(EndCellReference, out iEndRowIndex, out iEndColumnIndex))
            {
                iEndRowIndex = -1;
                iEndColumnIndex = -1;
            }

            return CreateChartInternal(gsSelectedWorksheetName, iStartRowIndex, iStartColumnIndex, iEndRowIndex,
                iEndColumnIndex, new SLCreateChartOptions());
        }

        /// <summary>
        ///     <strong>Obsolete. </strong>Creates an instance of SLChart, given cell references of opposite cells in a cell range
        ///     and whether rows or columns contain the data series and if hidden data is used.
        /// </summary>
        /// <param name="StartCellReference">
        ///     The cell reference of the start cell of the cell range to be in the chart, such as
        ///     "A1". This is typically the top-left cell.
        /// </param>
        /// <param name="EndCellReference">
        ///     The cell reference of the end cell of the cell range to be in the chart, such as "A1".
        ///     This is typically the bottom-right cell.
        /// </param>
        /// <param name="RowsAsDataSeries">True if rows contain the data series. False if columns contain the data series.</param>
        /// <param name="ShowHiddenData">True if hidden data is used in the chart. False otherwise.</param>
        /// <returns>An SLChart with the required information.</returns>
        [Obsolete("Use an overload with the SLCreateChartOptions parameter instead.")]
        public SLChart CreateChart(string StartCellReference, string EndCellReference, bool RowsAsDataSeries,
            bool ShowHiddenData)
        {
            var iStartRowIndex = -1;
            var iStartColumnIndex = -1;
            var iEndRowIndex = -1;
            var iEndColumnIndex = -1;
            if (
                !SLTool.FormatCellReferenceToRowColumnIndex(StartCellReference, out iStartRowIndex,
                    out iStartColumnIndex))
            {
                iStartRowIndex = -1;
                iStartColumnIndex = -1;
            }
            if (!SLTool.FormatCellReferenceToRowColumnIndex(EndCellReference, out iEndRowIndex, out iEndColumnIndex))
            {
                iEndRowIndex = -1;
                iEndColumnIndex = -1;
            }

            var Options = new SLCreateChartOptions();
            Options.RowsAsDataSeries = RowsAsDataSeries;
            Options.ShowHiddenData = ShowHiddenData;
            return CreateChartInternal(gsSelectedWorksheetName, iStartRowIndex, iStartColumnIndex, iEndRowIndex,
                iEndColumnIndex, Options);
        }

        /// <summary>
        ///     Creates an instance of SLChart, given cell references of opposite cells in a cell range.
        /// </summary>
        /// <param name="StartCellReference">
        ///     The cell reference of the start cell of the cell range to be in the chart, such as
        ///     "A1". This is typically the top-left cell.
        /// </param>
        /// <param name="EndCellReference">
        ///     The cell reference of the end cell of the cell range to be in the chart, such as "A1".
        ///     This is typically the bottom-right cell.
        /// </param>
        /// <param name="Options">Chart creation options.</param>
        /// <returns>An SLChart with the required information.</returns>
        public SLChart CreateChart(string StartCellReference, string EndCellReference,
            SLCreateChartOptions Options)
        {
            var iStartRowIndex = -1;
            var iStartColumnIndex = -1;
            var iEndRowIndex = -1;
            var iEndColumnIndex = -1;
            if (
                !SLTool.FormatCellReferenceToRowColumnIndex(StartCellReference, out iStartRowIndex,
                    out iStartColumnIndex))
            {
                iStartRowIndex = -1;
                iStartColumnIndex = -1;
            }
            if (!SLTool.FormatCellReferenceToRowColumnIndex(EndCellReference, out iEndRowIndex, out iEndColumnIndex))
            {
                iEndRowIndex = -1;
                iEndColumnIndex = -1;
            }

            return CreateChartInternal(gsSelectedWorksheetName, iStartRowIndex, iStartColumnIndex, iEndRowIndex,
                iEndColumnIndex, Options);
        }

        /// <summary>
        ///     Creates an instance of SLChart, given cell references of opposite cells in a cell range.
        /// </summary>
        /// <param name="WorksheetName">The name of the source worksheet.</param>
        /// <param name="StartCellReference">
        ///     The cell reference of the start cell of the cell range to be in the chart, such as
        ///     "A1". This is typically the top-left cell.
        /// </param>
        /// <param name="EndCellReference">
        ///     The cell reference of the end cell of the cell range to be in the chart, such as "A1".
        ///     This is typically the bottom-right cell.
        /// </param>
        /// <returns>An SLChart with the required information.</returns>
        public SLChart CreateChart(string WorksheetName, string StartCellReference, string EndCellReference)
        {
            var iStartRowIndex = -1;
            var iStartColumnIndex = -1;
            var iEndRowIndex = -1;
            var iEndColumnIndex = -1;
            if (
                !SLTool.FormatCellReferenceToRowColumnIndex(StartCellReference, out iStartRowIndex,
                    out iStartColumnIndex))
            {
                iStartRowIndex = -1;
                iStartColumnIndex = -1;
            }
            if (!SLTool.FormatCellReferenceToRowColumnIndex(EndCellReference, out iEndRowIndex, out iEndColumnIndex))
            {
                iEndRowIndex = -1;
                iEndColumnIndex = -1;
            }

            return CreateChartInternal(WorksheetName, iStartRowIndex, iStartColumnIndex, iEndRowIndex, iEndColumnIndex,
                new SLCreateChartOptions());
        }

        /// <summary>
        ///     <strong>Obsolete. </strong>Creates an instance of SLChart, given cell references of opposite cells in a cell range
        ///     and whether rows or columns contain the data series.
        /// </summary>
        /// <param name="WorksheetName">The name of the source worksheet.</param>
        /// <param name="StartCellReference">
        ///     The cell reference of the start cell of the cell range to be in the chart, such as
        ///     "A1". This is typically the top-left cell.
        /// </param>
        /// <param name="EndCellReference">
        ///     The cell reference of the end cell of the cell range to be in the chart, such as "A1".
        ///     This is typically the bottom-right cell.
        /// </param>
        /// <param name="RowsAsDataSeries">True if rows contain the data series. False if columns contain the data series.</param>
        /// <param name="ShowHiddenData">True if hidden data is used in the chart. False otherwise.</param>
        /// <returns>An SLChart with the required information.</returns>
        [Obsolete("Use an overload with the SLCreateChartOptions parameter instead.")]
        public SLChart CreateChart(string WorksheetName, string StartCellReference, string EndCellReference,
            bool RowsAsDataSeries, bool ShowHiddenData)
        {
            var iStartRowIndex = -1;
            var iStartColumnIndex = -1;
            var iEndRowIndex = -1;
            var iEndColumnIndex = -1;
            if (
                !SLTool.FormatCellReferenceToRowColumnIndex(StartCellReference, out iStartRowIndex,
                    out iStartColumnIndex))
            {
                iStartRowIndex = -1;
                iStartColumnIndex = -1;
            }
            if (!SLTool.FormatCellReferenceToRowColumnIndex(EndCellReference, out iEndRowIndex, out iEndColumnIndex))
            {
                iEndRowIndex = -1;
                iEndColumnIndex = -1;
            }

            var Options = new SLCreateChartOptions();
            Options.RowsAsDataSeries = RowsAsDataSeries;
            Options.ShowHiddenData = ShowHiddenData;
            return CreateChartInternal(WorksheetName, iStartRowIndex, iStartColumnIndex, iEndRowIndex, iEndColumnIndex,
                Options);
        }

        /// <summary>
        ///     Creates an instance of SLChart, given cell references of opposite cells in a cell range.
        /// </summary>
        /// <param name="WorksheetName">The name of the source worksheet.</param>
        /// <param name="StartCellReference">
        ///     The cell reference of the start cell of the cell range to be in the chart, such as
        ///     "A1". This is typically the top-left cell.
        /// </param>
        /// <param name="EndCellReference">
        ///     The cell reference of the end cell of the cell range to be in the chart, such as "A1".
        ///     This is typically the bottom-right cell.
        /// </param>
        /// <param name="Options">Chart creation options.</param>
        /// <returns>An SLChart with the required information.</returns>
        public SLChart CreateChart(string WorksheetName, string StartCellReference, string EndCellReference,
            SLCreateChartOptions Options)
        {
            var iStartRowIndex = -1;
            var iStartColumnIndex = -1;
            var iEndRowIndex = -1;
            var iEndColumnIndex = -1;
            if (
                !SLTool.FormatCellReferenceToRowColumnIndex(StartCellReference, out iStartRowIndex,
                    out iStartColumnIndex))
            {
                iStartRowIndex = -1;
                iStartColumnIndex = -1;
            }
            if (!SLTool.FormatCellReferenceToRowColumnIndex(EndCellReference, out iEndRowIndex, out iEndColumnIndex))
            {
                iEndRowIndex = -1;
                iEndColumnIndex = -1;
            }

            return CreateChartInternal(WorksheetName, iStartRowIndex, iStartColumnIndex, iEndRowIndex, iEndColumnIndex,
                Options);
        }

        /// <summary>
        ///     Creates an instance of SLChart, given row and column indices of opposite cells in a cell range.
        /// </summary>
        /// <param name="StartRowIndex">The row index of the start row. This is typically the top row.</param>
        /// <param name="StartColumnIndex">The column index of the start column. This is typically the left-most column.</param>
        /// <param name="EndRowIndex">The row index of the end row. This is typically the bottom row.</param>
        /// <param name="EndColumnIndex">The column index of the end column. This is typically the right-most column.</param>
        /// <returns>An SLChart with the required information.</returns>
        public SLChart CreateChart(int StartRowIndex, int StartColumnIndex, int EndRowIndex, int EndColumnIndex)
        {
            return CreateChartInternal(gsSelectedWorksheetName, StartRowIndex, StartColumnIndex, EndRowIndex,
                EndColumnIndex, new SLCreateChartOptions());
        }

        /// <summary>
        ///     <strong>Obsolete. </strong>Creates an instance of SLChart, given row and column indices of opposite cells in a cell
        ///     range and whether rows or columns contain the data series and if hidden data is used.
        /// </summary>
        /// <param name="StartRowIndex">The row index of the start row. This is typically the top row.</param>
        /// <param name="StartColumnIndex">The column index of the start column. This is typically the left-most column.</param>
        /// <param name="EndRowIndex">The row index of the end row. This is typically the bottom row.</param>
        /// <param name="EndColumnIndex">The column index of the end column. This is typically the right-most column.</param>
        /// <param name="RowsAsDataSeries">True if rows contain the data series. False if columns contain the data series.</param>
        /// <param name="ShowHiddenData">True if hidden data is used in the chart. False otherwise.</param>
        /// <returns>An SLChart with the required information.</returns>
        [Obsolete("Use an overload with the SLCreateChartOptions parameter instead.")]
        public SLChart CreateChart(int StartRowIndex, int StartColumnIndex, int EndRowIndex, int EndColumnIndex,
            bool RowsAsDataSeries, bool ShowHiddenData)
        {
            var Options = new SLCreateChartOptions();
            Options.RowsAsDataSeries = RowsAsDataSeries;
            Options.ShowHiddenData = ShowHiddenData;
            return CreateChartInternal(gsSelectedWorksheetName, StartRowIndex, StartColumnIndex, EndRowIndex,
                EndColumnIndex, Options);
        }

        /// <summary>
        ///     Creates an instance of SLChart, given row and column indices of opposite cells in a cell range.
        /// </summary>
        /// <param name="StartRowIndex">The row index of the start row. This is typically the top row.</param>
        /// <param name="StartColumnIndex">The column index of the start column. This is typically the left-most column.</param>
        /// <param name="EndRowIndex">The row index of the end row. This is typically the bottom row.</param>
        /// <param name="EndColumnIndex">The column index of the end column. This is typically the right-most column.</param>
        /// <param name="Options">Chart creation options.</param>
        /// <returns>An SLChart with the required information.</returns>
        public SLChart CreateChart(int StartRowIndex, int StartColumnIndex, int EndRowIndex, int EndColumnIndex,
            SLCreateChartOptions Options)
        {
            return CreateChartInternal(gsSelectedWorksheetName, StartRowIndex, StartColumnIndex, EndRowIndex,
                EndColumnIndex, Options);
        }

        /// <summary>
        ///     Creates an instance of SLChart, given row and column indices of opposite cells in a cell range.
        /// </summary>
        /// <param name="WorksheetName">The name of the source worksheet.</param>
        /// <param name="StartRowIndex">The row index of the start row. This is typically the top row.</param>
        /// <param name="StartColumnIndex">The column index of the start column. This is typically the left-most column.</param>
        /// <param name="EndRowIndex">The row index of the end row. This is typically the bottom row.</param>
        /// <param name="EndColumnIndex">The column index of the end column. This is typically the right-most column.</param>
        /// <returns>An SLChart with the required information.</returns>
        public SLChart CreateChart(string WorksheetName, int StartRowIndex, int StartColumnIndex, int EndRowIndex,
            int EndColumnIndex)
        {
            return CreateChartInternal(WorksheetName, StartRowIndex, StartColumnIndex, EndRowIndex, EndColumnIndex,
                new SLCreateChartOptions());
        }

        /// <summary>
        ///     <strong>Obsolete. </strong>Creates an instance of SLChart, given row and column indices of opposite cells in a cell
        ///     range and whether rows or columns contain the data series and if hidden data is used.
        /// </summary>
        /// <param name="WorksheetName">The name of the source worksheet.</param>
        /// <param name="StartRowIndex">The row index of the start row. This is typically the top row.</param>
        /// <param name="StartColumnIndex">The column index of the start column. This is typically the left-most column.</param>
        /// <param name="EndRowIndex">The row index of the end row. This is typically the bottom row.</param>
        /// <param name="EndColumnIndex">The column index of the end column. This is typically the right-most column.</param>
        /// <param name="RowsAsDataSeries">True if rows contain the data series. False if columns contain the data series.</param>
        /// <param name="ShowHiddenData">True if hidden data is used in the chart. False otherwise.</param>
        /// <returns>An SLChart with the required information.</returns>
        [Obsolete("Use an overload with the SLCreateChartOptions parameter instead.")]
        public SLChart CreateChart(string WorksheetName, int StartRowIndex, int StartColumnIndex, int EndRowIndex,
            int EndColumnIndex, bool RowsAsDataSeries, bool ShowHiddenData)
        {
            var Options = new SLCreateChartOptions();
            Options.RowsAsDataSeries = RowsAsDataSeries;
            Options.ShowHiddenData = ShowHiddenData;
            return CreateChartInternal(WorksheetName, StartRowIndex, StartColumnIndex, EndRowIndex, EndColumnIndex,
                Options);
        }

        /// <summary>
        ///     Creates an instance of SLChart, given row and column indices of opposite cells in a cell range.
        /// </summary>
        /// <param name="WorksheetName">The name of the source worksheet.</param>
        /// <param name="StartRowIndex">The row index of the start row. This is typically the top row.</param>
        /// <param name="StartColumnIndex">The column index of the start column. This is typically the left-most column.</param>
        /// <param name="EndRowIndex">The row index of the end row. This is typically the bottom row.</param>
        /// <param name="EndColumnIndex">The column index of the end column. This is typically the right-most column.</param>
        /// <param name="Options">Chart creation options.</param>
        /// <returns>An SLChart with the required information.</returns>
        public SLChart CreateChart(string WorksheetName, int StartRowIndex, int StartColumnIndex, int EndRowIndex,
            int EndColumnIndex, SLCreateChartOptions Options)
        {
            return CreateChartInternal(WorksheetName, StartRowIndex, StartColumnIndex, EndRowIndex, EndColumnIndex,
                Options);
        }

        private SLChart CreateChartInternal(string WorksheetName, int StartRowIndex, int StartColumnIndex,
            int EndRowIndex, int EndColumnIndex, SLCreateChartOptions Options)
        {
            if (Options == null) Options = new SLCreateChartOptions();

            var chart = new SLChart();

            int iStartRowIndex = 1, iEndRowIndex = 1, iStartColumnIndex = 1, iEndColumnIndex = 1;
            if (StartRowIndex < EndRowIndex)
            {
                iStartRowIndex = StartRowIndex;
                iEndRowIndex = EndRowIndex;
            }
            else
            {
                iStartRowIndex = EndRowIndex;
                iEndRowIndex = StartRowIndex;
            }

            if (StartColumnIndex < EndColumnIndex)
            {
                iStartColumnIndex = StartColumnIndex;
                iEndColumnIndex = EndColumnIndex;
            }
            else
            {
                iStartColumnIndex = EndColumnIndex;
                iEndColumnIndex = StartColumnIndex;
            }

            if (iStartRowIndex < 1) iStartRowIndex = 1;
            if (iStartColumnIndex < 1) iStartColumnIndex = 1;
            if (iEndRowIndex > SLConstants.RowLimit) iEndRowIndex = SLConstants.RowLimit;
            if (iEndColumnIndex > SLConstants.ColumnLimit) iEndColumnIndex = SLConstants.ColumnLimit;

            // this will keep the calculations within workable range
            if (iStartRowIndex >= SLConstants.RowLimit) iStartRowIndex = SLConstants.RowLimit - 1;
            if (iStartColumnIndex >= SLConstants.ColumnLimit) iStartColumnIndex = SLConstants.ColumnLimit - 1;

            chart.WorksheetName = WorksheetName;

            if (Options.RowsAsDataSeries == null)
                if (iEndColumnIndex - iStartColumnIndex >= iEndRowIndex - iStartRowIndex)
                    chart.RowsAsDataSeries = true;
                else
                    chart.RowsAsDataSeries = false;
            else
                chart.RowsAsDataSeries = Options.RowsAsDataSeries.Value;

            chart.ShowHiddenData = Options.ShowHiddenData;
            chart.ShowDataLabelsOverMaximum = Options.IsStylish ? false : true;

            int i;
            chart.listThemeColors = new List<Color>();
            for (i = 0; i < SimpleTheme.listThemeColors.Count; ++i)
                chart.listThemeColors.Add(SimpleTheme.listThemeColors[i]);

            chart.Date1904 = slwb.WorkbookProperties.Date1904;
            chart.IsStylish = Options.IsStylish;
            chart.RoundedCorners = false;

            // assume combination charts are possible first
            chart.IsCombinable = true;

            chart.PlotArea = new SLPlotArea(SimpleTheme.listThemeColors, slwb.WorkbookProperties.Date1904,
                Options.IsStylish);
            chart.PlotArea.DataSeries = FillChartDataSeries(WorksheetName, StartRowIndex, StartColumnIndex, EndRowIndex,
                EndColumnIndex, chart.RowsAsDataSeries, chart.ShowHiddenData);
            chart.SetPlotAreaAxes();

            chart.HasShownSecondaryTextAxis = false;

            chart.StartRowIndex = iStartRowIndex;
            chart.StartColumnIndex = iStartColumnIndex;
            chart.EndRowIndex = iEndRowIndex;
            chart.EndColumnIndex = iEndColumnIndex;

            chart.ShowEmptyCellsAs = Options.IsStylish ? C.DisplayBlanksAsValues.Zero : C.DisplayBlanksAsValues.Gap;
            chart.ChartStyle = SLChartStyle.Style2;

            chart.ChartName = string.Format("Chart {0}", slws.Charts.Count + 1);

            chart.HasTitle = false;
            chart.Title = new SLTitle(SimpleTheme.listThemeColors, Options.IsStylish);
            chart.Title.Overlay = false;

            chart.Is3D = false;
            chart.Floor = new SLFloor(SimpleTheme.listThemeColors, Options.IsStylish);
            chart.SideWall = new SLSideWall(SimpleTheme.listThemeColors, Options.IsStylish);
            chart.BackWall = new SLBackWall(SimpleTheme.listThemeColors, Options.IsStylish);

            chart.ShowLegend = true;
            chart.Legend = new SLLegend(SimpleTheme.listThemeColors, Options.IsStylish);
            chart.Legend.Overlay = false;
            if (Options.IsStylish)
                chart.Legend.LegendPosition = C.LegendPositionValues.Bottom;

            chart.ShapeProperties = new SLShapeProperties(SimpleTheme.listThemeColors);

            if (Options.IsStylish)
            {
                chart.ShapeProperties.Fill.SetSolidFill(A.SchemeColorValues.Background1, 0, 0);
                chart.ShapeProperties.Outline.Width = 0.75m;
                chart.ShapeProperties.Outline.CapType = A.LineCapValues.Flat;
                chart.ShapeProperties.Outline.CompoundLineType = A.CompoundLineValues.Single;
                chart.ShapeProperties.Outline.Alignment = A.PenAlignmentValues.Center;
                chart.ShapeProperties.Outline.SetSolidLine(A.SchemeColorValues.Text1, 0.85m, 0);
                chart.ShapeProperties.Outline.JoinType = SLLineJoinValues.Round;
            }

            chart.TopPosition = 1;
            chart.LeftPosition = 1;
            chart.BottomPosition = 16;
            chart.RightPosition = 8.5;

            return chart;
        }

        // this is here because it's only used by the FillChartDataSeries() function.
        private string GetCellTrueValue(SLCell c)
        {
            var sValue = c.CellText ?? string.Empty;
            if (c.DataType == CellValues.Number)
            {
                if (c.CellText == null)
                    sValue = c.NumericValue.ToString(CultureInfo.InvariantCulture);
            }
            else if (c.DataType == CellValues.SharedString)
            {
                if (c.CellText == null)
                {
                    var index = Convert.ToInt32(c.NumericValue);
                    SLRstType rst;
                    sValue = string.Empty;
                    if ((index >= 0) && (index < listSharedString.Count))
                    {
                        rst = new SLRstType(SLConstants.OfficeThemeMajorLatinFont, SLConstants.OfficeThemeMinorLatinFont,
                            new List<Color>(), new List<Color>());
                        rst.FromSharedStringItem(new SharedStringItem {InnerXml = listSharedString[index]});
                        sValue = rst.ToPlainString();
                    }
                }
            }
            else if (c.DataType == CellValues.Boolean)
            {
                if (c.CellText == null)
                    if (c.NumericValue > 0.5) sValue = "1";
                    else sValue = "0";
            }
            // no inline strings

            return sValue;
        }

        private List<SLDataSeries> FillChartDataSeries(string WorksheetName, int StartRowIndex, int StartColumnIndex,
            int EndRowIndex, int EndColumnIndex, bool RowsAsDataSeries, bool ShowHiddenData)
        {
            var series = new List<SLDataSeries>();
            int i, j;
            SLCell c;
            SLCellPoint pt;
            var HiddenRows = new Dictionary<int, bool>();
            var HiddenColumns = new Dictionary<int, bool>();
            var cellstore = new Dictionary<SLCellPoint, SLCell>();

            #region GetCells

            for (i = StartRowIndex; i <= EndRowIndex; ++i)
                HiddenRows[i] = false;
            for (j = StartColumnIndex; j <= EndColumnIndex; ++j)
                HiddenColumns[j] = false;

            var bFound = false;
            var sWorksheetRelID = string.Empty;
            if (WorksheetName.Equals(gsSelectedWorksheetName, StringComparison.OrdinalIgnoreCase))
            {
                bFound = false;
            }
            else
            {
                bFound = false;
                foreach (var sheet in slwb.Sheets)
                    if (sheet.Name.Equals(WorksheetName, StringComparison.OrdinalIgnoreCase))
                    {
                        bFound = true;
                        sWorksheetRelID = sheet.Id;
                        break;
                    }
            }

            if (bFound)
            {
                var cellref = new Dictionary<string, SLCellPoint>();
                for (i = StartRowIndex; i <= EndRowIndex; ++i)
                    for (j = StartColumnIndex; j <= EndColumnIndex; ++j)
                    {
                        pt = new SLCellPoint(i, j);
                        cellref[SLTool.ToCellReference(i, j)] = pt;
                    }

                var wsp = (WorksheetPart) wbp.GetPartById(sWorksheetRelID);
                Row row;
                Column column;
                Cell cell;
                if (!ShowHiddenData)
                    using (var oxr = OpenXmlReader.Create(wsp))
                    {
                        while (oxr.Read())
                            if (oxr.ElementType == typeof(Row))
                            {
                                row = (Row) oxr.LoadCurrentElement();
                                if (row.RowIndex != null)
                                    foreach (var rowindex in HiddenRows.Keys)
                                        if ((row.RowIndex.Value == rowindex) && (row.Hidden != null) && row.Hidden.Value)
                                            HiddenRows[rowindex] = true;
                            }
                    }
                using (var oxr = OpenXmlReader.Create(wsp))
                {
                    while (oxr.Read())
                        if (oxr.ElementType == typeof(Column))
                        {
                            if (!ShowHiddenData)
                            {
                                column = (Column) oxr.LoadCurrentElement();
                                foreach (var colindex in HiddenColumns.Keys)
                                    if ((column.Min <= colindex) && (colindex <= column.Max) && (column.Hidden != null) &&
                                        column.Hidden.Value)
                                        HiddenColumns[colindex] = true;
                            }
                        }
                        else if (oxr.ElementType == typeof(Cell))
                        {
                            cell = (Cell) oxr.LoadCurrentElement();
                            if (cell.CellReference != null)
                                if (cellref.ContainsKey(cell.CellReference.Value))
                                {
                                    c = new SLCell();
                                    c.FromCell(cell);
                                    cellstore[cellref[cell.CellReference.Value]] = c.Clone();
                                }
                        }
                }
            }
            else
            {
                SLRowProperties rp;
                SLColumnProperties cp;

                if (!ShowHiddenData)
                    for (j = StartColumnIndex; j <= EndColumnIndex; ++j)
                        if (slws.ColumnProperties.ContainsKey(j))
                        {
                            cp = slws.ColumnProperties[j];
                            if (cp.Hidden) HiddenColumns[j] = true;
                        }

                for (i = StartRowIndex; i <= EndRowIndex; ++i)
                {
                    if (!ShowHiddenData && slws.RowProperties.ContainsKey(i))
                    {
                        rp = slws.RowProperties[i];
                        if (rp.Hidden) HiddenRows[i] = true;
                    }

                    for (j = StartColumnIndex; j <= EndColumnIndex; ++j)
                    {
                        pt = new SLCellPoint(i, j);
                        if (slws.Cells.ContainsKey(pt))
                            cellstore[pt] = slws.Cells[pt].Clone();
                    }
                }
            }

            #endregion

            var index = 0;
            var index2 = 0;
            string sCellValue;
            string sFormatCode;
            SLDataSeries ser;
            SLStringReference sr;
            SLNumberReference nr;
            SLStyle style;

            var bIsStringReference = true;
            // we're going to assume that the format code applies to all in the "category" cells.
            var sAxisFormatCode = string.Empty;

            var cat = new SLAxisDataSourceType();
            if (RowsAsDataSeries)
            {
                bIsStringReference = true;
                sAxisFormatCode = SLConstants.NumberFormatGeneral;
                pt = new SLCellPoint(StartRowIndex, StartColumnIndex + 1);
                if (cellstore.ContainsKey(pt))
                    if (cellstore[pt].DataType == CellValues.Number)
                    {
                        bIsStringReference = false;
                        style = new SLStyle(SLConstants.OfficeThemeMajorLatinFont, SLConstants.OfficeThemeMinorLatinFont,
                            new List<Color>(), new List<Color>());
                        style.FromHash(listStyle[(int) cellstore[pt].StyleIndex]);
                        TranslateStylesToStyleIds(ref style);
                        sAxisFormatCode = style.FormatCode;
                    }

                sr = new SLStringReference();
                nr = new SLNumberReference();
                if (bIsStringReference)
                {
                    cat.UseStringReference = true;
                    sr = new SLStringReference();
                    sr.WorksheetName = WorksheetName;
                    sr.StartRowIndex = StartRowIndex;
                    sr.StartColumnIndex = StartColumnIndex + 1;
                    sr.EndRowIndex = StartRowIndex;
                    sr.EndColumnIndex = EndColumnIndex;
                    sr.Formula = SLChartTool.GetChartReferenceFormula(WorksheetName, StartRowIndex,
                        StartColumnIndex + 1, StartRowIndex, EndColumnIndex);
                }
                else
                {
                    cat.UseNumberReference = true;
                    nr.WorksheetName = WorksheetName;
                    nr.StartRowIndex = StartRowIndex;
                    nr.StartColumnIndex = StartColumnIndex + 1;
                    nr.EndRowIndex = StartRowIndex;
                    nr.EndColumnIndex = EndColumnIndex;
                    nr.Formula = SLChartTool.GetChartReferenceFormula(WorksheetName, StartRowIndex,
                        StartColumnIndex + 1, StartRowIndex, EndColumnIndex);
                    nr.NumberingCache.FormatCode = sAxisFormatCode;
                }

                index2 = 0;
                // if the header row is hidden, I don't know what to do...
                for (j = StartColumnIndex + 1; j <= EndColumnIndex; ++j)
                    if (HiddenColumns.ContainsKey(j) && !HiddenColumns[j])
                    {
                        pt = new SLCellPoint(StartRowIndex, j);
                        sCellValue = string.Empty;
                        if (cellstore.ContainsKey(pt))
                        {
                            c = cellstore[pt];
                            sCellValue = GetCellTrueValue(c);

                            if (bIsStringReference)
                                sr.Points.Add(new SLStringPoint
                                {
                                    Index = (uint) index2,
                                    NumericValue = sCellValue
                                });
                            else
                                nr.NumberingCache.Points.Add(new SLNumericPoint
                                {
                                    Index = (uint) index2,
                                    NumericValue = sCellValue
                                });
                        }
                        ++index2;
                    }

                if (bIsStringReference)
                {
                    sr.PointCount = (uint) index2;
                    cat.StringReference = sr;
                }
                else
                {
                    nr.NumberingCache.PointCount = (uint) index2;
                    cat.NumberReference = nr;
                }

                index = 0;
                for (i = StartRowIndex + 1; i <= EndRowIndex; ++i)
                    if (HiddenRows.ContainsKey(i) && !HiddenRows[i])
                    {
                        ser = new SLDataSeries(SimpleTheme.listThemeColors);
                        ser.Index = (uint) index;
                        ser.Order = (uint) index;
                        ser.IsStringReference = true;

                        sr = new SLStringReference();
                        pt = new SLCellPoint(i, StartColumnIndex);
                        sCellValue = string.Empty;
                        if (cellstore.ContainsKey(pt))
                        {
                            c = cellstore[pt];
                            sCellValue = GetCellTrueValue(c);
                        }
                        sr.WorksheetName = WorksheetName;
                        sr.StartRowIndex = i;
                        sr.StartColumnIndex = StartColumnIndex;
                        sr.EndRowIndex = i;
                        sr.EndColumnIndex = StartColumnIndex;
                        sr.Formula = SLChartTool.GetChartReferenceFormula(WorksheetName, i, StartColumnIndex);
                        sr.PointCount = 1;
                        sr.Points.Add(new SLStringPoint {Index = 0, NumericValue = sCellValue});
                        ser.StringReference = sr;

                        ser.AxisData = cat.Clone();

                        ser.NumberData.UseNumberReference = true;

                        nr = new SLNumberReference();
                        nr.WorksheetName = WorksheetName;
                        nr.StartRowIndex = i;
                        nr.StartColumnIndex = StartColumnIndex + 1;
                        nr.EndRowIndex = i;
                        nr.EndColumnIndex = EndColumnIndex;
                        nr.Formula = SLChartTool.GetChartReferenceFormula(WorksheetName, i, StartColumnIndex + 1, i,
                            EndColumnIndex);
                        nr.NumberingCache.FormatCode = SLConstants.NumberFormatGeneral;
                        index2 = 0;
                        for (j = StartColumnIndex + 1; j <= EndColumnIndex; ++j)
                            if (HiddenColumns.ContainsKey(j) && !HiddenColumns[j])
                            {
                                pt = new SLCellPoint(i, j);
                                sCellValue = string.Empty;
                                sFormatCode = string.Empty;
                                if (cellstore.ContainsKey(pt))
                                {
                                    c = cellstore[pt];
                                    sCellValue = GetCellTrueValue(c);

                                    style = new SLStyle(SLConstants.OfficeThemeMajorLatinFont,
                                        SLConstants.OfficeThemeMinorLatinFont, new List<Color>(), new List<Color>());
                                    style.FromHash(listStyle[(int) c.StyleIndex]);
                                    TranslateStylesToStyleIds(ref style);
                                    if (style.HasNumberingFormat) sFormatCode = style.FormatCode;

                                    nr.NumberingCache.Points.Add(new SLNumericPoint
                                    {
                                        FormatCode = sFormatCode,
                                        Index = (uint) index2,
                                        NumericValue = sCellValue
                                    });
                                }
                                ++index2;
                            }
                        nr.NumberingCache.PointCount = (uint) index2;
                        ser.NumberData.NumberReference = nr;

                        series.Add(ser);
                        ++index;
                    }

                // end of rows as data series
            }
            else
            {
                bIsStringReference = true;
                sAxisFormatCode = SLConstants.NumberFormatGeneral;
                pt = new SLCellPoint(StartRowIndex + 1, StartColumnIndex);
                if (cellstore.ContainsKey(pt))
                    if (cellstore[pt].DataType == CellValues.Number)
                    {
                        bIsStringReference = false;
                        style = new SLStyle(SLConstants.OfficeThemeMajorLatinFont, SLConstants.OfficeThemeMinorLatinFont,
                            new List<Color>(), new List<Color>());
                        style.FromHash(listStyle[(int) cellstore[pt].StyleIndex]);
                        TranslateStylesToStyleIds(ref style);
                        sAxisFormatCode = style.FormatCode;
                    }

                sr = new SLStringReference();
                nr = new SLNumberReference();
                if (bIsStringReference)
                {
                    cat.UseStringReference = true;
                    sr.WorksheetName = WorksheetName;
                    sr.StartRowIndex = StartRowIndex + 1;
                    sr.StartColumnIndex = StartColumnIndex;
                    sr.EndRowIndex = EndRowIndex;
                    sr.EndColumnIndex = StartColumnIndex;
                    sr.Formula = SLChartTool.GetChartReferenceFormula(WorksheetName, StartRowIndex + 1,
                        StartColumnIndex, EndRowIndex, StartColumnIndex);
                }
                else
                {
                    cat.UseNumberReference = true;
                    nr.WorksheetName = WorksheetName;
                    nr.StartRowIndex = StartRowIndex + 1;
                    nr.StartColumnIndex = StartColumnIndex;
                    nr.EndRowIndex = EndRowIndex;
                    nr.EndColumnIndex = StartColumnIndex;
                    nr.Formula = SLChartTool.GetChartReferenceFormula(WorksheetName, StartRowIndex + 1,
                        StartColumnIndex, EndRowIndex, StartColumnIndex);
                    nr.NumberingCache.FormatCode = sAxisFormatCode;
                }

                index2 = 0;
                // if the header column is hidden, I don't know what to do...
                for (i = StartRowIndex + 1; i <= EndRowIndex; ++i)
                    if (HiddenRows.ContainsKey(i) && !HiddenRows[i])
                    {
                        pt = new SLCellPoint(i, StartColumnIndex);
                        sCellValue = string.Empty;
                        if (cellstore.ContainsKey(pt))
                        {
                            c = cellstore[pt];
                            sCellValue = GetCellTrueValue(c);

                            if (bIsStringReference)
                                sr.Points.Add(new SLStringPoint
                                {
                                    Index = (uint) index2,
                                    NumericValue = sCellValue
                                });
                            else
                                nr.NumberingCache.Points.Add(new SLNumericPoint
                                {
                                    Index = (uint) index2,
                                    NumericValue = sCellValue
                                });
                        }
                        ++index2;
                    }

                if (bIsStringReference)
                {
                    sr.PointCount = (uint) index2;
                    cat.StringReference = sr;
                }
                else
                {
                    nr.NumberingCache.PointCount = (uint) index2;
                    cat.NumberReference = nr;
                }

                index = 0;
                for (j = StartColumnIndex + 1; j <= EndColumnIndex; ++j)
                    if (HiddenColumns.ContainsKey(j) && !HiddenColumns[j])
                    {
                        ser = new SLDataSeries(SimpleTheme.listThemeColors);
                        ser.Index = (uint) index;
                        ser.Order = (uint) index;
                        ser.IsStringReference = true;

                        sr = new SLStringReference();
                        pt = new SLCellPoint(StartRowIndex, j);
                        sCellValue = string.Empty;
                        if (cellstore.ContainsKey(pt))
                        {
                            c = cellstore[pt];
                            sCellValue = GetCellTrueValue(c);
                        }
                        sr.WorksheetName = WorksheetName;
                        sr.StartRowIndex = StartRowIndex;
                        sr.StartColumnIndex = j;
                        sr.EndRowIndex = StartRowIndex;
                        sr.EndColumnIndex = j;
                        sr.Formula = SLChartTool.GetChartReferenceFormula(WorksheetName, StartRowIndex, j);
                        sr.PointCount = 1;
                        sr.Points.Add(new SLStringPoint {Index = 0, NumericValue = sCellValue});
                        ser.StringReference = sr;

                        ser.AxisData = cat.Clone();

                        ser.NumberData.UseNumberReference = true;

                        nr = new SLNumberReference();
                        nr.WorksheetName = WorksheetName;
                        nr.StartRowIndex = StartRowIndex + 1;
                        nr.StartColumnIndex = j;
                        nr.EndRowIndex = EndRowIndex;
                        nr.EndColumnIndex = j;
                        nr.Formula = SLChartTool.GetChartReferenceFormula(WorksheetName, StartRowIndex + 1, j,
                            EndRowIndex, j);
                        nr.NumberingCache.FormatCode = SLConstants.NumberFormatGeneral;
                        index2 = 0;
                        for (i = StartRowIndex + 1; i <= EndRowIndex; ++i)
                            if (HiddenRows.ContainsKey(i) && !HiddenRows[i])
                            {
                                pt = new SLCellPoint(i, j);
                                sCellValue = string.Empty;
                                sFormatCode = string.Empty;
                                if (cellstore.ContainsKey(pt))
                                {
                                    c = cellstore[pt];
                                    sCellValue = GetCellTrueValue(c);

                                    style = new SLStyle(SLConstants.OfficeThemeMajorLatinFont,
                                        SLConstants.OfficeThemeMinorLatinFont, new List<Color>(), new List<Color>());
                                    style.FromHash(listStyle[(int) c.StyleIndex]);
                                    TranslateStylesToStyleIds(ref style);
                                    if (style.HasNumberingFormat) sFormatCode = style.FormatCode;

                                    nr.NumberingCache.Points.Add(new SLNumericPoint
                                    {
                                        FormatCode = sFormatCode,
                                        Index = (uint) index2,
                                        NumericValue = sCellValue
                                    });
                                }
                                ++index2;
                            }
                        nr.NumberingCache.PointCount = (uint) index2;
                        ser.NumberData.NumberReference = nr;

                        series.Add(ser);
                        ++index;
                    }

                // end of columns as data series
            }

            return series;
        }
    }
}