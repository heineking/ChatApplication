using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ChatApplication.Infrastructure.Contracts;
using ChatApplication.Password;
using ChatApplication.Security;
using ChatApplication.Security.Contracts;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ChatApplication.Test.Password
{
    [TestFixture]
    public class PasswordServiceTest
    {
        private IPasswordService _passwordService;
        private string _password;
        private string _hash;

        [SetUp]
        public void SetUp()
        {
            _passwordService = new PasswordService(new ConfigSettings(), new RNGCryptoServiceProvider());
            _password = "password";
            _hash = _passwordService.GeneratePasswordHash(_password);
        }

        [Test]
        public void Should_Take_500MS_To_Validate()
        {
            // arrange
            const int iterations = 5;
            var watch = System.Diagnostics.Stopwatch.StartNew();

            for (var i = 0; i < iterations; ++i)
            {
                _passwordService.ValidatePassword(_hash, _password);
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds / iterations;
            Assert.GreaterOrEqual(elapsedMs, 450);
            Assert.LessOrEqual(elapsedMs, 550);
        }

        [Test]
        public void Should_Create_Unique_Password_Hashes()
        {
            // arrange
            var hash2 = _passwordService.GeneratePasswordHash(_password);
            // assert
            Assert.AreNotEqual(_hash, hash2);
        }

        [Test]
        public void Should_Return_True_If_Passed_Correct_Password()
        {
            // arrange
            var password = "password";
            // act
            var isValid = _passwordService.ValidatePassword(_hash, password);
            // assert
            Assert.AreEqual(isValid, true);
        }

        [Test]
        public void Should_Return_False_If_Passed_Incorrect_Password()
        {
            // arrange
            var wrongPassword = "wrongPassword";
            // act
            var isValid = _passwordService.ValidatePassword(_hash, wrongPassword);
            // assert
            Assert.AreEqual(isValid, false);
        }

        [Test]
        public void Should_Return_False_Without_Key()
        {
            // arrange
            // hashed with a different key
            var hashed = "TxdHmY2Hs1XE20FTQQERwZGrbtVE2Xu6xrrwXr1hCTjNVTUEx920ijOEpPfFdZY0KrB27KEWM1dKXlOkz4wjPg==";
            // act
            var isValid = _passwordService.ValidatePassword(hashed, "password");
            // assert
            Assert.AreEqual(isValid, false);
        }
    }
}
