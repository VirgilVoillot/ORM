using ORM;
namespace ORM_TST
{

[TableAttribute(Constants.TABLE_NAME)]
public class NoUpdateEntity : Entity {
        

    private int _clientID;
    [ColumnAttribute(Constants.COLUMN_CLIENT_ID_NAME, IsPrimaryKey =true)]
    public int ClientID{
        get => _clientID;
        set => _clientID = value;
    }

    private string _clientFirstName;
    [ColumnAttribute(Constants.COLUMN_CLIENT_FIRSTNAME_NAME, PreventUpdate = true)]
    public string ClientFirstName{
        get => _clientFirstName;
        set => _clientFirstName = value;
    }

    private string _clientLastName;
    [ColumnAttribute(Constants.COLUMN_CLIENT_LASTNAME_NAME, PreventUpdate = true)]
    public string ClientLastName{
        get => _clientLastName;
        set => _clientLastName = value;
    }
}
}