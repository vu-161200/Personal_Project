
package View;

import Model.Customer;
import Model.CustomerList;
import Model.DeluxeRoom;
import Model.Room;
import Model.RoomList;
import Model.StandardRoom;
import Property.FormatJTable;
import java.awt.Color;
import java.text.NumberFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.List;
import javax.swing.JOptionPane;
import javax.swing.RowFilter;
import javax.swing.table.DefaultTableModel;
import javax.swing.table.TableRowSorter;

/**
 *
 * @author Đ.Tee, Vũ, Diệu
 */

public class HotelInfo extends javax.swing.JPanel {
    NumberFormat formatter = NumberFormat.getInstance();
    
    RoomList rl = new RoomList();
    public List<Room> listRent = new ArrayList<>();
    FormatJTable ft = new FormatJTable();
    
    public HotelInfo() {
        initComponents();
        
        loadDataTable(false);
        
        jScrollPane1.getViewport().setBackground(new Color(204,204,255));
        jtbl.getTableHeader().setBackground(new Color(238, 232, 170));
        jtbl.setSelectionBackground(new Color(135, 206, 250));
        
    }
    
    //Thêm dữ liệu vào bảng
    public void loadDataTable(boolean rented)
    {   
        DefaultTableModel dtm = new DefaultTableModel() {
            public boolean isCellEditable(int row, int col) {
                return false;
            }
        };
        
        if(rented) // Các phòng đang được thuê
        {
            SimpleDateFormat sdf = new SimpleDateFormat("dd/MM/yyyy");
            CustomerList cl = new CustomerList();
            List<Customer> customerList = cl.getList();
            
            String[] col = {"Room ID", "Area (m2)", "Floor", "Type Room", "Renter", "Start Renting", "Stop Renting", "Total Fee"};

            dtm.setColumnIdentifiers(col);

            jtbl.setModel(dtm);

            jtbl.getColumnModel().getColumn(7).setPreferredWidth(120);
            jtbl.getColumnModel().getColumn(3).setPreferredWidth(100);
            jtbl.getColumnModel().getColumn(4).setPreferredWidth(130);

            dtm.setRowCount(0);
        
            for(Customer c : customerList)
            {
                List<Room> roomList = c.getRoomList();
                for (Room r : roomList) {
                    double fee = (c.getNumOfDays() + 1) * r.getFee();
                    dtm.addRow(new Object[]{r.getRoomID(), r.getArea(), r.getFloor(), r.getType().equals("DELUXE_ROOM") ? "Deluxe Room" : "Standard Room",
                        c.getFullName(), sdf.format(c.getStartRenting()), sdf.format(c.getStopRenting()), formatter.format(fee) + " VNĐ"});

                }
            }
        }
        else // Tất cả các phòng
        {
            List<Room> roomList = rl.getListRoom();
            
            String[] col = {"Room ID", "Area (m2)", "Floor", "Type Room", "Fee", "Available"};

            dtm.setColumnIdentifiers(col);

            jtbl.setModel(dtm);

            jtbl.getColumnModel().getColumn(4).setPreferredWidth(150);

            dtm.setRowCount(0);
            
            for(Room room : roomList)
            {
                dtm.addRow(new Object[]{room.getRoomID(), room.getArea(), room.getFloor(), room.getType().equals("DELUXE_ROOM") ? "Deluxe Room" : "Standard Room",
                                        formatter.format(room.getFee()) + " VNĐ", room.isAvailable() ? "Available" : "Not Available"});
            }
        }
        
        ft.setCellsAlignment(jtbl);
    }

    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jPanel2 = new javax.swing.JPanel();
        jPanel3 = new javax.swing.JPanel();
        jLabel2 = new javax.swing.JLabel();
        txtIDRoom = new javax.swing.JTextField();
        jLabel3 = new javax.swing.JLabel();
        txtArea = new javax.swing.JTextField();
        jLabel4 = new javax.swing.JLabel();
        txtFloor = new javax.swing.JTextField();
        jLabel5 = new javax.swing.JLabel();
        cbbType = new javax.swing.JComboBox<>();
        jLabel7 = new javax.swing.JLabel();
        jlbFee = new javax.swing.JLabel();
        txtFee = new javax.swing.JTextField();
        jPanel4 = new javax.swing.JPanel();
        jbnUPDATE = new javax.swing.JButton();
        jbtnCLEAR = new javax.swing.JButton();
        jbtnADD = new javax.swing.JButton();
        jbtnDELETE = new javax.swing.JButton();
        jScrollPane1 = new javax.swing.JScrollPane();
        jtbl = new javax.swing.JTable();
        jPanel8 = new javax.swing.JPanel();
        jbtnAddList = new javax.swing.JButton();
        jbtnConfirm = new javax.swing.JButton();
        jButton1 = new javax.swing.JButton();
        jLabel1 = new javax.swing.JLabel();
        txtSearch = new javax.swing.JTextField();
        jcb = new javax.swing.JCheckBox();

        setBackground(new java.awt.Color(204, 204, 255));
        setToolTipText("");
        setName("HOTEL MANAGEMENT"); // NOI18N

        jPanel2.setBackground(new java.awt.Color(204, 204, 255));
        jPanel2.setBorder(javax.swing.BorderFactory.createEmptyBorder(1, 1, 1, 1));

        jPanel3.setBackground(new java.awt.Color(204, 204, 255));
        jPanel3.setBorder(javax.swing.BorderFactory.createEmptyBorder(1, 1, 1, 1));

        jLabel2.setText("  Room ID");

        txtIDRoom.setBackground(new java.awt.Color(204, 204, 255));
        txtIDRoom.setBorder(javax.swing.BorderFactory.createMatteBorder(0, 0, 1, 0, new java.awt.Color(51, 51, 51)));

        jLabel3.setText("  Area (m2)");

        txtArea.setBackground(new java.awt.Color(204, 204, 255));
        txtArea.setBorder(javax.swing.BorderFactory.createMatteBorder(0, 0, 1, 0, new java.awt.Color(51, 51, 51)));
        txtArea.addFocusListener(new java.awt.event.FocusAdapter() {
            public void focusGained(java.awt.event.FocusEvent evt) {
                txtAreaFocusGained(evt);
            }
        });
        txtArea.addKeyListener(new java.awt.event.KeyAdapter() {
            public void keyReleased(java.awt.event.KeyEvent evt) {
                txtAreaKeyReleased(evt);
            }
        });

        jLabel4.setText("  Floor");

        txtFloor.setBackground(new java.awt.Color(204, 204, 255));
        txtFloor.setBorder(javax.swing.BorderFactory.createMatteBorder(0, 0, 1, 0, new java.awt.Color(51, 51, 51)));
        txtFloor.addFocusListener(new java.awt.event.FocusAdapter() {
            public void focusGained(java.awt.event.FocusEvent evt) {
                txtFloorFocusGained(evt);
            }
        });
        txtFloor.addKeyListener(new java.awt.event.KeyAdapter() {
            public void keyReleased(java.awt.event.KeyEvent evt) {
                txtFloorKeyReleased(evt);
            }
        });

        jLabel5.setText("  Type");

        cbbType.setBackground(new java.awt.Color(204, 204, 255));
        cbbType.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "Choose Room Type", "Deluxe Room", "Standard Room" }));
        cbbType.setBorder(javax.swing.BorderFactory.createCompoundBorder());
        cbbType.addItemListener(new java.awt.event.ItemListener() {
            public void itemStateChanged(java.awt.event.ItemEvent evt) {
                cbbTypeItemStateChanged(evt);
            }
        });

        jLabel7.setFont(new java.awt.Font("Source Sans Pro", 1, 14)); // NOI18N
        jLabel7.setForeground(new java.awt.Color(0, 0, 255));
        jLabel7.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        jLabel7.setText("ROOM INFORMATION");

        jlbFee.setText("  Fee");
        jlbFee.setVisible(false);

        txtFee.setVisible(false);
        txtFee.setEditable(false);
        txtFee.setBackground(new java.awt.Color(204, 204, 255));
        txtFee.setForeground(new java.awt.Color(255, 0, 0));
        txtFee.setHorizontalAlignment(javax.swing.JTextField.CENTER);
        txtFee.setBorder(javax.swing.BorderFactory.createMatteBorder(0, 0, 1, 0, new java.awt.Color(51, 51, 51)));

        jPanel4.setBackground(new java.awt.Color(204, 204, 255));

        jbnUPDATE.setBackground(new java.awt.Color(220, 220, 220));
        jbnUPDATE.setText("UPDATE");
        jbnUPDATE.setBorderPainted(false);
        jbnUPDATE.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jbnUPDATEActionPerformed(evt);
            }
        });

        jbtnCLEAR.setBackground(new java.awt.Color(220, 220, 220));
        jbtnCLEAR.setText("CLEAR");
        jbtnCLEAR.setBorderPainted(false);
        jbtnCLEAR.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jbtnCLEARActionPerformed(evt);
            }
        });

        jbtnADD.setBackground(new java.awt.Color(220, 220, 220));
        jbtnADD.setText("ADD");
        jbtnADD.setBorderPainted(false);
        jbtnADD.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jbtnADDActionPerformed(evt);
            }
        });

        jbtnDELETE.setBackground(new java.awt.Color(220, 220, 220));
        jbtnDELETE.setText("DELETE");
        jbtnDELETE.setBorderPainted(false);
        jbtnDELETE.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jbtnDELETEActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout jPanel4Layout = new javax.swing.GroupLayout(jPanel4);
        jPanel4.setLayout(jPanel4Layout);
        jPanel4Layout.setHorizontalGroup(
            jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel4Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(jbtnADD, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(jbtnDELETE, javax.swing.GroupLayout.DEFAULT_SIZE, 106, Short.MAX_VALUE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 12, Short.MAX_VALUE)
                .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(jbtnCLEAR, javax.swing.GroupLayout.DEFAULT_SIZE, 112, Short.MAX_VALUE)
                    .addComponent(jbnUPDATE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addContainerGap())
        );
        jPanel4Layout.setVerticalGroup(
            jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel4Layout.createSequentialGroup()
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(jbtnADD, javax.swing.GroupLayout.DEFAULT_SIZE, 44, Short.MAX_VALUE)
                    .addComponent(jbnUPDATE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.UNRELATED)
                .addGroup(jPanel4Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jbtnCLEAR, javax.swing.GroupLayout.PREFERRED_SIZE, 42, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(jbtnDELETE, javax.swing.GroupLayout.PREFERRED_SIZE, 42, javax.swing.GroupLayout.PREFERRED_SIZE)))
        );

        javax.swing.GroupLayout jPanel3Layout = new javax.swing.GroupLayout(jPanel3);
        jPanel3.setLayout(jPanel3Layout);
        jPanel3Layout.setHorizontalGroup(
            jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel3Layout.createSequentialGroup()
                .addContainerGap()
                .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jPanel4, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(jLabel7, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addGroup(jPanel3Layout.createSequentialGroup()
                        .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING, false)
                            .addComponent(jlbFee, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(jLabel4, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(jLabel2, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(jLabel3, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                            .addComponent(jLabel5, javax.swing.GroupLayout.Alignment.LEADING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                        .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, 14, Short.MAX_VALUE)
                        .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                            .addComponent(cbbType, javax.swing.GroupLayout.Alignment.TRAILING, 0, 160, Short.MAX_VALUE)
                            .addComponent(txtFloor, javax.swing.GroupLayout.Alignment.TRAILING)
                            .addComponent(txtArea, javax.swing.GroupLayout.Alignment.TRAILING)
                            .addComponent(txtIDRoom, javax.swing.GroupLayout.Alignment.TRAILING)
                            .addComponent(txtFee))))
                .addContainerGap())
        );
        jPanel3Layout.setVerticalGroup(
            jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel3Layout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jLabel7, javax.swing.GroupLayout.PREFERRED_SIZE, 38, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(jLabel2, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(txtIDRoom, javax.swing.GroupLayout.DEFAULT_SIZE, 35, Short.MAX_VALUE))
                .addGap(30, 30, 30)
                .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(jLabel3, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(txtArea, javax.swing.GroupLayout.DEFAULT_SIZE, 35, Short.MAX_VALUE))
                .addGap(30, 30, 30)
                .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.BASELINE)
                    .addComponent(jLabel4, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE)
                    .addComponent(txtFloor, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE))
                .addGap(30, 30, 30)
                .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(jLabel5, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(cbbType, javax.swing.GroupLayout.DEFAULT_SIZE, 35, Short.MAX_VALUE))
                .addGap(35, 35, 35)
                .addGroup(jPanel3Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING, false)
                    .addComponent(jlbFee, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(txtFee, javax.swing.GroupLayout.DEFAULT_SIZE, 35, Short.MAX_VALUE))
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(jPanel4, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap())
        );

        jtbl.setAutoCreateRowSorter(true);
        jtbl.setBackground(java.awt.SystemColor.controlHighlight);
        jtbl.setModel(new javax.swing.table.DefaultTableModel(
            new Object [][] {

            },
            new String [] {

            }
        ));
        jtbl.setOpaque(false);
        jtbl.addMouseListener(new java.awt.event.MouseAdapter() {
            public void mouseClicked(java.awt.event.MouseEvent evt) {
                jtblMouseClicked(evt);
            }
        });
        jScrollPane1.setViewportView(jtbl);

        jPanel8.setBackground(new java.awt.Color(204, 204, 255));

        jbtnAddList.setBackground(java.awt.Color.lightGray);
        jbtnAddList.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Images/icons8_plus_math_40px.png"))); // NOI18N
        jbtnAddList.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jbtnAddListActionPerformed(evt);
            }
        });

        jbtnConfirm.setBackground(java.awt.Color.lightGray);
        jbtnConfirm.setFont(new java.awt.Font("Source Sans Pro", 1, 14)); // NOI18N
        jbtnConfirm.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Images/icons8_Done_40px.png"))); // NOI18N
        jbtnConfirm.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jbtnConfirmActionPerformed(evt);
            }
        });

        jButton1.setBackground(java.awt.Color.lightGray);
        jButton1.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Images/icons8_synchronize_32px_1.png"))); // NOI18N
        jButton1.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jButton1ActionPerformed(evt);
            }
        });

        jLabel1.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Images/icons8_search_40px.png"))); // NOI18N

        txtSearch.setBackground(new java.awt.Color(204, 204, 255));
        txtSearch.setForeground(new java.awt.Color(0, 0, 0));
        txtSearch.setText("Type to search");
        txtSearch.setBorder(javax.swing.BorderFactory.createMatteBorder(0, 0, 1, 0, new java.awt.Color(51, 51, 51)));
        txtSearch.addFocusListener(new java.awt.event.FocusAdapter() {
            public void focusGained(java.awt.event.FocusEvent evt) {
                txtSearchFocusGained(evt);
            }
            public void focusLost(java.awt.event.FocusEvent evt) {
                txtSearchFocusLost(evt);
            }
        });
        txtSearch.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                txtSearchActionPerformed(evt);
            }
        });
        txtSearch.addKeyListener(new java.awt.event.KeyAdapter() {
            public void keyReleased(java.awt.event.KeyEvent evt) {
                txtSearchKeyReleased(evt);
            }
        });

        jcb.setBackground(new java.awt.Color(204, 204, 255));
        jcb.setText("Renting");
        jcb.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jcbActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout jPanel8Layout = new javax.swing.GroupLayout(jPanel8);
        jPanel8.setLayout(jPanel8Layout);
        jPanel8Layout.setHorizontalGroup(
            jPanel8Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel8Layout.createSequentialGroup()
                .addComponent(jLabel1, javax.swing.GroupLayout.PREFERRED_SIZE, 45, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(4, 4, 4)
                .addComponent(txtSearch, javax.swing.GroupLayout.PREFERRED_SIZE, 199, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jcb, javax.swing.GroupLayout.PREFERRED_SIZE, 76, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addComponent(jbtnAddList)
                .addGap(18, 18, 18)
                .addComponent(jbtnConfirm, javax.swing.GroupLayout.PREFERRED_SIZE, 101, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(18, 18, 18)
                .addComponent(jButton1, javax.swing.GroupLayout.PREFERRED_SIZE, 50, javax.swing.GroupLayout.PREFERRED_SIZE))
        );
        jPanel8Layout.setVerticalGroup(
            jPanel8Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(jbtnConfirm, javax.swing.GroupLayout.PREFERRED_SIZE, 0, Short.MAX_VALUE)
            .addComponent(jButton1, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
            .addComponent(txtSearch)
            .addComponent(jLabel1, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
            .addComponent(jbtnAddList, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.PREFERRED_SIZE, 0, Short.MAX_VALUE)
            .addComponent(jcb, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
        );

        javax.swing.GroupLayout jPanel2Layout = new javax.swing.GroupLayout(jPanel2);
        jPanel2.setLayout(jPanel2Layout);
        jPanel2Layout.setHorizontalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addComponent(jPanel3, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGroup(jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jScrollPane1, javax.swing.GroupLayout.DEFAULT_SIZE, 740, Short.MAX_VALUE)
                    .addComponent(jPanel8, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)))
        );
        jPanel2Layout.setVerticalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.TRAILING)
            .addGroup(jPanel2Layout.createSequentialGroup()
                .addGap(5, 5, 5)
                .addComponent(jPanel8, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jScrollPane1, javax.swing.GroupLayout.DEFAULT_SIZE, 530, Short.MAX_VALUE)
                .addContainerGap())
            .addComponent(jPanel3, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
        );

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(this);
        this.setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 1000, Short.MAX_VALUE)
            .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                .addComponent(jPanel2, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGap(0, 589, Short.MAX_VALUE)
            .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                .addComponent(jPanel2, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );
    }// </editor-fold>//GEN-END:initComponents

    private void cbbTypeItemStateChanged(java.awt.event.ItemEvent evt) {//GEN-FIRST:event_cbbTypeItemStateChanged
        update();
    }//GEN-LAST:event_cbbTypeItemStateChanged

    //Tìm kiếm thông tin phòng hoặc thông tin phòng đã được thuê
    private void txtSearchKeyReleased(java.awt.event.KeyEvent evt) {//GEN-FIRST:event_txtSearchKeyReleased
        try {
            DefaultTableModel dtm = (DefaultTableModel) jtbl.getModel();
            TableRowSorter<DefaultTableModel> trs = new TableRowSorter<DefaultTableModel>(dtm);
            jtbl.setRowSorter(trs);
            trs.setRowFilter(RowFilter.regexFilter("(?i)" + txtSearch.getText().trim()));
        } catch (Exception e) {
        }
    }//GEN-LAST:event_txtSearchKeyReleased

    private void txtSearchFocusGained(java.awt.event.FocusEvent evt) {//GEN-FIRST:event_txtSearchFocusGained
        if(jcb.isSelected()) loadDataTable(true);
        else loadDataTable(false);
        reset();
        txtSearch.setText("");
    }//GEN-LAST:event_txtSearchFocusGained

    private void txtSearchFocusLost(java.awt.event.FocusEvent evt) {//GEN-FIRST:event_txtSearchFocusLost
        if(txtSearch.getText().equals(""))
            txtSearch.setText("Type to search");
    }//GEN-LAST:event_txtSearchFocusLost

    private void jButton1ActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jButton1ActionPerformed
        reload();
    }//GEN-LAST:event_jButton1ActionPerformed

    //Set data to textfield when user click on JTable
    private void jtblMouseClicked(java.awt.event.MouseEvent evt) {//GEN-FIRST:event_jtblMouseClicked
        cbbType.setSelectedIndex(0);
        DefaultTableModel dtm = (DefaultTableModel) jtbl.getModel();
        
        int index = jtbl.convertRowIndexToModel(jtbl.getSelectedRow());
              
        if(index != -1)
        {
            String id = jtbl.getModel().getValueAt(index, 0).toString();
            String area = jtbl.getModel().getValueAt(index, 1).toString();
            String floor = jtbl.getModel().getValueAt(index, 2).toString();
            String type = jtbl.getModel().getValueAt(index, 3).toString();
            txtIDRoom.setText(id);
            txtArea.setText(String.valueOf(area));
            txtFloor.setText(String.valueOf(floor));
            cbbType.setSelectedIndex("Deluxe Room".equals(type) ? 1 : 2);
        }
    }//GEN-LAST:event_jtblMouseClicked

    //Reset textfield
    private void jbtnCLEARActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jbtnCLEARActionPerformed
        reset();
    }//GEN-LAST:event_jbtnCLEARActionPerformed

    //Thêm phòng
    private void jbtnADDActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jbtnADDActionPerformed
        if(isNull())
        {
            JOptionPane.showMessageDialog(this, "Please fill all information", "", JOptionPane.INFORMATION_MESSAGE);
            return;
        }
        
        String id = txtIDRoom.getText().trim();
        double are = Double.parseDouble(txtArea.getText().trim());
        int floor = Integer.parseInt(txtFloor.getText().trim());
        
        switch (cbbType.getSelectedIndex())
        {
            case 1:
                Room rDR = new DeluxeRoom(id, are, floor, true); //Upcasting
                rl.add(rDR);
                break;
            case 2:
                Room rSR = new StandardRoom(id, are, floor, true); //Upcasting
                rl.add(rSR);
                break;
        }
        
        reset();
        loadDataTable(false);
    }//GEN-LAST:event_jbtnADDActionPerformed

    //Cập nhật phòng
    private void jbnUPDATEActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jbnUPDATEActionPerformed
        DefaultTableModel dtm = (DefaultTableModel) jtbl.getModel();
        
        if(jtbl.getSelectedRow() == -1)
        {
            JOptionPane.showMessageDialog(this, "Select room to update!", "", JOptionPane.INFORMATION_MESSAGE);
            reset();
            return;
        }
        
        int index = jtbl.convertRowIndexToModel(jtbl.getSelectedRow());
        
        if(isNull()) return;
        String id = txtIDRoom.getText().trim();
        double are = Double.parseDouble(txtArea.getText().trim());
        int floor = Integer.parseInt(txtFloor.getText().trim());
        boolean available = dtm.getValueAt(index, 5).equals("Available") ? true : false;
        
        switch (cbbType.getSelectedIndex())
        {
            case 1: 
                rl.update(rl.getListRoom().get(index) , new DeluxeRoom(id, are, floor, available)); // upcasting
                break;
            case 2:
                rl.update(rl.getListRoom().get(index) , new StandardRoom(id, are, floor, available)); // upcasting
                break;
        }
        
        reset();
        loadDataTable(false);
    }//GEN-LAST:event_jbnUPDATEActionPerformed

    //Xóa phòng
    private void jbtnDELETEActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jbtnDELETEActionPerformed
        DefaultTableModel dtm = (DefaultTableModel) jtbl.getModel();  
        
        if(jtbl.getSelectedRow() == -1)
        {
            JOptionPane.showMessageDialog(this, "Select room to delete!", "", JOptionPane.INFORMATION_MESSAGE);
            reset();
            return;
        }
        
        int index = jtbl.convertRowIndexToModel(jtbl.getSelectedRow());
        
        int choose = JOptionPane.showConfirmDialog(this, "Are you sure to delete roomID '" + jtbl.getModel().getValueAt(index, 0) + "' ?", "", JOptionPane.OK_CANCEL_OPTION);
        if(choose != JOptionPane.YES_OPTION) return;
        
        rl.delete(index);
        
        reset();
        loadDataTable(false);
    }//GEN-LAST:event_jbtnDELETEActionPerformed

    private void jbtnAddListActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jbtnAddListActionPerformed
        if(jcb.isSelected()) return;
        
        DefaultTableModel dtm = (DefaultTableModel) jtbl.getModel();  
        
        if(jtbl.getSelectedRow() == -1)
        {
            JOptionPane.showMessageDialog(this, "Select room to rent!", "", JOptionPane.INFORMATION_MESSAGE);
            reset();
            return;
        }
        
        int index = jtbl.convertRowIndexToModel(jtbl.getSelectedRow());
        
        if(dtm.getValueAt(index, 5).equals("Not Available")) 
        {
            JOptionPane.showMessageDialog(this, "This room is already rented!", "", JOptionPane.INFORMATION_MESSAGE);
            reset();
            return;
        }
            
        listRent.add(rl.getListRoom().get(index));
        JOptionPane.showMessageDialog(this, "Added!", "", JOptionPane.INFORMATION_MESSAGE);
        jbtnConfirm.setText(" (" + listRent.size() + ")");
        
        Room r = rl.getListRoom().get(index);
        r.setAvailable(false);
        
        reset();
        loadDataTable(false);
    }//GEN-LAST:event_jbtnAddListActionPerformed

    //Thanh toán các phòng đã thuê
    private void jbtnConfirmActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jbtnConfirmActionPerformed
        RentRoom rr = new RentRoom(listRent, this);
        rr.setVisible(true);
    }//GEN-LAST:event_jbtnConfirmActionPerformed

    private void txtAreaFocusGained(java.awt.event.FocusEvent evt) {//GEN-FIRST:event_txtAreaFocusGained
        if(cbbType.getSelectedIndex() == 0 || !txtArea.getText().equals("")) return;
        cbbType.setSelectedIndex(0);
        jlbFee.setVisible(false);
        txtFee.setVisible(false);
    }//GEN-LAST:event_txtAreaFocusGained

    private void txtFloorFocusGained(java.awt.event.FocusEvent evt) {//GEN-FIRST:event_txtFloorFocusGained
        if(cbbType.getSelectedIndex() == 0 || !txtFloor.getText().equals("")) return;
        cbbType.setSelectedIndex(0);
    }//GEN-LAST:event_txtFloorFocusGained

    private void txtFloorKeyReleased(java.awt.event.KeyEvent evt) {//GEN-FIRST:event_txtFloorKeyReleased
        update();
    }//GEN-LAST:event_txtFloorKeyReleased

    private void txtAreaKeyReleased(java.awt.event.KeyEvent evt) {//GEN-FIRST:event_txtAreaKeyReleased
        update();
    }//GEN-LAST:event_txtAreaKeyReleased

    private void txtSearchActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_txtSearchActionPerformed
        // TODO add your handling code here:
    }//GEN-LAST:event_txtSearchActionPerformed

    private void jcbActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jcbActionPerformed
        if(jcb.isSelected())
        {
            txtSearch.setText("Type to search");
            loadDataTable(true);
        }
        else
        {
            txtSearch.setText("Type to search");
            loadDataTable(false);
        }
    }//GEN-LAST:event_jcbActionPerformed
    
    //Tính chi phí phòng
    private void update()
    {
        if(txtArea.getText().trim().equals("") || txtFloor.getText().trim().equals(""))
        {
            txtFee.setText("0 VND/Day");
            return;
        }
        
        try {
            double area = Double.parseDouble(txtArea.getText().trim());
            double floor = Double.parseDouble(txtFloor.getText().trim());
            double fee;

            switch (cbbType.getSelectedIndex()) {
                case 0:
                    txtFee.setText("0 VND/Day");
                    jlbFee.setVisible(false);
                    txtFee.setVisible(false);
                    break;
                case 1:
                    jlbFee.setVisible(true);
                    txtFee.setVisible(true);
                    fee = area * 150000 + floor * 500000;
                    txtFee.setText(formatter.format(fee) + " VND/Day");
                    break;
                case 2:
                    jlbFee.setVisible(true);
                    txtFee.setVisible(true);
                    fee =  area * 100000;
                    txtFee.setText(formatter.format(fee) + " VND/Day");
                    break;
            }
        } catch (Exception e) {
            txtFee.setText("Error !");
        }
    }
    
    //Kiểm tra tất cả thông tin đã được điền đầy đủ chưa?
    private boolean isNull()
    {
        if(cbbType.getSelectedIndex() == 0) return true;
        else return txtIDRoom.getText().equals("") && txtArea.getText().equals("") && 
                    txtFloor.getText().equals("");
    }
    
    //Khởi tạo lại panel
    public void reload()
    {      
        jcb.setSelected(false);
        
        this.removeAll();
        this.revalidate();
        initComponents();
        
        jScrollPane1.getViewport().setBackground(new Color(204,204,255));
        jtbl.getTableHeader().setBackground(new Color(238, 232, 170));
        jtbl.setSelectionBackground(new Color(135, 206, 250));

        //initTable();
        loadDataTable(false);
    }
    
    public void reLoadListRent()
    {
        if(listRent.size() == 0) jbtnConfirm.setText("");
        else jbtnConfirm.setText(" (" + listRent.size() + ")");
        loadDataTable(false);
    }
    
    //Khởi tạo danh sách phòng mà khách thuê
    public void setListNull()
    {
        listRent = new ArrayList<>();
    }
    
    //Xóa dữ liệu các textfield người dùng điền vào
    private void reset()
    {
        jtbl.clearSelection();
        txtIDRoom.setText("");
        txtArea.setText("");
        txtFloor.setText("");
        cbbType.setSelectedIndex(0);
        jlbFee.setVisible(false);
        txtFee.setVisible(false);
    }
    

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JComboBox<String> cbbType;
    public javax.swing.JButton jButton1;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel3;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JLabel jLabel5;
    private javax.swing.JLabel jLabel7;
    private javax.swing.JPanel jPanel2;
    private javax.swing.JPanel jPanel3;
    private javax.swing.JPanel jPanel4;
    private javax.swing.JPanel jPanel8;
    private javax.swing.JScrollPane jScrollPane1;
    private javax.swing.JButton jbnUPDATE;
    private javax.swing.JButton jbtnADD;
    private javax.swing.JButton jbtnAddList;
    private javax.swing.JButton jbtnCLEAR;
    private javax.swing.JButton jbtnConfirm;
    private javax.swing.JButton jbtnDELETE;
    private javax.swing.JCheckBox jcb;
    private javax.swing.JLabel jlbFee;
    private javax.swing.JTable jtbl;
    private javax.swing.JTextField txtArea;
    private javax.swing.JTextField txtFee;
    private javax.swing.JTextField txtFloor;
    private javax.swing.JTextField txtIDRoom;
    private javax.swing.JTextField txtSearch;
    // End of variables declaration//GEN-END:variables

    
}
