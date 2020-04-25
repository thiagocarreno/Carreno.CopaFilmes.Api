using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BinaryLab.CopaFilmes.Filme.Repositorio;
using BinaryLab.CopaFilmes.Mocks.Repositorio.Entidades;
using BinaryLab.CopaFilmes.Repositorio.Abstracoes;
using FluentAssertions;
using Moq;
using Xunit;

namespace BinaryLab.CopaFilmes.Repositorio.Tests
{
    public class FilmeRepositorioTests
    {
        public Filmes FilmesRepositorioMock { get; set; }
        public Mocks.ServicoAplicacao.DTO.Filmes FilmesSevicoAplicacaoMock { get; set; }

        public FilmeRepositorioTests()
        {
            FilmesRepositorioMock = new Filmes();
            FilmesSevicoAplicacaoMock = new Mocks.ServicoAplicacao.DTO.Filmes();
        }

        [Fact(DisplayName = "Deve Obter Filmes")]
        public void DeveObterFilmes()
        {
            var repositorioLeituraMock = new Mock<IRepositorioLeitura<Filme.Repositorio.Entidades.Filme, string>>();
            repositorioLeituraMock.Setup(r => r.Obter()).Returns(FilmesRepositorioMock.Lista);

            var filmeRepositorio = new FilmeRepositorio(repositorioLeituraMock.Object);
            var filmes = filmeRepositorio.Obter();

            filmes.Should().BeEquivalentTo(FilmesRepositorioMock.Lista);
        }

        [Fact(DisplayName = "Deve Obter Filmes Assíncronamente")]
        public async Task DeveObterFilmesAsync()
        {
            var repositorioLeituraMock = new Mock<IRepositorioLeitura<Filme.Repositorio.Entidades.Filme, string>>();
            repositorioLeituraMock.Setup(r => r.ObterAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(FilmesRepositorioMock.Lista));

            var filmeRepositorio = new FilmeRepositorio(repositorioLeituraMock.Object);
            var filmes = await filmeRepositorio.ObterAsync();

            filmes.Should().BeEquivalentTo(FilmesRepositorioMock.Lista);
        }

        [Fact(DisplayName = "Deve Obter Filmes por Ids")]
        public void DeveObterFilmesPorIds()
        {
            var repositorioLeituraMock = new Mock<IRepositorioLeitura<Filme.Repositorio.Entidades.Filme, string>>();
            repositorioLeituraMock.Setup(r => r.Obter(It.IsAny<string[]>())).Returns(FilmesRepositorioMock.OitoPrimeiros);

            var filmeRepositorio = new FilmeRepositorio(repositorioLeituraMock.Object);
            var filmes = filmeRepositorio.Obter(FilmesSevicoAplicacaoMock.OitoPrimeirosIds.ToArray());

            filmes.Should().BeEquivalentTo(FilmesRepositorioMock.OitoPrimeiros);
        }

        [Fact(DisplayName = "Deve Obter Filmes por Ids Assíncronamente")]
        public async Task DeveObterFilmesPorIdsAsync()
        {
            var repositorioLeituraMock = new Mock<IRepositorioLeitura<Filme.Repositorio.Entidades.Filme, string>>();
            repositorioLeituraMock.Setup(r => r.ObterAsync(It.IsAny<string[]>(), It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(FilmesRepositorioMock.OitoPrimeiros));

            var filmeRepositorio = new FilmeRepositorio(repositorioLeituraMock.Object);
            var filmes = await filmeRepositorio.ObterAsync(FilmesSevicoAplicacaoMock.OitoPrimeirosIds.ToArray(), CancellationToken.None);

            filmes.Should().BeEquivalentTo(FilmesRepositorioMock.OitoPrimeiros);
        }
    }
}
