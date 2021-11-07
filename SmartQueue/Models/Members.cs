using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace authenticateModel
{
    public class Members
    {
        public static bool readUser(string user, string pass)
        {
            var client = new AmazonDynamoDBClient();
            var table = Table.LoadTable(client, "UserDB");
            var item = table.GetItem("1"); // potential problem, how do we search the entire database without
                                           // specifying the primary key?
            if(item["UserName"] == user && item["UserPass"] == pass)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}