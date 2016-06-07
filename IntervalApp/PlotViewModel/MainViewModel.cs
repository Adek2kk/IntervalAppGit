using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace IntervalApp.PlotViewModel
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            // Create the plot model
            var tmp = new PlotModel { Title = "Simple example", Subtitle = "using OxyPlot" };
        }

        public PlotModel Model { get; set; }

        public void Plot_2_Column(List<StatHolder> PlotData, string XAxisTitle = "", string YAxisTitle = "", double LabelAngle = 0)
        {
            var plotModel = new PlotModel();
            plotModel.Title = "Query time";

            var categoryAxis = new CategoryAxis();
            categoryAxis.MinorStep = 1;

            int i = 0;
            foreach (StatHolder row in PlotData)
            {
                i++;
                categoryAxis.Labels.Add("Query_" + i);
            }
            categoryAxis.Angle = LabelAngle;
            categoryAxis.Title = XAxisTitle;
            plotModel.Axes.Add(categoryAxis);

            var linearAxis1 = new LinearAxis();
            linearAxis1.AbsoluteMinimum = 0;
            linearAxis1.Minimum = 0;
            linearAxis1.MaximumPadding = 0.06;
            linearAxis1.MinimumPadding = 0;
            linearAxis1.Title = YAxisTitle;
            plotModel.Axes.Add(linearAxis1);

            //AddingSeries
            var columnSeries = new ColumnSeries();
            columnSeries.StrokeThickness = 1;

            foreach (StatHolder row in PlotData)
            {
                    columnSeries.Items.Add(new ColumnItem(row.time, -1));
            }

            if (LabelAngle < 40)
            {
                columnSeries.LabelFormatString = "{0}";
            }
            plotModel.Series.Add(columnSeries);

            this.Model = plotModel;
        }

    }
}
