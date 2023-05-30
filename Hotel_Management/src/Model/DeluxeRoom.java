
package Model;

/**
 *
 * @author Diệu
 */

public class DeluxeRoom extends Room{
    //Constant
    private final int PRICE_BY_AREA = 150000;
    private final int PRICE_BY_FLOOR = 500000;

    public DeluxeRoom(String roomID, double area, int floor, boolean available) {
        super(roomID, area, floor, available);
    }
    
    //Tính phí thuê phòng, trả về double
    public double getFee()
    {
        double fee = area * PRICE_BY_AREA + floor * PRICE_BY_FLOOR;
        double feeOff = (double) Math.round(fee*100)/100; //Làm tròn 2 số sau dấu ','
        return feeOff;
    }
    
    //Loại phòng
    public String getType()
    {
        return "DELUXE_ROOM";
    }
    
    
}
