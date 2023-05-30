
package Model;

import java.util.List;
import java.util.ArrayList;
import javax.swing.JOptionPane;

/**
 *
 * @author Vũ
 */

public class RoomList {
    
    //Khai báo biến static là danh sách các phòng
    //Để dùng chung
    //Chạy xuyên xuốt chương trình
    static List<Room> roomList;
    
    //Khởi tại và thêm dữ liệu mẫu vào list nếu null
    public RoomList()
    {
        if(roomList == null)
        {
            roomList = new ArrayList<>();
            setDataToList();
        }

    }
    
    public void setDataToList()
    {
        
        roomList.add(new StandardRoom("1-1", 20, 1, true));
        roomList.add(new StandardRoom("1-2", 25.5, 1, true));
        roomList.add(new DeluxeRoom("1-3", 35.5, 1, true));
        
        roomList.add(new DeluxeRoom("2-1", 33.5, 2, true));
        roomList.add(new StandardRoom("2-2", 20.5, 2, true));
        roomList.add(new DeluxeRoom("2-3", 30.5, 2, true));
        
        roomList.add(new StandardRoom("3-1", 27.5, 3, true));
        roomList.add(new DeluxeRoom("3-2", 40.5, 3, true));
        
        roomList.add(new DeluxeRoom("4-1", 30.75, 4, true));
        roomList.add(new StandardRoom("4-2", 17.75, 4, true));
        roomList.add(new StandardRoom("4-3", 17.75, 4, true));
        
        roomList.add(new DeluxeRoom("5-1", 40.5, 5, true));
        roomList.add(new StandardRoom("5-2", 20.5, 5, true));
        
        roomList.add(new DeluxeRoom("6-1", 45.5, 6, true));
        roomList.add(new StandardRoom("6-2", 25, 6, true));
        
        roomList.add(new StandardRoom("7-1", 17.5, 7, true));
        roomList.add(new DeluxeRoom("7-2", 25.5, 7, true));
        roomList.add(new DeluxeRoom("7-3", 45.5, 7, true));
        
        roomList.add(new DeluxeRoom("8-1", 30, 8, true));
        roomList.add(new StandardRoom("8-2", 15.5, 8, true));
        
        roomList.add(new DeluxeRoom("9-1", 50.5, 9, true));
        roomList.add(new DeluxeRoom("9-9", 45.5, 9, true));
    }
    
    //Thêm room nếu roomID chưa tồn tại
    public void add(Room r)
    {
        //RoomID: key, duy nhất
        //Kiểm tra xem room ID đã tồn tại hay chưa
        for(Room room : roomList)
        {
            if(room.getRoomID().equals(r.getRoomID()))
            {
                JOptionPane.showMessageDialog(null, "Room ID '" + r.getRoomID() + "' already exists!", "", JOptionPane.INFORMATION_MESSAGE);
                return;
            }
        }
        JOptionPane.showMessageDialog(null, "Added!", "", JOptionPane.INFORMATION_MESSAGE);
        roomList.add(r);
    }
    
    //Cập nhật lại room
    public void update(Room rBefore, Room rAfter)
    {
        //Kiểm tra trường hợp cập nhật roomID mà roomID đó đã tồn tại
        if(!rBefore.getRoomID().equals(rAfter.getRoomID()))
        {       
            for(Room room : roomList)
            {
                if(room.getRoomID().equals(rAfter.getRoomID()))
                {
                    JOptionPane.showMessageDialog(null, "Room ID '" + rAfter.getRoomID() + "' already exists!", "", JOptionPane.INFORMATION_MESSAGE);
                    return;
                }
            }
        }
        JOptionPane.showMessageDialog(null, "Updated!", "", JOptionPane.INFORMATION_MESSAGE);
        int index = roomList.indexOf(rBefore);
        roomList.set(index, rAfter);
    }
    
    //Xóa room
    public void delete(int index)
    {
        JOptionPane.showMessageDialog(null, "Deleted!", "", JOptionPane.INFORMATION_MESSAGE);
        roomList.remove(index);
    }

    //Get
    public List<Room> getListRoom()
    {
        return roomList;
    }
    
}
