
package Property;

import javax.swing.JLabel;
import javax.swing.JTable;
import javax.swing.SwingConstants;
import javax.swing.table.DefaultTableCellRenderer;
import javax.swing.table.TableCellRenderer;
import javax.swing.table.TableModel;

/**
 *
 * @author Đ.Tee
 */

public class FormatJTable {
    //Căn giữa table
    public void setCellsAlignment(JTable table)
    {
        //Căn giữa tiêu đề (header) của bảng
        TableCellRenderer rendererFromHeader = table.getTableHeader().getDefaultRenderer();
        JLabel headerLabel = (JLabel) rendererFromHeader;
        headerLabel.setHorizontalAlignment(JLabel.CENTER);
        
        
        //Căn giữa dữ liệu (các dòng) của bảng
        DefaultTableCellRenderer rightRenderer = new DefaultTableCellRenderer();
        rightRenderer.setHorizontalAlignment(SwingConstants.CENTER);

        TableModel tableModel = table.getModel();
        
        for (int columnIndex = 0; columnIndex < tableModel.getColumnCount(); columnIndex++) {
            table.getColumnModel().getColumn(columnIndex).setCellRenderer(rightRenderer);
        }
        
        //Điều chỉnh độ cao của các hàng
        table.setRowHeight(20);
    }
}
