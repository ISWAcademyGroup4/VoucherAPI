using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoucherAPILibrary.Utils
{
    public enum ErrorStatusCode
    {

        [DefaultValue("")]
        [Description("")]
        resource_not_found,

        [DefaultValue("")]
        [Description("")]
        voucher_not_active,

        [DefaultValue("")]
        [Description("")]
        voucher_expired,

        [DefaultValue("")]
        [Description("")]
        voucher_disabled,

        [DefaultValue("")]
        [Description("")]
        quantity_exceeded,

        [DefaultValue("")]
        [Description("")]
        gift_amount_exceeded,

        [DefaultValue("")]
        [Description("")]
        invalid_amount,

        [DefaultValue("")]
        [Description("")]
        missing_amount,


        [DefaultValue("")]
        [Description("")]
        missing_customer,


        [DefaultValue("")]
        [Description("")]
        invalid_voucher,


        [DefaultValue("")]
        [Description("")]
        invalid_gift,


        [DefaultValue("")]
        [Description("")]
        invalid_add_balance_params,


        [DefaultValue("")]
        [Description("")]
        invalid_campaign_params,


        [DefaultValue("")]
        [Description("")]
        invalid_code_config,


        [DefaultValue("")]
        [Description("")]
        invalid_customer,


        [DefaultValue("")]
        [Description("")]
        invalid_export_params,


        [DefaultValue("")]
        [Description("")]
        invalid_query_params

    }
}
