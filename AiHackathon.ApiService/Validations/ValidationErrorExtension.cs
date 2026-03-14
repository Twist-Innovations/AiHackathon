using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace AiHackathon.ApiService.Validations
{
    public static class ValidationErrorExtension
    {
        public static ValidationError CheckEmail<TTarget, TProperty>(
         this ValidationError errors,
         TTarget obj,
         Expression<Func<TTarget, TProperty>> propertyExpression)
        {
            if(propertyExpression.Body is MemberExpression memberExpr &&
                memberExpr.Member is PropertyInfo prop)
            {
                if(prop.PropertyType != typeof(string))
                    return errors;

                var data = prop.GetValue(obj, null);

                if(data == null || !errors.IsValidEmail(data.ToString()))
                {
                    errors.Add(prop.Name.ToLower(), "Invalid email address.");
                }
                return errors;
            }

            return errors;
        }

        public static ValidationError CheckName<TTarget, TProperty>(this ValidationError errors, TTarget obj,
         Expression<Func<TTarget, TProperty>> propertyExpression)
        {
            if(propertyExpression.Body is MemberExpression memberExpr &&
                memberExpr.Member is PropertyInfo prop)
            {
                if(prop.PropertyType != typeof(string))
                    return errors;

                var propName = prop.Name.ToLower();

                var data = prop.GetValue(obj, null);

                var result = GetStringErrors(data!.ToString());

                foreach(var error in result)
                {
                    errors.Add(propName, error);
                }
            }

            return errors;
        }

        public static ValidationError CheckPassword<TTarget, TProperty>(this ValidationError errors, TTarget obj,
        Expression<Func<TTarget, TProperty>> propertyExpression)
        {
            if(propertyExpression.Body is MemberExpression memberExpr &&
                memberExpr.Member is PropertyInfo prop)
            {
                if(prop.PropertyType != typeof(string))
                    return errors;

                var propName = prop.Name.ToLower();

                var data = prop.GetValue(obj, null);

                var result = GetPasswordErrors(data!.ToString());

                foreach(var error in result)
                {
                    errors.Add(propName, error);
                }
            }

            return errors;
        }

        public static ValidationError CheckErrors(this ValidationError errors, object obj)
        {

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

                    if(data == null || !errors.IsValidEmail(data.ToString()))
                    {
                        errors.Add(prop.Name.ToLower(), "Invalid email address.");
                    }

                }
                else if(propName.Contains("password"))
                {
                    if(prop.PropertyType != typeof(string))
                        continue;

                    var data = prop.GetValue(obj, null);

                    if(data == null)
                    {
                        errors.Add(prop.Name.ToLower(), "Entry can not be null.");
                        continue;
                    }

                    var valErrors = GetPasswordErrors(data.ToString());

                    if(valErrors.Count > 0)
                    {
                        foreach(var error in valErrors)
                        {
                            errors.Add(prop.Name.ToLower(), error);
                        }
                    }

                }
                else if(propName.Contains("name"))
                {
                    if(prop.PropertyType != typeof(string))
                        continue;

                    var data = prop.GetValue(obj, null);

                    if(data == null)
                    {
                        errors.Add(prop.Name.ToLower(), "Names can not be null.");
                        continue;
                    }

                    var valErrors = GetStringErrors(data.ToString());

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

        public static bool IsValidEmail(this ValidationError obj, string? email)
        {
            if(string.IsNullOrWhiteSpace(email))
                return false;

            return DataValidator.emailRegex.IsMatch(email);
        }

        public static bool IsValidName(string value)
        {
            return GetStringErrors(value).Count == 0;
        }

        public static bool IsValidID(string id) => !string.IsNullOrEmpty(id);

        private static List<string> GetPasswordErrors(string? value)
        {
            var errors = new List<string>();

            if(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                errors.Add("Passwords can not be empty.");
                return errors;
            }

            if(value.Length < 6)
                errors.Add("Must containt atleast 6 characters.");

            if(value.Any(x => x == ' '))
                errors.Add("Must not contain white space.");

            return errors;
        }
        private static List<string> GetStringErrors(string? value)
        {
            var errors = new List<string>();

            if(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                errors.Add("Entry cannot be left blank.");

            if(errors.Count < 1 && !DataValidator.nameRegex.IsMatch(value!))
                errors.Add("No special characters allowed.");

            return errors;
        }
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
