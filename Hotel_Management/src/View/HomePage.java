
package View;

import Model.Customer;
import Model.CustomerList;
import Model.Room;
import com.toedter.calendar.JDateChooser;
import java.awt.BorderLayout;
import java.awt.Image;
import java.awt.Toolkit;
import java.awt.event.ComponentAdapter;
import java.awt.event.ComponentEvent;
import java.text.NumberFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;
import java.util.logging.Level;
import java.util.logging.Logger;
import javax.swing.ImageIcon;
import javax.swing.JOptionPane;
import javax.swing.JPanel;

/**
 *
 * @author Đ.Tee, Vũ, Diệu
 */

public class HomePage extends javax.swing.JFrame {

    SimpleDateFormat sdf = new SimpleDateFormat("MM/dd/yyyy");
    SimpleDateFormat sdfVN = new SimpleDateFormat("dd/MM/yyyy");
    
    NumberFormat formatter = NumberFormat.getInstance();
    
    JPanel hotelinfo;
    
    boolean maximun = false, home = true;
    
    public HomePage() {
        setIconImage(Toolkit.getDefaultToolkit().getImage(getClass().getResource("/Images/icons8_hotel_125px_1.png")));
        showCurrentTime().start();
        
        initComponents();
        setLocationRelativeTo(null);
        
        home();
        
        final int with = jplmain.getWidth();
        final int height = jplmain.getHeight();
        
        this.addComponentListener(new ComponentAdapter() {
            public void componentResized(ComponentEvent componentEvent) {
                if(!maximun)
                {
                    maximun = true;
                    if(!home) return;
                    jplmain.setSize(with, height);
                    home();
                }
                else
                {
                    maximun = false;
                    if(!home) return;
                    home();
                }
            }
        });
        
    }
      
    private Thread showCurrentTime()
    {
        SimpleDateFormat sdfCurrTime = new SimpleDateFormat("EEEE, MMMM dd, yyyy\n     HH:mm:ss a");
        Thread thread = new Thread(() -> {
            while(true)
            {
                try {
                    Thread.sleep(1000);
                    txtTime.setText(sdfCurrTime.format(new Date()));
                } catch (InterruptedException ex) {
                    Logger.getLogger(HomePage.class.getName()).log(Level.SEVERE, null, ex);
                }
            }
        });
        
        return thread;
    }
    
    private void home()
    {
        home = true;
        IMGHome jpl = new IMGHome(jplmain.getWidth(), jplmain.getHeight());
        jplmain.removeAll();
        jplmain.setLayout(new BorderLayout());
        jplmain.add(jpl);
        jplmain.validate();
    }
    
    @SuppressWarnings("unchecked")
    // <editor-fold defaultstate="collapsed" desc="Generated Code">//GEN-BEGIN:initComponents
    private void initComponents() {

        jplTT = new javax.swing.JPanel();
        jbtnRI = new javax.swing.JButton();
        jbtnRevenue = new javax.swing.JButton();
        jLabel4 = new javax.swing.JLabel();
        jLabel2 = new javax.swing.JLabel();
        jLabel5 = new javax.swing.JLabel();
        jbtnSBR = new javax.swing.JButton();
        jbtnSBT = new javax.swing.JButton();
        jbtnHome = new javax.swing.JButton();
        jplmain = new javax.swing.JPanel();
        jComboBox1 = new javax.swing.JComboBox<>();
        jPanel1 = new javax.swing.JPanel();
        jLabel1 = new javax.swing.JLabel();
        jPanel2 = new javax.swing.JPanel();
        txtTime = new javax.swing.JLabel();

        setDefaultCloseOperation(javax.swing.WindowConstants.EXIT_ON_CLOSE);
        setTitle("HOTEL MANAGEMENT");
        setBackground(new java.awt.Color(120, 168, 252));

        jplTT.setBackground(new java.awt.Color(33, 63, 86));
        jplTT.setToolTipText("");

        jbtnRI.setBackground(new java.awt.Color(204, 204, 255));
        jbtnRI.setText("Room Info");
        jbtnRI.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jbtnRIActionPerformed(evt);
            }
        });

        jbtnRevenue.setBackground(new java.awt.Color(204, 204, 255));
        jbtnRevenue.setText("Revenue");
        jbtnRevenue.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jbtnRevenueActionPerformed(evt);
            }
        });

        jLabel4.setFont(new java.awt.Font("Source Sans Pro", 1, 13)); // NOI18N
        jLabel4.setForeground(new java.awt.Color(204, 204, 255));
        jLabel4.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        jLabel4.setText("Total Revenue");

        jLabel2.setFont(new java.awt.Font("Source Sans Pro", 1, 13)); // NOI18N
        jLabel2.setForeground(new java.awt.Color(204, 204, 255));
        jLabel2.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        jLabel2.setText("Statistics");

        jLabel5.setBackground(new java.awt.Color(120, 168, 252));
        jLabel5.setFont(new java.awt.Font("Source Sans Pro", 1, 14)); // NOI18N
        jLabel5.setForeground(new java.awt.Color(204, 204, 255));
        jLabel5.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        jLabel5.setText("Management");

        jbtnSBR.setBackground(new java.awt.Color(204, 204, 255));
        jbtnSBR.setText("By Room");
        jbtnSBR.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jbtnSBRActionPerformed(evt);
            }
        });

        jbtnSBT.setBackground(new java.awt.Color(204, 204, 255));
        jbtnSBT.setText("By Time");
        jbtnSBT.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jbtnSBTActionPerformed(evt);
            }
        });

        jbtnHome.setBackground(new java.awt.Color(120, 168, 252));
        jbtnHome.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Images/icons8_home_40px.png"))); // NOI18N
        jbtnHome.addActionListener(new java.awt.event.ActionListener() {
            public void actionPerformed(java.awt.event.ActionEvent evt) {
                jbtnHomeActionPerformed(evt);
            }
        });

        javax.swing.GroupLayout jplTTLayout = new javax.swing.GroupLayout(jplTT);
        jplTT.setLayout(jplTTLayout);
        jplTTLayout.setHorizontalGroup(
            jplTTLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(jLabel5, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
            .addComponent(jLabel2, javax.swing.GroupLayout.DEFAULT_SIZE, 100, Short.MAX_VALUE)
            .addComponent(jLabel4, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
            .addGroup(jplTTLayout.createSequentialGroup()
                .addContainerGap()
                .addGroup(jplTTLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addComponent(jbtnSBR, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(jbtnRI, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(jbtnSBT, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addComponent(jbtnRevenue, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
                .addContainerGap())
            .addComponent(jbtnHome, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
        );
        jplTTLayout.setVerticalGroup(
            jplTTLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jplTTLayout.createSequentialGroup()
                .addContainerGap()
                .addComponent(jbtnHome, javax.swing.GroupLayout.PREFERRED_SIZE, 44, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addPreferredGap(javax.swing.LayoutStyle.ComponentPlacement.RELATED)
                .addComponent(jLabel5, javax.swing.GroupLayout.PREFERRED_SIZE, 40, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(2, 2, 2)
                .addComponent(jbtnRI, javax.swing.GroupLayout.PREFERRED_SIZE, 40, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(30, 30, 30)
                .addComponent(jLabel4, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(1, 1, 1)
                .addComponent(jbtnRevenue, javax.swing.GroupLayout.PREFERRED_SIZE, 40, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(30, 30, 30)
                .addComponent(jLabel2, javax.swing.GroupLayout.PREFERRED_SIZE, 35, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(1, 1, 1)
                .addComponent(jbtnSBR, javax.swing.GroupLayout.PREFERRED_SIZE, 40, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(1, 1, 1)
                .addComponent(jbtnSBT, javax.swing.GroupLayout.PREFERRED_SIZE, 40, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(238, Short.MAX_VALUE))
        );

        jplmain.setBackground(new java.awt.Color(204, 204, 255));
        jplmain.setBorder(javax.swing.BorderFactory.createMatteBorder(1, 1, 0, 0, new java.awt.Color(51, 51, 51)));

        jComboBox1.setModel(new javax.swing.DefaultComboBoxModel<>(new String[] { "Item 1", "Item 2", "Item 3", "Item 4" }));

        javax.swing.GroupLayout jplmainLayout = new javax.swing.GroupLayout(jplmain);
        jplmain.setLayout(jplmainLayout);
        jplmainLayout.setHorizontalGroup(
            jplmainLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jplmainLayout.createSequentialGroup()
                .addGap(450, 450, 450)
                .addComponent(jComboBox1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(441, Short.MAX_VALUE))
        );
        jplmainLayout.setVerticalGroup(
            jplmainLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jplmainLayout.createSequentialGroup()
                .addGap(193, 193, 193)
                .addComponent(jComboBox1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addContainerGap(javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE))
        );

        jPanel1.setBackground(new java.awt.Color(120, 168, 252));
        jPanel1.setBorder(javax.swing.BorderFactory.createMatteBorder(0, 1, 0, 0, new java.awt.Color(51, 51, 51)));

        jLabel1.setBackground(new java.awt.Color(204, 204, 255));
        jLabel1.setFont(new java.awt.Font("Source Sans Pro", 1, 14)); // NOI18N
        jLabel1.setForeground(new java.awt.Color(255, 0, 0));
        jLabel1.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
        jLabel1.setIcon(new javax.swing.ImageIcon(getClass().getResource("/Images/icons8_management_40px.png"))); // NOI18N
        jLabel1.setText("   HOTEL MANAGEMENT");
        jLabel1.setBorder(javax.swing.BorderFactory.createEmptyBorder(1, 1, 1, 1));

        javax.swing.GroupLayout jPanel1Layout = new javax.swing.GroupLayout(jPanel1);
        jPanel1.setLayout(jPanel1Layout);
        jPanel1Layout.setHorizontalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(jPanel1Layout.createSequentialGroup()
                .addGap(233, 233, 233)
                .addComponent(jLabel1, javax.swing.GroupLayout.DEFAULT_SIZE, 785, Short.MAX_VALUE))
        );
        jPanel1Layout.setVerticalGroup(
            jPanel1Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(jLabel1, javax.swing.GroupLayout.DEFAULT_SIZE, 56, Short.MAX_VALUE)
        );

        jPanel2.setBackground(new java.awt.Color(120, 168, 252));
        jPanel2.setBorder(javax.swing.BorderFactory.createMatteBorder(0, 1, 0, 0, new java.awt.Color(51, 51, 51)));

        txtTime.setBackground(new java.awt.Color(47, 43, 67));
        txtTime.setForeground(new java.awt.Color(0, 0, 0));
        txtTime.setText(" ");

        javax.swing.GroupLayout jPanel2Layout = new javax.swing.GroupLayout(jPanel2);
        jPanel2.setLayout(jPanel2Layout);
        jPanel2Layout.setHorizontalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(javax.swing.GroupLayout.Alignment.TRAILING, jPanel2Layout.createSequentialGroup()
                .addGap(0, 12, Short.MAX_VALUE)
                .addComponent(txtTime, javax.swing.GroupLayout.PREFERRED_SIZE, 248, javax.swing.GroupLayout.PREFERRED_SIZE))
        );
        jPanel2Layout.setVerticalGroup(
            jPanel2Layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addComponent(txtTime, javax.swing.GroupLayout.DEFAULT_SIZE, 19, Short.MAX_VALUE)
        );

        javax.swing.GroupLayout layout = new javax.swing.GroupLayout(getContentPane());
        getContentPane().setLayout(layout);
        layout.setHorizontalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addComponent(jplTT, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGroup(layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                    .addGroup(layout.createSequentialGroup()
                        .addComponent(jPanel2, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addGap(758, 758, 758))
                    .addComponent(jPanel1, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                    .addGroup(layout.createSequentialGroup()
                        .addGap(49, 49, 49)
                        .addComponent(jplmain, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                        .addContainerGap())))
        );
        layout.setVerticalGroup(
            layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
            .addGroup(layout.createSequentialGroup()
                .addComponent(jPanel1, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE)
                .addGap(46, 46, 46)
                .addComponent(jplmain, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
                .addGap(18, 18, 18)
                .addComponent(jPanel2, javax.swing.GroupLayout.PREFERRED_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.PREFERRED_SIZE))
            .addComponent(jplTT, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
        );

        pack();
    }// </editor-fold>//GEN-END:initComponents
    
    private void jbtnRIActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jbtnRIActionPerformed
        home = false;

        hotelinfo = new HotelInfo();
        jplmain.removeAll();
        jplmain.setLayout(new BorderLayout());
        jplmain.add(hotelinfo);
        jplmain.validate();
    }//GEN-LAST:event_jbtnRIActionPerformed

    private void jbtnRevenueActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jbtnRevenueActionPerformed
        double totalRevenue = 0;
       
        Date start = null, end = null, from, to;
        
        JDateChooser jdc = new JDateChooser();
        
        String options[] = {"OK"};
        int choose1 = JOptionPane.showOptionDialog(null, jdc, "Enter start time", JOptionPane.DEFAULT_OPTION, JOptionPane.DEFAULT_OPTION, null, options, "OK");
        if(choose1 == JOptionPane.YES_OPTION) start = new Date(sdf.format(jdc.getDate()));
        else return;
        
        int choose2 = JOptionPane.showOptionDialog(null, jdc, "Enter stop time", JOptionPane.DEFAULT_OPTION, JOptionPane.DEFAULT_OPTION, null, options, "OK");
        if(choose2 == JOptionPane.YES_OPTION) end = new Date(sdf.format(jdc.getDate()));
        else return;
        
        if(end.compareTo(start) < 0)
        {
            JOptionPane.showMessageDialog(this, "Invalid period!", "Error", JOptionPane.ERROR_MESSAGE);
            return;
        }
        
        //========================
        CustomerList cl = new CustomerList();
        List<Customer> customerList = cl.getList();

        for (Customer c : customerList) {
            //So sánh khoảng thời gian nhập vào với khoảng thời gian thuê của từng khách hàng
            if(c.getStartRenting().compareTo(start) >= 0) from = c.getStartRenting();
            else from = start;
            
            if(c.getStopRenting().compareTo(end) >= 0) to = end;
            else to = c.getStopRenting();
            
            //Khoảng cách giữa 2 ngày
            int days = c.getNumOfDays(from, to) + 1;
            
            List<Room> roomList = c.getRoomList();
            
            for (Room r : roomList) {
                totalRevenue += r.getFee() * days;
            }
            
        }

        String result = "<html>-- The hotel's total revenue from <font color=red>" + sdfVN.format(start) + "</font> to "
                      + "<font color=red>" + sdfVN.format(end) + "</font> : " + "<font color=red>" + formatter.format(totalRevenue) + " VNĐ </font>";
        JOptionPane.showOptionDialog(null, result, "TOTAL REVENUE OF HOTEL", JOptionPane.DEFAULT_OPTION, JOptionPane.DEFAULT_OPTION, null, new Object[]{}, null);

    }//GEN-LAST:event_jbtnRevenueActionPerformed

    private void jbtnSBRActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jbtnSBRActionPerformed
        int deluxe = 0; // số phòng cao cấp đc thuê
        int standard = 0; // số phòng tiêu chuẩn được thuê
        int totalCus = 0; // tổng số khách hàng thuê phòng
        boolean isDeluxe = false; // khách hàng có thuê phòng cao cấp không?
        boolean isStandard = false; // khách hàng có thuê phòng tiêu chuẩn không?
        
        CustomerList cl = new CustomerList();
        List<Customer> customerList = cl.getList();
        
        for(Customer c : customerList)
        {
            totalCus++;
            isDeluxe = false;
            isStandard = false;
            
            List<Room> roomList = c.getRoomList();
            for(Room r : roomList)
            {
                //Kiểm tra khách hàng đó thuê phòng cao cấp hay tiêu chuẩn hay cả hai?
                if(r.getType().equals("DELUXE_ROOM"))
                {
                    isDeluxe = true;
                }
                else
                {
                    isStandard = true;
                }
            }
            if(isDeluxe) deluxe++;
            if(isStandard) standard++;
        }
        
        String result = "<html>- Total customers:  <font color=red>" + String.valueOf(totalCus) + "</font>" +
                        "\n<html> + The number of customer renting Deluxe Rooms:  <font color=red>" + String.valueOf(deluxe) +
                        "\n<html> + The number of customer renting Standard Rooms:  <font color=red>" + String.valueOf(standard) + "</font>";
        
        JOptionPane.showOptionDialog(null, result, "STATISTICS BY TYPE OF ROOM", JOptionPane.DEFAULT_OPTION, JOptionPane.DEFAULT_OPTION, null, new Object[]{}, null);
    }//GEN-LAST:event_jbtnSBRActionPerformed

    private void jbtnSBTActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jbtnSBTActionPerformed
        
        Date start = null, end = null;
        
        JDateChooser jdc = new JDateChooser();
        
        String options[] = {"OK"};
        int choose1 = JOptionPane.showOptionDialog(null, jdc, "Enter start time", JOptionPane.DEFAULT_OPTION, JOptionPane.DEFAULT_OPTION, null, options, "OK");
        if(choose1 == JOptionPane.YES_OPTION) start = new Date(sdf.format(jdc.getDate()));
        else return;
        
        int choose2 = JOptionPane.showOptionDialog(null, jdc, "Enter stop time", JOptionPane.DEFAULT_OPTION, JOptionPane.DEFAULT_OPTION, null, options, "OK");
        if(choose2 == JOptionPane.YES_OPTION) end = new Date(sdf.format(jdc.getDate()));
        else return;
        
        if(end.compareTo(start) < 0)
        {
            JOptionPane.showMessageDialog(this, "Invalid period!", "Error", JOptionPane.ERROR_MESSAGE);
            return;
        }

        int totalCus = 0; // tổng số khách hàng thuê
        int totalCusRent = 0; // tổng số khách hàng thuê trong khoảng thời gian nhập vào

        CustomerList cl = new CustomerList();
        List<Customer> customerList = cl.getList();

        for (Customer c : customerList) {
            totalCus++;
            
            //Kiểm tra khoảng thời gian nhập vào so với khoảng thời gian mà khách thuê
            if(c.checkRentalDate(start) && c.checkRentalDate(end)) totalCusRent++;
            
        }

        String result = "<html>-- Total customers:  <font color=red>" + String.valueOf(totalCus) + "</font>"
                + "\n<html>-- The number of customer renting room from <font color=red>" + sdfVN.format(start) + "</font> to <font color=red>" + 
                    sdfVN.format(end) + "</font> : <font color=red>" + String.valueOf(totalCusRent) + "</font>\n";

        JOptionPane.showOptionDialog(null, result, "STATISTICS BY TIME", JOptionPane.DEFAULT_OPTION, JOptionPane.DEFAULT_OPTION, null, new Object[]{}, null);

        
    }//GEN-LAST:event_jbtnSBTActionPerformed

    private void jbtnHomeActionPerformed(java.awt.event.ActionEvent evt) {//GEN-FIRST:event_jbtnHomeActionPerformed
        home();
    }//GEN-LAST:event_jbtnHomeActionPerformed

    // Variables declaration - do not modify//GEN-BEGIN:variables
    private javax.swing.JComboBox<String> jComboBox1;
    private javax.swing.JLabel jLabel1;
    private javax.swing.JLabel jLabel2;
    private javax.swing.JLabel jLabel4;
    private javax.swing.JLabel jLabel5;
    private javax.swing.JPanel jPanel1;
    private javax.swing.JPanel jPanel2;
    private javax.swing.JButton jbtnHome;
    private javax.swing.JButton jbtnRI;
    private javax.swing.JButton jbtnRevenue;
    private javax.swing.JButton jbtnSBR;
    private javax.swing.JButton jbtnSBT;
    private javax.swing.JPanel jplTT;
    private javax.swing.JPanel jplmain;
    private javax.swing.JLabel txtTime;
    // End of variables declaration//GEN-END:variables
    
    class IMGHome extends javax.swing.JPanel {

        public IMGHome(int with, int height) {
            initComponents();

            ImageIcon imgIcon = new ImageIcon(Toolkit.getDefaultToolkit().getImage(getClass().getResource("/Images/digital-marketing-for-hotels.jpg")));

            Image img1 = imgIcon.getImage();
            Image img2 = img1.getScaledInstance(with, height, Image.SCALE_SMOOTH);

            ImageIcon ii = new ImageIcon(img2);

            jlb.setIcon(ii);

        }
                      
        private void initComponents() {

            jpl = new javax.swing.JPanel();
            jlb = new javax.swing.JLabel();

            javax.swing.GroupLayout jplLayout = new javax.swing.GroupLayout(jpl);
            jpl.setLayout(jplLayout);
            jplLayout.setHorizontalGroup(
                jplLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                .addComponent(jlb, javax.swing.GroupLayout.DEFAULT_SIZE, 990, Short.MAX_VALUE)
            );
            jplLayout.setVerticalGroup(
                jplLayout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                .addComponent(jlb, javax.swing.GroupLayout.DEFAULT_SIZE, 589, Short.MAX_VALUE)
            );

            javax.swing.GroupLayout layout = new javax.swing.GroupLayout(this);
            this.setLayout(layout);
            layout.setHorizontalGroup(
                layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                .addComponent(jpl, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
            );
            layout.setVerticalGroup(
                layout.createParallelGroup(javax.swing.GroupLayout.Alignment.LEADING)
                .addComponent(jpl, javax.swing.GroupLayout.Alignment.TRAILING, javax.swing.GroupLayout.DEFAULT_SIZE, javax.swing.GroupLayout.DEFAULT_SIZE, Short.MAX_VALUE)
            );
        }                    
                   
        private javax.swing.JLabel jlb;
        private javax.swing.JPanel jpl;
                
    }
    
}
