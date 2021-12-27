using Newtonsoft.Json;

namespace PRFT1
{
    public partial class Produtos
    {
        [JsonProperty("ID")]
        public string ID { get; set; }

        [JsonProperty("NOME")]
        public string Nome { get; set; }

        [JsonProperty("QUANTIDADE")]
        public string Quantidade { get; set; }

        [JsonProperty("MEDIDA")]
        public string Medida { get; set; }

        [JsonProperty("PRECO")]
        public string Preco { get; set; }
    }
}