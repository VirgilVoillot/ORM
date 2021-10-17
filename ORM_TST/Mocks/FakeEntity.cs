using ORM;

public class FakeEntity : Entity {

    public override string Table(){
        return Constants.TABLE_NAME;
        }
        
    private int _clientID;
    [ColumnAttribute(Constants.COLUMN_NAME)]
    public int ClientID{
        get => _clientID;
        set => _clientID = value;
    }
}