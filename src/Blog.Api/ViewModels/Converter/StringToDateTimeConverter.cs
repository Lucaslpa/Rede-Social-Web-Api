using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blog.api.ViewModels.Converter
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private const string DateFormat = "dd/MM/yyyy";

        public override DateTime Read( ref Utf8JsonReader reader , Type typeToConvert , JsonSerializerOptions options )
        {
            if (reader.TokenType == JsonTokenType.String && DateTime.TryParseExact( reader.GetString() , DateFormat , CultureInfo.InvariantCulture , DateTimeStyles.None , out DateTime parsedDateTime ))
            {
                if (IsValidDateTime( parsedDateTime ))
                {
                    return parsedDateTime;
                }
            }

            throw new ValidationException( "A data fornecida não é válida." );
        }

        public override void Write( Utf8JsonWriter writer , DateTime value , JsonSerializerOptions options )
        {
            writer.WriteStringValue( value.ToString( DateFormat , CultureInfo.InvariantCulture ) );
        }

        private bool IsValidDateTime( DateTime dateTime )
        {
            return dateTime.Year >= 1900 &&
                   dateTime.Month >= 1 && dateTime.Month <= 12 &&
                   dateTime.Day >= 1 && dateTime.Day <= DateTime.DaysInMonth( dateTime.Year , dateTime.Month );
        }
    }
}
