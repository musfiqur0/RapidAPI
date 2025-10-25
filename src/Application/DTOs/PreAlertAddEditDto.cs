using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs;

public class PreAlertAddEditDto
{
    public int Id { get; set; } = 0;
    public string Tracking { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Customer { get; set; } = string.Empty;
    public int ShippingCompanyId { get; set; }
    public int SupplierId { get; set; }
    public string PackageDescription { get; set; } = string.Empty;
    public DateTime DeliveryDate { get; set; }
    public decimal PurchasePrice { get; set; }
    public short StatusId { get; set; }
    public bool Active { get; set; }
}