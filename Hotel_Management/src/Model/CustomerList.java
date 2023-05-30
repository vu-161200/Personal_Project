
package Model;

import java.util.ArrayList;
import java.util.List;
import javax.swing.JOptionPane;

/**
 *
 * @author Đ.Tee
 */

public class CustomerList {
    
    //Khai báo biến static là danh sách các khách hàng
    //Để dùng chung
    //Chạy xuyên xuốt chương trình
    static List<Customer> customerList;
    
    //Constructor
    public CustomerList()
    {
        if(customerList == null) customerList = new ArrayList<>();
    }
    
    //Thêm 1 khách hàng vào danh sách
    public void add(Customer c)
    {
        customerList.add(c);
        JOptionPane.showMessageDialog(null, "Success!", "", JOptionPane.INFORMATION_MESSAGE);
    }
    
    //Lấy danh sách khách hàng
    public List<Customer> getList()
    {
        return customerList;
    } 
        
}
