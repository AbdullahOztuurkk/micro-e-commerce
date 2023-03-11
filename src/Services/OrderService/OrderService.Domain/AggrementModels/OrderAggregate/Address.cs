namespace OrderService.Domain.AggrementModels.OrderAggregate
{
    //Record ile referans tip oluşturulacağı için başka adresler birbirine eşit mi diye kontrol sağlanabilir.
    public record Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

        public Address(string street, string city, string state, string country, string zipCode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public Address()
        {

        }
    }
}
