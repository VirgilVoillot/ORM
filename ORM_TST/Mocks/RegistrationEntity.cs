using ORM;

namespace ORM_TST
{
[TableAttribute(Constants.TABLE_REGISTRATION_NAME)]
public class RegistrationEntity : Entity {
        
    private int _clientID;
    [ColumnAttribute(Constants.COLUMN_CLIENT_ID_NAME)]
    public int ClientID{
        get => _clientID;
        set => _clientID = value;
    }

    private bool _isRegistered;
    [ColumnAttribute(Constants.COLUMN_IS_REGISTERD_NAME, IncludeDefaultValueInResearch = true)]
    public bool IsRegistered{
        get => _isRegistered;
        set => _isRegistered = value;
    }

}
}