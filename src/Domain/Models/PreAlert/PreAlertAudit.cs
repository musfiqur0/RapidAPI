using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models;

public class PreAlertAudit : Audit
{
    public int Id { get; set; }
    public string Tracking { get; set; }
    public DateTime Date { get; set; }
    public string Customer { get; set; }
    public int ShippingCompanyId { get; set; }
    public int SupplierId { get; set; }
    public string PackageDescription { get; set; }
    public DateTime DeliveryDate { get; set; }
    public decimal PurchasePrice { get; set; }
    public short StatusId { get; set; }
    public bool Active { get; set; }
}