var ServerConnection;

function std_login()
{
    return ServerConnection = login("http://localhost/LPS/Server.asmx", "langpa", "");
};

cd('../../../LPSServer/resources/tables');

std_login();

