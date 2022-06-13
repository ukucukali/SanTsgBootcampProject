using System.Collections.Generic;
using static SanTsgBootcampProject.Domain.ApiResponseModels.ProductDetailsResult;

namespace SanTsgBootcampProject.Domain.ApiResponseModels
{
    public class ProductDetailsResult : BaseEntity<ProductDetails>
    {
        /// <summary>
        /// this model helps us display detailed hotel information
        /// </summary>
        public class ProductDetails
        {
            public Hotel Hotel { get; set; }
        }
        public class Hotel
        {
            public List<Season> Seasons { get; set; }
            public Address Address { get; set; }
            public string FaxNumber { get; set; }
            public string PhoneNumber { get; set; }
            public string HomePage { get; set; }
            public float Stars { get; set; }
            public double Rating { get; set; }
            public City City { get; set; }
            public Description Description { get; set; }
            public string Id { get; set; }
            public string Name { get; set; }

        }
        public class Season
        {
            public List<TextCategory> TextCategories { get; set; } = new List<TextCategory>();
            public List<MediaFile> MediaFiles { get; set; } = new List<MediaFile>();
        }
        public class TextCategory
        {
            public string Name { get; set; }
            public List<Presentation> Presentations { get; set; }
        }

        public class Presentation
        {
            public string Text { get; set; }
        }

        public class Address
        {
            public List<string> AddressLines { get; set; }
            public string Street { get; set; }
            public string ZipCode { get; set; }
        }

        public class City
        {
            public string Name { get; set; }
            public string Id { get; set; }
        }

        public class Description
        {
            public string Text { get; set; }
        }

        public class MediaFile
        {
            public string Url { get; set; }
            public string UrlFull { get; set; }
        }



    }
}
