using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using System;

namespace PatentModel
{
    public class patentInfo 
    {
        public static string value;
        /*There are 3 ways of ASP.NET Modelling API's such as;
         * low-level Api
         * Document model Api
         * Object Persistance Model Api
         */
        
        public static void readTable() //This way is to use the Document model for CRUD operations.
        {
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "PatentInfo");
            //var item = table.GetItem("01", "01"); // use primary and hash key to locate row. In my case, both my keys are string and thats why I input "01".
            var item = table.GetItem("1", "1");
            value = item["PatentID"]; //the 
        }
        
        public static void insertValues()
        {
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "PatentInfo");
            var item = new Document();

            item["PatentID"] = "3";
            item["NameID"] = "3";
            item["FirstName"] = "kevin";
            item["LastName"] = "Junior";

            table.PutItem(item); //insert a new row into the table.
        }

        public static void updateRow()
        {
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "PatentInfo");
            var item = table.GetItem("01", "01");
            item["PatentID"] = "1";
            item["NameID"] = "1";
            item["FirstName"] = "Adam";
            item["LastName"] = "Smtih";

            table.UpdateItem(item,"1", "1"); //specify the primary and hash key to update the row.

        }

        public static void deleteRow()
        {
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "PatentInfo");
            var item = table.DeleteItem("2", "2"); //specify which row to delete based on primary and hash key.
        }
        
    }

    
}
