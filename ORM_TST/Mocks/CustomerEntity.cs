using ORM;
namespace ORM_TST
{

[TableAttribute(Constants.TABLE_NAME)]
public class CustomerEntity : Entity {
        
    private int _propertyNotInDB;
    public int PropertyNotInDB{
        get => _propertyNotInDB;
        set => _propertyNotInDB = value;
    }

    private int _clientID;
    [ColumnAttribute(Constants.COLUMN_CLIENT_ID_NAME, IsPrimaryKey =true)]
    public int ClientID{
        get => _clientID;
        set => _clientID = value;
    }

    private string _clientFirstName;
    [ColumnAttribute(Constants.COLUMN_CLIENT_FIRSTNAME_NAME)]
    public string ClientFirstName{
        get => _clientFirstName;
        set => _clientFirstName = value;
    }

    private string _clientLastName;
    [ColumnAttribute(Constants.COLUMN_CLIENT_LASTNAME_NAME)]
    public string ClientLastName{
        get => _clientLastName;
        set => _clientLastName = value;
    }
}
}