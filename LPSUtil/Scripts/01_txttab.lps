function okresy()
{
    cd_uir(); 
    return LoadTxtTable('okresy.txt');
};

function UrciTyp(prevtyp, sample)
{
    if(sample is null)
        return prevtyp;
    // typ 0 = neurcen
    // typ 1 = integer
    // typ 2 = decimal
    // typ 3 = string
    var typ = 0;
    foreach(ch in sample.ToString().Trim())
    {
        if(ch in <cast "0" as System.Char, cast "9" as System.Char>)
        {
            if(typ < 1) typ = 1;
        }
        else if(ch == "." or ch == ",")
        {
            if(typ < 2) typ = 2;
        }
        else
        {
            typ = 100 + sample.Length;
            break;
        }
    }
    return (typ > prevtyp) ? typ : prevtyp;
};

function LoadTxtTable(filename)
{
    using(var reader = new System.IO.StreamReader(filename))
    {
        var cols = new System.Collections.Hashtable();
        var rows = new System.Collections.ArrayList();
        var col_id = 0;
        while((var line = reader.ReadLine()) not null)
        {
            if(line == "")
            {
                if(cols != null and cols.Count != 0) 
                {
                    rows.Add(cols);
                    cols = null;
                }
                cols = new System.Collections.Hashtable();
                col_id = 0;
                continue;
            }
            var bits = line.Split([cast ':' as System.Char]);
            cols[++col_id] = [bits[0].Trim(), bits[1].Trim()];
        }
        if(cols != null and cols.Count != 0) rows.Add(cols);
        return rows.ToArray();
    }
};

function AnalyzeColumns(arrayOfHash)
{
    var columns = new System.Collections.Hashtable();
    foreach(datarow in arrayOfHash)
    {
        foreach(keyval in datarow)
        {
            if(keyval.Value != null and (var val = keyval.Value[1].ToString().Trim()) != "")
            {
                var colinfo = columns[keyval.Key];
                if(colinfo == null)
                    colinfo = [keyval.Value[0], 0, 0, 0, new System.Collections.ArrayList()];
                colinfo[1]++;
                colinfo[3] = UrciTyp(colinfo[3], val);
                if(colinfo[4].IndexOf(val) < 0)
                {
                    colinfo[4].Add(val);
                    colinfo[2]++;
                }
                columns[keyval.Key] = colinfo;
            }
        }
    }
    return columns;
};

function AnalyzeTxtTable(filename)
{
    var cols = AnalyzeColumns(LoadTxtTable(filename));
    var result = new System.Collections.ArrayList();
    foreach(col in cols)
    {
        var typ = col.Value[3];
        if(typ == 0) typ = "null";
        else if(typ == 1) typ = "integer";
        else if(typ == 2) typ = "numeric";
        else if(typ > 100) typ = "varchar(" + (typ - 100) + ")";
        //result.Add("{0}:\tNenulových {1}\tUnikátních {2}\tTyp: {3}\tSloupec '{4}'" /
        //    [col.Key, col.Value[1], col.Value[2], typ, col.Value[0]]);
        result.Add("\t{0,-15}\t{1,-20} //{2, 3}: Nenulových {3}\tUnikátních {4}" /
            [col.Value[0], typ+",", col.Key, col.Value[1], col.Value[2]]);
    }
    return result.ToArray();
};


function AnalyzeAll()
{
    foreach(filename in dir("*.txt"))
    {
        using(var writer = writefile(filename + ".analyzed"))
        {
            foreach(line in AnalyzeTxtTable(filename))
            {
                writer.WriteLine(line.ToString());
            }
            writer.Close();
        }
    }
}



