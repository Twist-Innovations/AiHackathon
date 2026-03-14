using System.Text;

namespace AiHackathon.ApiService.Validations
{
    public sealed class ValidationError
    {
        public Dictionary<string, List<string>> Values { get { return GetErrorsList(); } }

        private readonly List<Errors> _errorList = new List<Errors>();
        public bool HasError => !IsEmpty;
        public bool IsEmpty => _errorList.Count == 0;
        public static ValidationError Empty => new ValidationError();
        public ValidationError() { }
        private ValidationError(string name, string description)
        {
            Add(name, description);
        }
        public static ValidationError Create(string name, string description)
        {
            return new ValidationError(name, description);
        }
        public Errors[] GetErrorsArray()
        {
            return _errorList.ToArray();
        }
        public void AddRange(params Errors[] errors)
        {
            _errorList.AddRange(errors);
        }
        public void Add(string name, string description)
        {
            if(TryFind(name.ToLower(), out var error))
            {
                error.Description.Add(description);
            }
            else
            {
                error.Description.Add(description);
                _errorList.Add(error);
            }
        }
        public void Remove(string name) => TryRemove(name);

        /// <summary>
        /// Returns a single string contain all errors.
        /// </summary>
        /// <returns></returns>
        public string GetStringErrors()
        {
            var sb = new StringBuilder();

            foreach(var error in _errorList)
            {
                sb.Append(string.Format("Name: {0}. Description: {1}.", error.Name, error.Description));
                sb.AppendLine();
            }

            return sb.ToString();
        }
        private bool TryFind(string name, out Errors error)
        {
            var data = _errorList.FirstOrDefault(e => e.Name == name);

            if(data == null)
            {
                error = new(name, []);
                return false;
            }

            error = data;
            return true;
        }
        private bool TryRemove(string name) => TryFind(name, out var error) && _errorList.Remove(error);
        private Dictionary<string, List<string>> GetErrorsList()
        {
            Dictionary<string, List<string>> values = new Dictionary<string, List<string>>();

            foreach(var error in _errorList)
            {
                if(values.TryGetValue(error.Name, out var errors))
                {
                    foreach(var item in error.Description)
                    {
                        values[error.Name].Add(item);
                    }
                }
                else
                {
                    values.Add(error.Name, [.. error.Description]);
                }
            }

            return values;
        }
        public override string ToString()
        {
            return GetStringErrors();
        }
    }

    public record class Errors(string Name, HashSet<string> Description);
}
