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
    /// <summary>
    /// This class contain all method required to import data to database
    /// </summary>
    public static class ImportCSV
    {
        /// <summary>
        /// Method import data from file 
        /// </summary>
        /// <remarks>
        /// Method can create table or drop table or do both before improt data. When method gets 'DATA' mark all previous marks are gather and table can create table. Then method starts import data.
        /// </remarks>
        /// <param name="prefix">Project prefix</param>
        /// <param name="filePath">File path to .csv file</param>
        /// <returns>Return string with import status message</returns>
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
                            string result= "OK";
                            if (onlyData == false)
                                result = tryCreateTable(prefix, tableName, tableType, columnsCreate, dropOld);
                            if (result != "OK")
                                return result;
                        }
                    }
                    else
                    {
                        data = "";
                        for (int i = 0; i < fields.Length - 1; i++)
                            data = data + "'"+ fields[i] + "',";

                        data = data.Remove(data.Length - 1);
                        Console.WriteLine(data);
                        Result result;
                        result = Connection.insert_row(fullTableName, columns, data);
                        if (result.errormsg != "OK")
                            return "Import status: Problem with insert. Import abort";
                        
                    }
                    
                }
            }
            return "Import status: success";
        }

        /// <summary>
        /// Method itry to drop and create table 
        /// </summary>
        /// <param name="prefix">Project prefix</param>
        /// <param name="tableName">Table name</param>
        /// <param name="tableType">New columns types</param>
        /// <param name="columns">New table columns</param>
        /// <param name="drop">If true table with the same name will be drop</param>
        private static string tryCreateTable(string prefix, string tableName, string tableType, string columns, bool drop)
        {
            if (tableName != "" && tableType != "" && columns != "")
            {
                Result result;

                if (tableType == "DIMENSION")
                {
                    if (drop == true)
                        DimensionHandler.dropDimension(prefix, tableName);
                    result = DimensionHandler.addDimension(tableName, columns, prefix);
                    if (result.errormsg != "OK")
                        return "Import status: Cannot create table. Please check if old table contains foreign keys or if all required inforamtion are correct.";
                    return "OK";
                }
                else
                {
                    if (drop == true)
                        FactHandler.dropFact(prefix, tableName);
                    result = FactHandler.addFact(tableName, columns, prefix);
                    if (result.errormsg != "OK")
                        return "Import status: Cannot create table. Please check if old table exists or contains foreign keys or if all required inforamtion are correct.";
                    return "OK";
                }
                if (tableType == "FUNCTION")
                {
                    if (drop == true)
                        FunctionHandler.dropFunction(prefix + "_" + tableType + "_" + tableName);
                    result = FunctionHandler.addFunction(tableName, columns, prefix);
                    if (result.errormsg != "OK")
                        return "Import status: Cannot create table. Please check if old table exists or contains foreign keys or if all required inforamtion are correct.";
                    return "OK";
                }
            }
            return "Import status: Please check if csv contains all required information - table type, table name and columns name with types";
        }
    }
}
