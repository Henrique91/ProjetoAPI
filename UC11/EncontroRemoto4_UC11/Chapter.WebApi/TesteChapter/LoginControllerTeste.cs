using Chapter.WebApi.Controllers;
using Chapter.WebApi.Interfaces;
using Chapter.WebApi.Models;
using Chapter.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteChapter
{
    public class LoginControllerTest
    {
        [Fact]
        public void LoginController_Retornar_Usuario_Invalido()
        {
            // Arrange
            var repositoryEspelhado = new Mock<IUsuarioRepository>();

            repositoryEspelhado.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns((Usuario)null);

            var controller = new LoginController(repositoryEspelhado.Object);

            LoginViewModel dadosUsuario = new LoginViewModel();
            dadosUsuario.Email = "batata@email.com";
            dadosUsuario.Senha = "batata";

            //Act

            var resultado = controller.Login(dadosUsuario);

            // Assert

            Assert.IsType<UnauthorizedObjectResult>(resultado);
        }

        [Fact]
        public void LoginController_Retorna_Token()
        {
            //Arrange - Preparação

            Usuario usuarioRetornado = new Usuario();
            usuarioRetornado.Email = "email@email.com";
            usuarioRetornado.Senha = "1234";
            usuarioRetornado.Tipo = "0";
            usuarioRetornado.Id = 1;

            var repositoryEspelhado = new Mock<IUsuarioRepository>();

            repositoryEspelhado.Setup(x => x.Login(It.IsAny<string>(), It.IsAny<string>())).Returns(usuarioRetornado);

            LoginViewModel dadosUsuario = new LoginViewModel();
            dadosUsuario.Email = "batata@email.com";
            dadosUsuario.Senha = "batata";

            var controller = new LoginController(repositoryEspelhado.Object);

            string issuerValido = "Chapter.WebApi";


            //Act - Ação

            OkObjectResult resultado = (OkObjectResult)controller.Login(dadosUsuario);

            string tokenString = resultado.Value.ToString().Split(' ')[3];

            var jwtHandler = new JwtSecurityTokenHandler();

            var tokenJwt = jwtHandler.ReadJwtToken(tokenString);

            //Assert - Verificação
            Assert.Equal(issuerValido, tokenJwt.Issuer);
        }
    }
}
