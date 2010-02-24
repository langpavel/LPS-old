function okresy()
{
    cd_uir(); 
    result = LoadTxtTable('okresy.txt');
    echo(result);
};

function LoadTxtTable(filename)
{
    using(var reader = new System.IO.StreamReader(filename))
    {
        var cols = new System.Collections.Hashtable();
        var rows = new System.Collections.ArrayList();
        while((var line = reader.ReadLine()) not null)
        {
            if(line == "")
            {
                rows.Add(cols);
                echo(cols);
                cols = new System.Collections.Hashtable();
                continue;
            }
            var bits = line.Split([cast ':' as System.Char]);
            cols[bits[0].Trim()] = bits[1].Trim();
        }
        rows.Add(cols);
        return rows.ToArray();
    }
};

