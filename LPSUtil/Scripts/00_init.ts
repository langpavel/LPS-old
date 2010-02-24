var ServerConnection;
var result = null;

function std_login()
{
    return ServerConnection = login("http://localhost/LPS/Server.asmx", "langpa", "");
};

cd('../../../LPSServer/resources/tables');

function cd_uir()
{
    cd('/home/plang/Projects/LPSoft/LPSServer/import/uir');
};


std_login();

