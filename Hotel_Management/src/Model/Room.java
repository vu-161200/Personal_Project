
package Model;

/**
 *
 * @author Vũ
 */

public class Room {
    protected String roomID;
    protected double area;
    protected int floor;
    protected boolean available;

    public Room() {
    }
    
    public Room(String roomID, double area, int floor, boolean available) {
        this.roomID = roomID;
        this.area = area;
        this.floor = floor;
        this.available = available;
    }
    
    public boolean isAvailable() {
        return available;
    }

    public void setAvailable(boolean available) {
        this.available = available;
    }

    public String getRoomID() {
        return roomID;
    }

    public void setRoomID(String roomID) {
        this.roomID = roomID;
    }

    public double getArea() {
        return area;
    }

    public void setArea(double area) {
        this.area = area;
    }

    public int getFloor() {
        return floor;
    }

    public void setFloor(int floor) {
        this.floor = floor;
    }

    public double getFee()
    {
        return 0.0;
    }
    
    public String getType()
    {
        return "";
    }
    
    
    
}
