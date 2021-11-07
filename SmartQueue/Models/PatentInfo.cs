using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Web.Mvc;
using System.Collections.Generic;

namespace PatentModel
{
    public class patentInfo 
    {
        List<string> itemList = new List<string>();
        static int tableSize = getTableCount();
        static string[,] value = new string[tableSize,5]; // a multiDimensional array for retrieving items from table

        /*There are 3 ways of ASP.NET Modelling API's such as;
         * low-level Api
         * Document model Api
         * Object Persistance Model Api
         * 
         * Currently, I am doing the Document model that is great for doing operations on tables with 
         * the most funcionality that is as complex as the low-level Api.
         */

        public static string[,] readTable() //This way is to use the Document model for CRUD operations.
        {
            int tableCursorCounter = 0; //starts at the beginning of the table.
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "PatentInfo");
            int ArrayCounter = 0;

            GetItemOperationConfig config = new GetItemOperationConfig
            {
                AttributesToGet = new List<string> { "PatentID", "FirstName", "LastName", }, // attributes/columns to retrieve
                ConsistentRead = true
            };

            while(tableCursorCounter < tableSize) // while loop that gets all the values from the table
            {
                tableCursorCounter++;
                string retrieveItemKey = tableCursorCounter.ToString(); //Convert tableCursorCounter to a string
                Document document = table.GetItem(retrieveItemKey, retrieveItemKey);

                value[ArrayCounter, 0] = document["PatentID"].ToString();
                value[ArrayCounter, 1] = document["NameID"].ToString();
                value[ArrayCounter, 2] = document["FirstName"].ToString();
                value[ArrayCounter, 3] = document["LastName"].ToString();
                value[ArrayCounter, 4] = document["QueueStatus"].ToString();

                ArrayCounter++;
            }
            
            return value; // return a multidimensional array
        }

        public static void insertValues(string FirstName, string LastName) // insert a new row into the table
        {
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "PatentInfo");
            var item = new Document();
            var tableNumber = getTableCount();
            tableNumber++;
            string tableCount = tableNumber.ToString();

            item["PatentID"] = tableCount;
            item["NameID"] = tableCount;
            item["FirstName"] = FirstName;
            item["LastName"] = LastName;
            item["QueueStatus"] = "false";

            table.PutItem(item); //insert a new row into the table.
        }

        public static void updateRow(string FirstName, string LastName) //updating existing row.
        {
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "PatentInfo");
            string[,] tableData = readTable();
            var resultID = returnIdFromTable(tableData, FirstName);

            var item = table.GetItem(resultID, resultID);
            item["PatentID"] = resultID;
            item["NameID"] = resultID;
            item["FirstName"] = FirstName;
            item["LastName"] = LastName;

            table.UpdateItem(item, resultID, resultID); //specify the primary and hash key to update the row.

        }

        public static void deleteRow(string FirstName, string LastName)
        {
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "PatentInfo");
            string[,] tableData = readTable();
            var resultID = returnIdFromTable(tableData, FirstName);
            var item = table.DeleteItem(resultID, resultID); //specify which row to delete based on primary and hash key.
        }
        
        public static int getTableCount()
        {
            int itemCount = 0;
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "PatentInfo");
            //var itemCount = table.GetItemAsync("2");

            var request = new ScanRequest
            {
                TableName = "PatentInfo"
            };

            var response = client.Scan(request);
            var result = response.ScannedCount;

            foreach (Dictionary<string, AttributeValue> item in response.Items)
            {
                itemCount = itemCount + 1;
            }
            return itemCount;
        }

        public static string returnIdFromTable(string[,] table, string searchFirstNameQuery)
        {
            var iDCounter = 1; //database id starts from 1
            var attributeCounter = 0;
            var tableCount = getTableCount();

            while(iDCounter <= tableCount)
            {
                while(attributeCounter < 5) //there are only 5 attributes or columns in the database
                {
                    if(table[iDCounter, attributeCounter] == searchFirstNameQuery)
                    {
                        return table[iDCounter, 0]; // 0 is the patentiD column in the db
                    }   
                }
            }
            return "";
        }

        public static string returnNameFromTable(string[,] table, string searchFirstNameQuery)
        {
            var iDCounter = 1; //iD in database starts from 1
            var attributeCounter = 0;
            var tableCount = getTableCount();

            while (iDCounter <= tableCount)
            {
                while (attributeCounter < 5) //there are only 5 attributes or columns in the database
                {
                    if (table[iDCounter, attributeCounter] == searchFirstNameQuery)
                    {
                        return table[iDCounter, 1]; // 1 is the firstName column in the db
                    }
                }
            }
            return "";
        }

    }
}
