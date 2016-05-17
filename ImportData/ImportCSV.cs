﻿using System;
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
        public static string importTable(string prefix, string filePath)
        {
            string tmp,tmpCol,tmpColType;
            string tableName = "", tableType = "";
            string columns = "", columnsCreate="";
            string data = "";
            string fullTableName = prefix + "_";
            bool startData = false, onlyData = false, dropOld=false;


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
                            fullTableName = fullTableName + tableType + "_" + tableName;
                        }
                        else if (tmp == "ONLYDATA")
                            onlyData = true;
                        else if (tmp == "DROP")
                            dropOld = true;
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
                        }
                        else if (tmp == "DATA")
                        {
                            startData = true;
                            if (onlyData == false)
                                tryCreateTable(prefix, tableName, tableType, columnsCreate, dropOld);

                        }
                    }
                    else
                    {
                        data = "";
                        for (int i = 0; i < fields.Length - 1; i++)
                            data = data + "'"+ fields[i] + "',";

                        data = data.Remove(data.Length - 1);
                        Console.WriteLine(data);
                        Connection.insert_row(fullTableName, columns, data);
                    }
                    
                }
            }
            return "Import status: success";
        }

        private static bool tryCreateTable(string prefix, string tableName, string tableType, string columns, bool drop)
        {
            if (tableName != "" && tableType != "" && columns != "")
            {
                if (tableType == "DIMENSION")
                {
                    if (drop == true)
                        DimensionHandler.dropDimension(prefix, tableName);
                    DimensionHandler.addDimension(tableName, columns, prefix);
                }
                else
                {
                    if (drop == true)
                        FactHandler.dropFact(prefix, tableName);
                    FactHandler.addFact(tableName, columns, prefix);
                }
                return true;
            }
            return false;
        }
    }
}