using Authorization.TokenEndpoint;
using BaseLibrary;
using BaseLibrary.Responses;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using OAuth.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ScuffedAuth.Tests
{
    public class TokenServiceConfigurationTests
    {
        [Theory]
        [InlineData(60)]
        [InlineData(3600)]
        public async Task GetToken_ForProvidedExpiresInSetting_ShouldReturnTokenWithCorrectExpiresInValue(int expiresIn)
        {
            var settings = new TokenGeneratorSettings
            {
                ExpiresIn = expiresIn,
                Length = 32,
                TokenType = "Bearer"
            };
            ITokenService service = CreateTokenService(settings);
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.client_credentials
            };

            Response response = await service.GetToken(request);

            response.Should().BeOfType<SuccessResponse<TokenResource>>();
            response.As<SuccessResponse<TokenResource>>().Payload.ExpiresIn.Should().Be(expiresIn);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(32)]
        public async Task GetToken_ForProvidedEvenLengthSetting_ShouldReturnTokenWithCorrectExactLength(int length)
        {
            var settings = new TokenGeneratorSettings
            {
                ExpiresIn = 60,
                Length = length,
                TokenType = "Bearer"
            };
            ITokenService service = CreateTokenService(settings);
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.client_credentials
            };

            Response response = await service.GetToken(request);

            response.Should().BeOfType<SuccessResponse<TokenResource>>();
            response.As<SuccessResponse<TokenResource>>().Payload.AccessToken.Length.Should().Be(length);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(-1)]
        public void TokenGeneratorSettings_ForLengthLessOrEqualToOne_ShouldReturnValidationError(int length)
        {
            var settings = new TokenGeneratorSettings
            {
                ExpiresIn = 60,
                Length = length,
                TokenType = "Bearer"
            };

            var validationModel = ValidateModel(settings);

            validationModel.Should().Contain(
                x => x.MemberNames.Contains("Length")
                    && !string.IsNullOrEmpty(x.ErrorMessage)
                    && x.ErrorMessage.Contains("between"));
        }

        private static List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new System.ComponentModel.DataAnnotations.ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Theory]
        [InlineData(3, 2)]
        [InlineData(5, 4)]
        [InlineData(31, 30)]
        public async Task GetToken_ForProvidedOddLengthSetting_ShouldReturnTokenWithCorrectRoundedDownToEvenLength(int length, int expectedLength)
        {
            var settings = new TokenGeneratorSettings
            {
                ExpiresIn = 60,
                Length = length,
                TokenType = "Bearer"
            };
            ITokenService service = CreateTokenService(settings);
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.client_credentials
            };

            Response response = await service.GetToken(request);

            response.Should().BeOfType<SuccessResponse<TokenResource>>();
            response.As<SuccessResponse<TokenResource>>().Payload.AccessToken.Length.Should().Be(expectedLength);
        }

        [Theory]
        [InlineData("Bearer")]
        [InlineData("OtherTokenType")]
        public async Task GetToken_ForProvidedTokenTypeSetting_ShouldReturnTokenWithCorrectTokenType(string tokenType)
        {
            var settings = new TokenGeneratorSettings
            {
                ExpiresIn = 60,
                Length = 32,
                TokenType = tokenType
            };
            ITokenService service = CreateTokenService(settings);
            TokenRequest request = new TokenRequest
            {
                GrantType = GrantTypes.client_credentials
            };

            Response response = await service.GetToken(request);

            response.Should().BeOfType<SuccessResponse<TokenResource>>();
            response.As<SuccessResponse<TokenResource>>().Payload.TokenType.Should().Be(tokenType);
        }

        private static ITokenService CreateTokenService(TokenGeneratorSettings pSettings)
        {
            var tokenGeneratorSettings =
                Options.Create(pSettings);
            var tokenRepository = Substitute.For<ITokenRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            return new TokenService(tokenRepository, unitOfWork, new TokenGenerator(tokenGeneratorSettings), new TokenToTokenResourceMapper());
        }
    }
}
