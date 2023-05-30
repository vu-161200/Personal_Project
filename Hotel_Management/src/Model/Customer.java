
package Model;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.concurrent.TimeUnit;

/**
 *
 * @author Đ.Tee
 */

public class Customer {
    private List<Room> roomList = new ArrayList<>(); //Danh sách phòng mà khánh hàng đó thuê
    private String fullName; //Họ tên khách hàng
    private String identityCardID; // CMND/CCCD
    private String phoneNumber; // SĐT
    private Date startRenting, stopRenting; //Thời gian bắt đầu - Thời gian kết thúc thuê

    //Default constructor
    public Customer() {
    }
    
    //Constructor
    public Customer(String fullName, String identityCardID, String phoneNumber, List<Room> roomList, Date startRenting, Date stopRenting) {
        this.fullName = fullName;
        this.identityCardID = identityCardID;
        this.phoneNumber = phoneNumber;
        this.roomList = roomList;
        this.startRenting = startRenting;
        this.stopRenting = stopRenting;
    }

    //Getter and Setter
    public List<Room> getRoomList() {
        return roomList;
    }

    public String getFullName() {
        return fullName;
    }

    public void setFullName(String fullName) {
        this.fullName = fullName;
    }

    public String getIdentityCardID() {
        return identityCardID;
    }

    public void setIdentityCardID(String identityCardID) {
        this.identityCardID = identityCardID;
    }

    public String getPhoneNumber() {
        return phoneNumber;
    }

    public void setPhoneNumber(String phoneNumber) {
        this.phoneNumber = phoneNumber;
    }

    public Date getStartRenting() {
        return startRenting;
    }

    public void setStartRenting(Date startRenting) {
        this.startRenting = startRenting;
    }

    public Date getStopRenting() {
        return stopRenting;
    }

    public void setStopRenting(Date stopRenting) {
        this.stopRenting = stopRenting;
    }

    //Tính khoảng cách (số ngày) giữa ngày bắt đầu thuê và ngày kết thúc thuê
    public int getNumOfDays()
    {
        long diff = getStopRenting().getTime() - getStartRenting().getTime();
        TimeUnit time = TimeUnit.DAYS; 
        int diffrence = (int) time.convert(diff, TimeUnit.MILLISECONDS);
        
        return diffrence;
    }
    
    //Tính khoảng cách (số ngày) giữa 2 ngày nhập vào
    public int getNumOfDays(Date start, Date stop)
    {
        long diff = stop.getTime() - start.getTime();
        TimeUnit time = TimeUnit.DAYS; 
        int diffrence = (int) time.convert(diff, TimeUnit.MILLISECONDS);
        
        return diffrence;
    }
    
    //Kiểm tra ngày nhập vào có thuộc khoảng thời gian thuê không?
    public boolean checkRentalDate(Date date)
    {
        // Start <= date <= Stop
        return date.compareTo(getStartRenting()) >= 0 && date.compareTo(getStopRenting()) <= 0;
    }
    
    
}
