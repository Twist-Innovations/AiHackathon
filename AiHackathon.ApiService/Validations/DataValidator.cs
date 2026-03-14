using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AiHackathon.ApiService.Validations
{
    public static partial class DataValidator
    {
        public static readonly Regex emailRegex = EmailRegex();
        public static readonly Regex nameRegex = NameRegex();

        public static ValidationError CheckErrors(object obj)
        {
            var errors = ValidationError.Empty;

            foreach(PropertyInfo prop in obj.GetType().GetProperties())
            {
                var propName = prop.Name.ToLower();

                if(propName == "id")
                {
                    var data = prop.GetValue(obj, null);

                    if(data is string id)
                    {
                        if(!IsValidID(id))
                        {
                            errors.Add(prop.Name.ToLower(), "Invalid Position.");
                        }
                    }
                }
                else if(propName.Contains("email"))
                {
                    if(prop.PropertyType != typeof(string))
                        continue;

                    var data = prop.GetValue(obj, null);

                    if(data == null || !IsValidEmail(data.ToString()))
                    {
                        errors.Add(prop.Name.ToLower(), "Invalid email address.");
                    }

                }
                else if(propName.Contains("name"))
                {
                    if(prop.PropertyType != typeof(string))
                        continue;

                    var data = prop.GetValue(obj, null);

                    if(data == null)
                    {
                        errors.Add(prop.Name.ToLower(), "Cant not be null.");
                        continue;
                    }

                    var valErrors = GetStringErrors(data.ToString()!);

                    if(valErrors.Count > 0)
                    {
                        foreach(var error in valErrors)
                        {
                            errors.Add(prop.Name.ToLower(), error);
                        }
                    }

                }
            }

            return errors;
        }

        public static bool IsValidName(string value)
        {
            return GetStringErrors(value).Count == 0;
        }

        private static List<string> GetStringErrors(string value)
        {
            var errors = new List<string>();

            if(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                errors.Add("Entry cannot be left blank.");

            if(!nameRegex.IsMatch(value) && !string.IsNullOrEmpty(value))
                errors.Add("No special characters allowed.");

            return errors;
        }

        public static bool IsValidID(string id) => !string.IsNullOrEmpty(id);

        public static bool IsValidPassword(string password)
        {
            var pattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]).+$";
            return Regex.IsMatch(password, pattern);
        }

        public static bool IsValidEmail(string? email)
        {
            if(string.IsNullOrWhiteSpace(email))
                return false;

            return emailRegex.IsMatch(email);
        }

        [GeneratedRegex(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.IgnoreCase | RegexOptions.NonBacktracking | RegexOptions.Compiled,
            "en-US"
        )]
        private static partial Regex EmailRegex();

        [GeneratedRegex(
            @"^[a-zA-Z0-9\s\-']+$",
            RegexOptions.IgnoreCase | RegexOptions.NonBacktracking | RegexOptions.Compiled,
            "en-US"
        )]
        private static partial Regex NameRegex();

        private static string WordCaseConverter(string s)
        {
            int x = 0;
            var result = new StringBuilder();
            foreach(var c in s)
            {
                x++;

                if(x == 1)
                {
                    result.Append(c.ToString().ToUpper());
                }
                else
                {
                    result.Append(c.ToString().ToLower());
                }

                if(c == ' ')
                {
                    x = 0;
                }
            }

            return result.ToString();
        }
    }
}
