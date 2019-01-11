using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Services
{
    public class DbConfigService
    {
        private static IConfiguration Configuration;

        public DbConfigService(IConfiguration configuration) => Configuration = configuration;

        public static IDbConnection Connection
        {
            get
            {
                return new SqlConnection("Data Source=ENUNWAH-PC\\SQLEXPRESS;Initial Catalog=VoucherzDb;User ID=sa;Password=welcome12@");
            }
        }
    }
}
