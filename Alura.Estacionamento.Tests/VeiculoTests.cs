using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using NuGet.Frameworks;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Alura.Estacionamento.Tests
{
    public class VeiculoTests : IDisposable
    {
        private Veiculo _veiculo;
        private ITestOutputHelper _testOutputHelper;

        public VeiculoTests(ITestOutputHelper testOutputHelper) 
        {
            _testOutputHelper = testOutputHelper;
            _veiculo = new Veiculo();
        }

        public void Dispose()
        {
            _testOutputHelper.WriteLine("Execução do Cleanup: Limpando os objetos.");
        }

        [Fact]
        public void TestVeiculoAcelerarCom10()
        {
            //Arrange

            //Act
            _veiculo.Acelerar(10);

            //Assert
            Assert.Equal(100, _veiculo.VelocidadeAtual);
        }

        [Fact]
        public void TestVeiculoFrear()
        {
            //Arrange

            //Act
            _veiculo.Frear(10);

            //Assert
            Assert.Equal(-150, _veiculo.VelocidadeAtual);
        }

        [Fact]
        public void ValidaAlterarDadosVeiculo()
        {
            //Arrange
            var veiculo1 = CreateVeiculo("Joao", "abc-0000", "blue", "astra");
            var veiculoAlterado = CreateVeiculo("Maria", "abc-0000", "blue", "astra");

            //Act
            veiculo1.AlterarDados(veiculoAlterado);

            //Assert
            Assert.Equal(veiculo1.Proprietario, veiculoAlterado.Proprietario);
        }

        [Fact]
        public void ValidaFichaVeiculo()
        {
            //Arrange
            PopulaVeiculo("Joao", "abc-0000", "blue", "astra", TipoVeiculo.Automovel);

            //Act
            var ficha = _veiculo.ToString();

            //Assert
            Assert.Contains("Ficha veiculo:", ficha);
        }

        [Fact]
        public void ValidaProprietarioVeiculoMenos3Caracteres()
        {
            //Arrange
            string proprietario = "ab";
            
            //Assert
            Assert.Throws<FormatException>( () => new Veiculo(proprietario) );
        }

        [Fact]
        public void ValidaPlacaVeiculoQuartoCaracter()
        {
            //Arrange
            string placa = "abc0000";
            var expectedMessage = "O 4° caractere deve ser um hífen";

            //Act
            var message = Assert.Throws<FormatException>(() => new Veiculo().Placa = placa).Message;

            //Assert
            Assert.Equal(expectedMessage, message);
        }

        private Veiculo CreateVeiculo(string proprietario, string placa, string cor, 
            string modelo, TipoVeiculo tipo = TipoVeiculo.Automovel)
        {
            return new Veiculo()
            {
                Proprietario = proprietario,
                Tipo = tipo,
                Cor = cor,
                Modelo = modelo,
                Placa = placa,
            };
        }

        private void PopulaVeiculo(string proprietario, string placa, string cor,
            string modelo, TipoVeiculo tipo = TipoVeiculo.Automovel)
        {
            _veiculo.Proprietario = proprietario;
            _veiculo.Tipo = tipo;
            _veiculo.Cor = cor;
            _veiculo.Modelo = modelo;
            _veiculo.Placa = placa;
        }
    }
}