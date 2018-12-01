# Przykład konwertera EF Core 2.1

Zaprezentowany konwerter zapisuje obiekt do bazy danych w postaci json.

## Konwersja za pomocą wyrażeń lambda

~~~ csharp
builder.Property(p=>p.Gender)
  .HasConversion(
      v => v.ToString(),
      v => (Gender)Enum.Parse(typeof(Gender), v)
);
~~~

## Konwersja za pomocą obiektu konwertera
~~~ csharp            
var converter = new ValueConverter<Gender, string>(
v => v.ToString(),
v => (Gender)Enum.Parse(typeof(Gender), v));
~~~

## Użycie wbudowanego konwertera
   
~~~ csharp              
            var converter = new EnumToStringConverter<Gender>();
            builder.Property(p=>p.Gender)
              .HasConversion(converter);
~~~

Niektóre z konwerterów posiadają dodatkowe parametry:
~~~ csharp              
            builder.Property(p=>p.IsDeleted)
                .HasConversion(new BoolToStringConverter("Y", "N"));
~~~

Lista wbudowanych konwerterów [https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions]

                

## Predefiniowane konwersje
W większości przypadków nie trzeba tworzyć konwerterów, bo wystarczy skorzystać z predefiniowanych konwersji:
~~~ csharp 
builder.Property(p=>p.Gender)
                .HasConversion<string>();
~~~                

            
## Własny konwerter

### Własny konwerter za pomocą wyrażenia lambda

~~~ csharp 
  builder.Property(p => p.ShippingAddress).HasConversion(
    v => JsonConvert.SerializeObject(v),
    v => JsonConvert.DeserializeObject<Address>(v));
~~~
   
### Utworzenie własnego konwertera

Utworzenie klasy własnego konwertera
~~~ csharp 
public class JsonValueConverter<T> : ValueConverter<T, string>
    {
        public JsonValueConverter(ConverterMappingHints mappingHints = null)
        : base(v => JsonConvert.SerializeObject(v), 
               v => JsonConvert.DeserializeObject<T>(v), 
               mappingHints)
        {
        }      
    }
~~~

Użycie własnego konwertera
~~~ csharp 
builder.Property(p => p.ShippingAddress).HasConversion(new JsonValueConverter<Address>());
~~~

W celu ułatwienia korzystania z konwertera można utworzyć metodę rozszerzającą

~~~ csharp 
public static class PropertyBuilderExtensions
    {
        public static PropertyBuilder<T> HasJsonValueConversion<T>(this PropertyBuilder<T> propertyBuilder) where T : class
        {
            propertyBuilder
              .HasConversion(new JsonValueConverter<T>());

            return propertyBuilder;
        }
    }
    
~~~

A następnie użyć jej podczas konfiguracji
~~~ csharp 
            builder.Property(p => p.ShippingAddress)
                .HasJsonValueConversion();
~~~

## Dokumentacja
- https://stackoverflow.com/questions/50727860/ef-core-2-1-hasconversion-on-all-properties-of-type-datetime
- https://docs.microsoft.com/en-us/ef/core/modeling/value-conversions

## Biblioteki
https://github.com/Innofactor/EfCoreJsonValueConverter
