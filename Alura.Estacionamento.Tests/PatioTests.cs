using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Tests
{
    public class PatioTests: IDisposable
    {
        private Veiculo _veiculo;
        private ITestOutputHelper _testOutputHelper;
        public PatioTests(ITestOutputHelper testOutputHelper)        
        {
            _veiculo = new Veiculo();
            _testOutputHelper = testOutputHelper;
        }

        public void Dispose()
        {
            _testOutputHelper.WriteLine("Execução do Cleanup: Limpando os objetos.");
        }
        [Fact]
        public void ValidaFaturamento()
        {
            //Arrange
            var patio = new Patio();
            PopulaVeiculo("Jonh", "aaa-0000","white","BMW");
            patio.RegistrarEntradaVeiculo(_veiculo);
            patio.RegistrarSaidaVeiculo(_veiculo.Placa);

            //Act
            var faturado = patio.TotalFaturado();

            //Assert
            Assert.Equal(2, faturado);
        }

        [Theory]
        [InlineData("Jonh", "aaa-0000", "Blue","Golf")]
        [InlineData("Steve", "bbb-9999", "Red", "Seat")]
        [InlineData("Mary", "ccc-3333", "White", "Smart")]
        public void ValidaFaturamentoComVariosVeiculos(string proprietario, string placa, string cor, string modelo)
        {
            //Arrange
            var patio = new Patio();
            PopulaVeiculo(proprietario, placa, cor, modelo);
            patio.RegistrarEntradaVeiculo(_veiculo);
            patio.RegistrarSaidaVeiculo(_veiculo.Placa);

            //Act
            var faturado = patio.TotalFaturado();

            //Assert
            Assert.Equal(2, faturado);
        }

        [Theory]
        [InlineData("Jonh", "aaa-0000", "Blue", "Golf")]
        public void LocalizaVeiculoNoPatio(string proprietario, string placa, string cor, string modelo)
        {
            //Arrange
            var patio = new Patio();
            var veiculo = CreateVeiculo(proprietario, placa, cor, modelo);
            patio.RegistrarEntradaVeiculo(veiculo);

            //Act
            var consultado = patio.ConsultaVeiculo(veiculo.Placa);

            //Assert
            Assert.Equal(placa, consultado.Placa);

        }

        private Veiculo CreateVeiculo(string proprietario, string placa, string cor, string modelo)
        {
            return new Veiculo()
            {
                Proprietario = proprietario,
                Tipo = Alura.Estacionamento.Modelos.TipoVeiculo.Automovel,
                Cor = cor,
                Modelo = modelo,
                Placa = placa
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
