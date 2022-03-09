using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{

    public class Rootobject
    {
        public Data data { get; set; }
    }

    public class Data
    {
        public Transaction[] transactions { get; set; }
        public int server_knowledge { get; set; }
    }

    public class Transaction : ITransaction
    {
        public string id { get; set; }
        public string date { get; set; }
        public int amount { get; set; }
        public string memo { get; set; }
        public string cleared { get; set; }
        public bool approved { get; set; }
        public string flag_color { get; set; }
        public string account_id { get; set; }
        public string payee_id { get; set; }
        public string category_id { get; set; }
        public string transfer_account_id { get; set; }
        public string transfer_transaction_id { get; set; }
        public string matched_transaction_id { get; set; }
        public string import_id { get; set; }
        public bool deleted { get; set; }
        public string account_name { get; set; }
        public string payee_name { get; set; }
        public string category_name { get; set; }
        public Subtransaction[] subtransactions { get; set; }
    }

    public class Subtransaction
    {
        public string id { get; set; }
        public string transaction_id { get; set; }
        public int amount { get; set; }
        public string memo { get; set; }
        public string payee_id { get; set; }
        public string payee_name { get; set; }
        public string category_id { get; set; }
        public string category_name { get; set; }
        public string transfer_account_id { get; set; }
        public string transfer_transaction_id { get; set; }
        public bool deleted { get; set; }
    }

}