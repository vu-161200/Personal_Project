
package Main;

import View.HomePage;

/**
 *
 * @author Đ.Tee
 */

//Chạy chương trình
public class HotelManagement {

    public static void main(String[] args) {
        java.awt.EventQueue.invokeLater(new Runnable() {
            public void run() {
                new HomePage().setVisible(true);
            }
        });

    }

}
