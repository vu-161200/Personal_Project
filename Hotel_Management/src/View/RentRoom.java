
package View;

import Model.Customer;
import Model.CustomerList;
import Model.Room;
import Model.RoomList;
import Property.FormatJTable;
import java.awt.Color;
import java.text.NumberFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.util.concurrent.TimeUnit;
import javax.swing.JOptionPane;
import javax.swing.table.DefaultTableModel;

/**
 *
 * @author Đ.Tee, Vũ, Diệu
 */

public class RentRoom extends javax.swing.JFrame {

    SimpleDateFormat sdf = new SimpleDateFormat("MM/dd/yyyy");
    NumberFormat formatter = NumberFormat.getInstance();
    
    RoomList rl = new RoomList();
    List<Room> roomList;
    FormatJTable ft = new FormatJTable();
    
    double totalFee = 0;
    HotelInfo hi;
    
    public RentRoom(List<Room> roomList, HotelInfo hi) {
        initComponents();
        setLocationRelativeTo(null);
        setResizable(false);
        
        this.roomList = roomList;
        this.hi = hi;
        
        initTable();
        
        jScrollPane1.getViewport().setBackground(new Color(204,204,255));
        jtbl.getTableHeader().setBackground(new Color(238, 232, 170));
        jtbl.setSelectionBackground(new Color(135, 206, 250));
        
        this.addWindowListener(new java.awt.event.WindowAdapter() {
            @Override
            public void windowClosing(java.awt.event.WindowEvent windowEvent) {
                if(hi.listRent != null)
                {
                    hi.reLoadListRent();
                    return;
                }
                else
                    hi.reload();
            }
        });
        
    }

    //Khởi tạo talbe
    private void initTable()
    {
        totalFee = 0;
        
        DefaultTableModel dtm = new DefaultTableModel() {
            public boolean isCellEditable(int row, int col) {
                return false;
            }
        };
        
        String[] col = {"Room ID", "Area", "Floor", "Type Room", "Fee"};
        
        dtm.setColumnIdentifiers(col);
        jtbl.setModel(dtm);
        
        jtbl.getColumnModel().getColumn(0).setPreferredWidth(30);
        jtbl.getColumnModel().getColumn(1).setPreferredWidth(30);
        jtbl.getColumnModel().getColumn(2).setPreferredWidth(30);
        jtbl.getColumnModel().getColumn(3).setPreferredWidth(70);
        jtbl.getColumnModel().getColumn(4).setPreferredWidth(100);
        
        dtm.setRowCount(0);
        
        for(Room r : roomList)
        {
            totalFee += r.getFee();
            dtm.addRow(new Object[]{r.getRoomID(), r.getArea(), r.getFloor(), r.getType().equals("DELUXE_ROOM") ? "Deluxe Room" : "Standard Room",
                       formatter.format(r.getFee()) + " VNĐ"});
        }
        
        ft.setCellsAlignment(jtbl);
        calFee();
    }
    
    //check null
    private boolean isNull()
    {
        if(txtFullName.getText().trim().equals("")) return true;
        else if(txtCardID.getText().trim().equals("")) return true;
        else if(txtPhoneNumber.getText().trim().equals("")) return true;
        return false;
    }
    
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jPanel2 = new javax.swing.JPanel();
        jLabel1 = new javax.swing.JLabel();
        jPanel1 = new javax.swing.JPanel();
        jLabel2 = new javax.swing.JLabel();
        txtFullName = new javax.swing.JTextField();
        jLabel3 = new javax.swing.JLabel();
        txtCardID = new javax.swing.JTextField();
        jLabel4 = new javax.swing.JLabel();
        txtPhoneNumber = new javax.swing.JTextField();
        jLabel6 = new javax.swing.JLabel();
        txtTotalFee = new javax.swing.JTextField();
        jbtnSAVE = new javax.swing.JButton();
        jbtnREMOVE = new javax.swing.JButton();
        jdcStart = new com.toedter.calendar.JDateChooser();
        jdcStop = new com.toedter.calendar.JDateChooser();
        jLabel5 = new javax.swing.JLabel();
        jLabel7 = new javax.swing.JLabel();
        jScrollPane1 = new javax.swing.JScrollPane();
        jtbl = new javax.swing.JTable();

        setDefaultCloseOperation(javax.swing.WindowConstants.DISPOSE_ON_CLOSE);

        jPanel2.setBackground(new java.awt.Color(204, 204, 255));

        jLabel1.setFont(new java.awt.Font("Source Sans Pro", 1, 18)); // NOI18N
        jLabel1.setForeground(new java.awt.Color(255, 0, 0));
        jLabel1.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        jLabel1.setText("RENT");

        jPanel1.setBackground(new java.awt.Color(204, 204, 255));
        jPanel1.setBorder(javax.swing.BorderFactory.createTitledBorder(javax.swing.BorderFactory.createLineBorder(new java.awt.Color(0, 0, 0)), "CUSTOMER INFORMATION", javax.swing.border.TitledBorder.DEFAULT_JUSTIFICATION, javax.swing.border.TitledBorder.TOP, new java.awt.Font("Source Sans Pro", 1, 13), new java.awt.Color(0, 0, 255))); // NOI18N

        jLabel2.setText("Full Name");

        txtFullName.setBackground(new java.awt.Color(204, 204, 255));
        txtFullName.setBorder(javax.swing.BorderFactory.createMatteBorder(0, 0, 1, 0, new java.awt.Color(51, 51, 51)));

        jLabel3.setText("Identify Card ID");

        txtCardID.setBackground(new java.awt.Color(204, 204, 255));
        txtCardID.setBorder(javax.swing.BorderFactory.createMatteBorder(0, 0, 1, 0, new java.awt.Color(51, 51, 51)));

        jLabel4.setText("Phone Number");

        txtPhoneNumber.setBackground(new java.awt.Color(204, 204, 255));
        txtPhoneNumber.setBorder(javax.swing.BorderFactory.createMatteBorder(0, 0, 1, 0, new java.awt.Color(51, 51, 51)));

        jLabel6.setText("Total Fee");

        txtTotalFee.setEditable(false);
        txtTotalFee.setBackground(new java.awt.Color(204, 204, 255));
        txtTotalFee.setFont(new java.awt.Font("Source Sans Pro", 1, 14)); // NOI18N
        txtTotalFee.setForeground(new java.awt.Color(255, 0, 0));
        txtTotalFee.setHorizontalAlignment(javax.swing.JTextField.CENTER);
        txtTotalFee.setBorder(javax.swing.BorderFactory.createMatteBorder(0, 0, 1, 0, new java.awt.Color(51, 51, 51)));

        jbtnSAVE.setBackground(new java.awt.Color(220, 220, 220));
        jbtnSAVE.setText("SAVE");
        jbtnSAVE.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jbtnSAVEActionPerformed(evt);
            }
        });

        jbtnREMOVE.setBackground(new java.awt.Color(220, 220, 220));
        jbtnREMOVE.setText("REMOVE");
        jbtnREMOVE.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jbtnREMOVEActionPerformed(evt);
            }
        });

        jdcStart.setBackground(new java.awt.Color(204, 204, 255));
        jdcStart.setBorder(javax.swing.BorderFactory.createEmptyBorder(0, 0, 0, 0));
        jdcStart.addPropertyChangeListener(new java.beans.PropertyChangeListener() {
            public void propertyChange(java.beans.PropertyChangeEvent evt) {
                jdcStartPropertyChange(evt);
            }
        });

        jdcStop.setBackground(new java.awt.Color(204, 204, 255));
        jdcStop.setBorder(javax.swing.BorderFactory.createEmptyBorder(0, 0, 0, 0));
        jdcStop.addPropertyChangeListener(new java.beans.PropertyChangeListener() {
            public void propertyChange(java.beans.PropertyChangeEvent evt) {
                jdcStopPropertyChange(evt);
            }
        });

        jLabel5.setText("Start Renting");

        jLabel7.setText("Stop Renting");

        javax.swing.GroupLayout jPanel1Layout = new javax.swing.GroupLayout(jPanel1);
        jPanel1.setLayout(jPanel1Layout);
        jPanel1Layout.setHorizontalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addGap(17, 17, 17)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addComponent(jbtnSAVE, javax.swing.GroupLayout.PREFERRED_SIZE, 105, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addComponent(jbtnREMOVE, javax.swing.GroupLayout.PREFERRED_SIZE, 105, javax.swing.GroupLayout.PREFERRED_SIZE))
                    .addGroup(jPanel1Layout.createSequentialGroup()
                        .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                            .addComponent(jLabel6, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(jLabel2, javax.swing.GroupLayout.PREFERRED_SIZE, 82, javax.swing.GroupLayout.PREFERRED_SIZE)
                            .addComponent(jLabel3, javax.swing.GroupLayout.DEFAULT_SIZE, 90, Short.MAX_VALUE)
                            .addComponent(jLabel4, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(jLabel5, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(jLabel7, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                        .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                            .addGroup(jPanel1Layout.createSequentialGroup()
                                .addGap(19, 19, 19)
                                .addComponent(txtFullName, javax.swing.GroupLayout.PREFERRED_SIZE, 171, javax.swing.GroupLayout.PREFERRED_SIZE))
                            .addGroup(jPanel1Layout.createSequentialGroup()
                                .addGap(18, 18, 18)
                                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                                    .addComponent(jdcStart, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                                    .addComponent(txtCardID)
                                    .addComponent(txtPhoneNumber)
                                    .addComponent(txtTotalFee)
                                    .addComponent(jdcStop, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))))))
                .addContainerGap(17, Short.MAX_VALUE))
        );
        jPanel1Layout.setVerticalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addGap(23, 23, 23)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(jLabel2, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(txtFullName, javax.swing.GroupLayout.DEFAULT_SIZE, 35, Short.MAX_VALUE))
                .addGap(18, 18, 18)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(jLabel3, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(txtCardID, javax.swing.GroupLayout.DEFAULT_SIZE, 35, Short.MAX_VALUE))
                .addGap(18, 18, 18)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel4, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(txtPhoneNumber, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(18, 18, 18)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(jLabel5, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(jdcStart, javax.swing.GroupLayout.DEFAULT_SIZE, 36, Short.MAX_VALUE))
                .addGap(18, 18, 18)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(jdcStop, javax.swing.GroupLayout.DEFAULT_SIZE, 36, Short.MAX_VALUE)
                    .addComponent(jLabel7, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addGap(18, 18, 18)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel6, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(txtTotalFee, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(36, 36, 36)
                .addGroup(jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jbtnSAVE, javax.swing.GroupLayout.PREFERRED_SIZE, 46, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jbtnREMOVE, javax.swing.GroupLayout.PREFERRED_SIZE, 46, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addContainerGap())
        );

        jtbl.setBackground(java.awt.SystemColor.controlHighlight);
        jtbl.setModel(new javax.swing.table.DefaultTableModel(
            new Object [][] {
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null},
                {null, null, null, null}
            },
            new String [] {
                "Title 1", "Title 2", "Title 3", "Title 4"
            }
        ));
        jScrollPane1.setViewportView(jtbl);

        javax.swing.GroupLayout jPanel2Layout = new javax.swing.GroupLayout(jPanel2);
        jPanel2.setLayout(jPanel2Layout);
        jPanel2Layout.setHorizontalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jLabel1, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel2Layout.createSequentialGroup()
                        .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                        .addComponent(jScrollPane1, javax.swing.GroupLayout.DEFAULT_SIZE, 458, Short.MAX_VALUE)))
                .addContainerGap())
        );
        jPanel2Layout.setVerticalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addGap(3, 3, 3)
                .addComponent(jLabel1, javax.swing.GroupLayout.PREFERRED_SIZE, 36, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jPanel1, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addGroup(jPanel2Layout.createSequentialGroup()
                        .addGap(0, 8, Short.MAX_VALUE)
                        .addComponent(jScrollPane1, javax.swing.GroupLayout.PREFERRED_SIZE, 441, javax.swing.GroupLayout.PREFERRED_SIZE)))
                .addContainerGap())
        );

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(jPanel2, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(jPanel2, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents

    //Xóa khỏi DS thuê
    private void jbtnREMOVEActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jbtnREMOVEActionPerformed
        DefaultTableModel dtm = (DefaultTableModel) jtbl.getModel();       
        int index = jtbl.convertRowIndexToModel(jtbl.getSelectedRow());
        
        if(index < 0)
        {
            JOptionPane.showMessageDialog(this, "Select room to remove!", "", JOptionPane.INFORMATION_MESSAGE);
            return;
        }
        
        int choose = JOptionPane.showConfirmDialog(this, "Are you sure to delete roomID '" + jtbl.getModel().getValueAt(index, 0) + "' ?", "", JOptionPane.OK_CANCEL_OPTION);
        if(choose != JOptionPane.YES_OPTION) return;
        
        int pos = rl.getListRoom().indexOf(roomList.get(index));
        Room r = rl.getListRoom().get(pos);
        r.setAvailable(true);
        totalFee -= r.getFee();
        
        roomList.remove(index);
        
        JOptionPane.showMessageDialog(this, "Deleted", "", JOptionPane.INFORMATION_MESSAGE);
        
        initTable();
        calFee();
    }//GEN-LAST:event_jbtnREMOVEActionPerformed

    //Xác nhận đặt phòng
    private void jbtnSAVEActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jbtnSAVEActionPerformed
        if(isNull())
        {
            JOptionPane.showMessageDialog(this, "Please fill all information", "", JOptionPane.INFORMATION_MESSAGE);
            return;
        }
        
        String fullName = txtFullName.getText().trim();
        String ID = txtCardID.getText().trim();
        String phoneNum = txtPhoneNumber.getText().trim();
        
        Date startRenting = new Date(sdf.format(jdcStart.getDate()));
        Date stopRenting = new Date(sdf.format(jdcStop.getDate()));
          
        if(startRenting.compareTo(stopRenting) > 0)
        {
            JOptionPane.showMessageDialog(this, "The rental start date must be less than or equal the rental end date", "", JOptionPane.INFORMATION_MESSAGE);
            return;
        }
        
        if(roomList.isEmpty())
        {
            JOptionPane.showMessageDialog(this, "The list of rooms to rent is empty", "", JOptionPane.INFORMATION_MESSAGE);
            return;
        }
        
        CustomerList cl = new CustomerList();
        cl.add(new Customer(fullName, ID, phoneNum, roomList, startRenting, stopRenting));
        
        hi.setListNull();
        hi.reload();
        
        this.dispose();
    }//GEN-LAST:event_jbtnSAVEActionPerformed

    //Tính tổng chi phí của khách hàng
    private void calFee()
    {
        Date startRenting = new Date(sdf.format(jdcStart.getDate()));
        Date stopRenting = new Date(sdf.format(jdcStop.getDate()));
        
        int days = getDays(startRenting, stopRenting);
        
        double fee;
        
        if(days < 0)
        {
            txtTotalFee.setText("Start <= Stop");
            return;
        }
        else if(days == 0) fee = totalFee;
        else fee = totalFee * (days + 1);
        
        txtTotalFee.setText(formatter.format(fee) + " VNĐ");
    }
    
    //Tính số ngày giữa 2 date
    private int getDays(Date start, Date stop)
    {
        long diff = stop.getTime() - start.getTime();
        TimeUnit time = TimeUnit.DAYS; 
        int diffrence = (int) time.convert(diff, TimeUnit.MILLISECONDS);
        
        return diffrence;
    }
    
    private void jdcStopPropertyChange(java.beans.PropertyChangeEvent evt) {//GEN-FIRST:event_jdcStopPropertyChange
        calFee();
    }//GEN-LAST:event_jdcStopPropertyChange

    private void jdcStartPropertyChange(java.beans.PropertyChangeEvent evt) {//GEN-FIRST:event_jdcStartPropertyChange
        calFee();
    }//GEN-LAST:event_jdcStartPropertyChange

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JLabel jLabel5;
    private javax.swing.JLabel jLabel6;
    private javax.swing.JLabel jLabel7;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JPanel jPanel2;
    private javax.swing.JScrollPane jScrollPane1;
    private javax.swing.JButton jbtnREMOVE;
    private javax.swing.JButton jbtnSAVE;
    private com.toedter.calendar.JDateChooser jdcStart;
    private com.toedter.calendar.JDateChooser jdcStop;
    private javax.swing.JTable jtbl;
    private javax.swing.JTextField txtCardID;
    private javax.swing.JTextField txtFullName;
    private javax.swing.JTextField txtPhoneNumber;
    private javax.swing.JTextField txtTotalFee;
    // End of variables declaration//GEN-END:variables

}
