//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class DonHang
    {
        public int id { get; set; }
        public int order_id { get; set; }
        public int customer_id { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public string status { get; set; }
        public string voucher { get; set; }
    
        public virtual ChiTietDonHang ChiTietDonHang { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public virtual TrangThaiDonHang TrangThaiDonHang { get; set; }
        public virtual KhuyenMai KhuyenMai { get; set; }
    }
}
