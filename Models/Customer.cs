using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pedalacom.Models;

/// <summary>
/// Customer information.
/// </summary>
public partial class Customer
{
    /// <summary>
    /// Primary key for Customer records.
    /// </summary>
    [Key]
    [Required]
    public int CustomerId { get; set; }

    /// <summary>
    /// 0 = The data in FirstName and LastName are stored in western style (first name, last name) order.  1 = Eastern style (last name, first name) order.
    /// </summary>
    /// 
    [Required]
    public bool NameStyle { get; set; }

    /// <summary>
    /// A courtesy title. For example, Mr. or Ms.
    /// </summary>
    [MaxLength(8, ErrorMessage = "Il campo non può avere più di 8 caratteri")]
    public string? Title { get; set; }

    /// <summary>
    /// First name of the person.
    /// </summary>
    /// 
    [Required]
    [MinLength(2, ErrorMessage = "Il nome non può avere meno di 2 caratteri")]
    [MaxLength(50, ErrorMessage = "Il nome non può avere più di 50 caratteri")]

    public string FirstName { get; set; } = null!;

    /// <summary>
    /// Middle name or middle initial of the person.
    /// </summary>
    /// 
    [MaxLength(50, ErrorMessage = "Il nome non può avere più di 50 caratteri")]
    public string? MiddleName { get; set; }

    /// <summary>
    /// Last name of the person.
    /// </summary>
    /// 
    [Required]
    [MinLength(2, ErrorMessage = "Il cognome non può avere meno di 2 caratteri")]
    [MaxLength(50, ErrorMessage = "Il cognome non può avere più di 50 caratteri")]
    public string LastName { get; set; } = null!;

    /// <summary>
    /// Surname suffix. For example, Sr. or Jr.
    /// </summary>
    public string? Suffix { get; set; }

    /// <summary>
    /// The customer&apos;s organization.
    /// </summary>
    
    [MinLength(2, ErrorMessage = "La azienda non può avere meno di 2 caratteri")]
    [MaxLength(50, ErrorMessage = "La azienda non può avere più di 50 caratteri")]
    public string? CompanyName { get; set; }

    /// <summary>
    /// The customer&apos;s sales person, an employee of AdventureWorks Cycles.
    /// </summary>
    public string? SalesPerson { get; set; }

    /// <summary>
    /// E-mail address for the person.
    /// </summary>
    /// 
    [Required]
    [EmailAddress(ErrorMessage = "Errore di formato")]
    [MaxLength(50, ErrorMessage = "L'email non può avere più di 50 caratteri")]
    public string? EmailAddress { get; set; }

    /// <summary>
    /// Phone number associated with the person.
    /// </summary>
    /// 
    [MaxLength(25, ErrorMessage = "Il numero di telefono non può avere più di 25 caratteri")]
    public string? Phone { get; set; }

    /// <summary>
    /// Password for the e-mail account.
    /// </summary>
    /// 
    [Required]
    public string PasswordHash { get; set; } = null!;

    /// <summary>
    /// Random value concatenated with the password string before the password is hashed.
    /// </summary>
    /// 
    [Required]
    public string PasswordSalt { get; set; } = null!;

    /// <summary>
    /// ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
    /// </summary>
    /// 

    public Guid Rowguid { get; set; }

    /// <summary>
    /// Date and time the record was last updated.
    /// </summary>
    /// 
    
    public string? tmpPassword { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public int? IsOld { get; set; }

    public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();

    public virtual ICollection<SalesOrderHeader> SalesOrderHeaders { get; set; } = new List<SalesOrderHeader>();
}
