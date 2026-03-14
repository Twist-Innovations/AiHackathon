namespace AiHackathon.ApiService.Validations
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
    public class ModelNameAttribute(string name) : Attribute
    {
        public string Name { get; } = name;
    }
}
