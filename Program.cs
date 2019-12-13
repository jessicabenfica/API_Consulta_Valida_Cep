using System;
using RestSharp;
using Xunit;
using FluentAssertions;
using Correios.Models;
using Newtonsoft.Json;
using NUnit.Framework.Internal;

namespace Correios
{
    public class Program
    {
        private IRestResponse GetCEP(object CEP)
        {
            var client = new RestClient("https://viacep.com.br/ws/" + CEP + "/json/");
         // var client = new RestClient("https://viacep.com.br/ws/01001000/json");
            var RSrequest = new RestRequest(Method.GET) { RequestFormat = DataFormat.Json };
            return client.Execute(RSrequest);
        }

       /* [Fact]
        public void CasoSucesso_1()
        {
            var response = GetCEP("01001000");
            response.StatusCode.Should().Be(200);
        } */

        [Fact]
        public void CasoSucesso_2()
        {
            var response = GetCEP("01001000");
            var json = JsonConvert.DeserializeObject<CorreiosResponse>(response.Content); // Deserealizar o conteúdo do response na variável “json”.

            //validação dos campos 
            json.Cep.Should().Be("01001-000");
            json.Logradouro.Should().Be("Praça da Sé");
            json.Complemento.Should().Be("lado ímpar");
            json.Bairro.Should().Be("Sé");
            json.Localidade.Should().Be("São Paulo");
            json.UF.Should().Be("SP");
            json.Unidade.Should().Be("");
            json.Ibge.Should().Be("3550308");
            json.Gia.Should().Be("1004");

            response.StatusCode.Should().Be(200); // code = 200 - Dados enviados com sucesso. 
           
            
       
        }
    }
}
