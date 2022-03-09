namespace DataAccessLibrary.Models
{
    public interface ITransaction
    {
        string account_id { get; set; }
        string account_name { get; set; }
        int amount { get; set; }
        bool approved { get; set; }
        string category_id { get; set; }
        string category_name { get; set; }
        string cleared { get; set; }
        string date { get; set; }
        bool deleted { get; set; }
        string flag_color { get; set; }
        string id { get; set; }
        string import_id { get; set; }
        string matched_transaction_id { get; set; }
        string memo { get; set; }
        string payee_id { get; set; }
        string payee_name { get; set; }
        Subtransaction[] subtransactions { get; set; }
        string transfer_account_id { get; set; }
        string transfer_transaction_id { get; set; }
    }
}