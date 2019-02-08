namespace VoucherAPILibrary.Security
{
    public class User
    {
        public int Id { get; internal set; }
        internal string Token;
        public string Username { get; internal set; }
        public string Password { get; internal set; }       
        public string Firstname { get; internal set; }
        public string Lastname { get; internal set; }
    }
}