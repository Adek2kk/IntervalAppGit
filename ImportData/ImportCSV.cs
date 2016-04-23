using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;

using ConnDBlib;

namespace ImportData
{
    public static class ImportCSV
    {
        public static void importTable(string prefix, string filePath)
        {
            string tmp,tmpCol,tmpColType;
            string tableName = "", tableType = "";
            string columns = "", columnsCreate="";
            bool startData = false;

            using (var parser = new TextFieldParser(File.OpenRead(filePath)))
            {

                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");
                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields();
                    tmp = fields[0].ToUpper();

                    if(startData == false)
                    {
                        if (tmp == "TABLE")
                        {
                            tableType = fields[2].ToUpper();
                            tableName = fields[1].ToUpper();
                            if(columnsCreate != "")
                            {
                                if (tableType == "DIMENSION")
                                    DimensionHandler.addDimension(tableName, columnsCreate, prefix);
                                else
                                    FactHandler.addFact(tableName, columnsCreate, prefix);
                            }
                        }
                        else if (tmp == "COLUMNS")
                        {
                            for (int i = 1; i < fields.Length - 1; i++)
                            {
                                tmpColType = fields[i].Substring(fields[i].IndexOf('|') + 1);
                                tmpCol = fields[i].Substring(0, fields[i].IndexOf('|'));
                                columns = columns + tmpCol + ",";
                                columnsCreate = columnsCreate + tmpCol + " " + tmpColType + ",";
                            }

                            columns = columns.Remove(columns.Length - 1);
                            columnsCreate = columnsCreate.Remove(columnsCreate.Length - 1);

                            if (tableName != "" && tableType != "")
                            {
                                if (tableType == "DIMENSION")
                                    DimensionHandler.addDimension(tableName, columnsCreate, prefix);
                                else
                                    FactHandler.addFact(tableName, columnsCreate, prefix);
                            }
                        }
                    }
                    else
                    {
                        //TODO Addding to table lines
                    }
                    
                }
            }
        }
    }
}
