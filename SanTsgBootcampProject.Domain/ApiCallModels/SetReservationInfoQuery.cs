using System.Collections.Generic;

namespace SanTsgBootcampProject.Domain.ApiCallModels
{
    /// <summary>
    /// to set resarvation details with travellers information included
    /// </summary>
    public class SetReservationInfoQuery
    {
        public string TransactionId { get; set; }
        public List<Traveller> Travellers { get; set; }
        public CustomerInfo CustomerInfo { get; set; }
        public string ReservationNote { get; set; } = "Reservation note";
        public string AgencyReservationNumber { get; set; } = "Agency reservation number text";
    }
    public class Traveller
    {
        public string TravellerId { get; set; }
        public int Type { get; set; } = 1;
        public int Title { get; set; } = 1;
        public int PassengerType { get; set; } = 1;
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsLeader { get; set; } = false;
        public string BirthDate { get; set; } = "1995-06-08";
        public Nationality Nationality { get; set; }
        public string IdentityNumber { get; set; } = "1111111111";
        public PassportInfo PassportInfo { get; set; }
        public Addresss Address { get; set; }
        public DestinationAddress DestinationAddress { get; set; }
        public int OrderNumber { get; set; } = 1;
        public List<object> Documents { get; set; }
        public List<object> InsertFields { get; set; }
        public int Status { get; set; } = 0;
    }


    public class PassportInfo
    {
        public string Serial { get; set; } = "a";
        public string Number { get; set; } = "13";
        public string ExpireDate { get; set; } = "2030-01-01T00:00:00";
        public string IssueDate { get; set; } = "2020-01-01T00:00:00";
        public string CitizenshipCountryCode { get; set; } = string.Empty;
    }
    public class Addresss
    {
        public ContactPhone ContactPhone { get; set; }
        public string Email { get; set; } = "email@test.com";
        public string Address { get; set; } = "deneme";
        public string ZipCode { get; set; } = "07100";
        public City City { get; set; }
        public Country Country { get; set; }
        public string Phone { get; set; } = "+902222222222";
    }

    public class City
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class ContactPhone
    {
        public string CountryCode { get; set; } = "90";
        public string AreaCode { get; set; } = "555";
        public string PhoneNumber { get; set; } = "1112233";
    }

    public class Country
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class CustomerInfo
    {
        public bool IsCompany { get; set; } = false;
        public PassportInfo PassportInfo { get; set; }
        public Addresss Address { get; set; }
        public TaxInfoo TaxInfo { get; set; }
        public int Title { get; set; } = 1;
        public string Name { get; set; } = "Customer Name";
        public string Surname { get; set; } = "Customer Surname";
        public string BirthDate { get; set; } = "1996-01-01";
        public string IdentityNumber { get; set; } = "11111111111";
    }

    public class DestinationAddress
    {
        public string Address { get; set; } = "destinationAddress";
    }

    public class Nationality
    {
        public string TwoLetterCode { get; set; } = "DE";
    }


    public class TaxInfoo
    {
        public string TaxInfo { get; set; } = "TaxInfo";
    }


}
