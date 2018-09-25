using EFCoreJsonConvert.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFCoreJsonConvert.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {

            // This Converter will perform the conversion to and from Json to the desired type
            builder.Property(e => e.Addresses).HasConversion(
                v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                v => JsonConvert.DeserializeObject<IList<Address>>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }));


            builder.Property(p => p.Device).HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Device>(v));
        }
    }
}
