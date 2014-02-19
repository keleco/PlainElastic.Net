using PlainElastic.Net.Utils;

namespace PlainElastic.Net
{
    public class MultiSearchCommand : CommandBuilder<MultiSearchCommand>
    {
        public string Index { get; private set; }

        public string Type { get; private set; }
        
        public MultiSearchCommand(string index = null, string type = null)
        {
            Index = index;
            Type = type;
        }

        protected override string BuildUrlPath()
        {
            return UrlBuilder.BuildUrlPath(Index, Type, "_msearch");
        }
    }
}
