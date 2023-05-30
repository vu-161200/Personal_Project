
package Model;

/**
 *
 * @author Diệu
 */

public class StandardRoom extends Room{

    private final int PRICE_BY_AREA = 100000;
    
    public StandardRoom(String roomID, double area, int floor, boolean available) {
        super(roomID, area, floor, available);
    }

    //Tính phí của phòng
    //return double
    public double getFee()
    {
        double fee = area * PRICE_BY_AREA;
        double feeOff = (double) Math.round(fee*100)/100; //Làm tròn 2 số sau dấu ','
        return feeOff;
    }
    
    //Loại phòng
    public String getType()
    {
        return "STANDARAD_ROOM";
    }
    
    
}
