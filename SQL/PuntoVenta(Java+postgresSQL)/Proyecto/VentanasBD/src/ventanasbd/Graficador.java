/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package ventanasbd;

/**
 *
 * @author estebannoguerapenaranda
 */
import javax.swing.JFrame;

import org.jfree.chart.ChartFactory;
import org.jfree.chart.ChartPanel;
import org.jfree.chart.JFreeChart;
import org.jfree.chart.plot.PlotOrientation;
import org.jfree.data.xy.XYDataset;
import org.jfree.data.xy.XYSeries;
import org.jfree.data.xy.XYSeriesCollection;


/**
 *
 * @author estebannoguerapenaranda
 */
public class Graficador extends JFrame{
    
 
   private static final long serialVersionUID = 1L;

   public Graficador (String applicationTitle, String chartTitle) {
        super(applicationTitle);

        // based on the dataset we create the chart
        JFreeChart pieChart = ChartFactory.createXYLineChart(chartTitle, "Dias del mes", "Productos", createDataset(),PlotOrientation.VERTICAL, true, true, false);

        // Adding chart into a chart panel
        ChartPanel chartPanel = new ChartPanel(pieChart);
      
        // settind default size
        chartPanel.setPreferredSize(new java.awt.Dimension(650, 500));
      
        // add to contentPane
        setContentPane( chartPanel );
    }

    Graficador() {
        throw new UnsupportedOperationException("Not supported yet."); //To change body of generated methods, choose Tools | Templates.
    }
  
   private XYDataset createDataset() {
     
      final XYSeries produtos = new XYSeries("Productos");
      
      //produtos.add(1, 10);
      //produtos.add(10, 20);
      //produtos.add(20, 30);
      
//      final XYSeries chrome = new XYSeries("Chrome");
//      chrome.add(1.0, 4.0);
//      chrome.add(2.0, 6.0);
//      chrome.add(3.0, 5.0);
//    
//     
//      final XYSeries iexplorer = new XYSeries("InternetExplorer");
//      iexplorer.add(3.0, 4.0);
//      iexplorer.add(4.0, 5.0);
//      iexplorer.add(5.0, 4.0);
     
     
      final XYSeriesCollection dataset = new XYSeriesCollection();
      //dataset.addSeries(produtos);
      //dataset.addSeries(chrome);
      //dataset.addSeries(iexplorer);
     
      return dataset;
     
  }

//   public static void main(String[] args) {
//      Graficador chart = new Graficador("Ventas Mensuales", "Ventas Mesuales");
//      chart.pack();
//      chart.setVisible(true);
//   }
//    
}
