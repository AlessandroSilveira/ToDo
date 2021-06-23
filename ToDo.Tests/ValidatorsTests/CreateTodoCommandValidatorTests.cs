using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using ToDo.Domain.Validators.Commands;

namespace ToDo.Tests.ValidatorsTests
{
    [TestFixture]
    public class CreateTodoCommandValidatorTests
    {
        private CreateTodoCommandValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CreateTodoCommandValidator();
        }

        [Test]
        public void Title_ShouldHaveValidateForNull()
        {
            _validator.ShouldHaveValidationErrorFor (a=>a.Title, null as string);
        }
        
        [Test]
        public void Title_ShouldHaveValidateForMinLength()
        {
            _validator.ShouldHaveValidationErrorFor (a=>a.Title, "te" );
        }
        
        [Test]
        public void Title_ShouldHaveValidateForMinLengthWitResponseMessage()
        {
            _validator.ShouldHaveValidationErrorFor (a=>a.Title, "te" ).WithErrorMessage("Titulo deve ter pelo menos 3 caracteres");
        }
        
        
        
        [Test]
        public void User_ShouldHaveValidateForNull()
        {
            _validator.ShouldHaveValidationErrorFor (a=>a.User, null as string);
        }
        
        [Test]
        public void User_ShouldHaveValidateForMinLength()
        {
            _validator.ShouldHaveValidationErrorFor (a=>a.User, "te" );
        }
        
        [Test]
        public void User_ShouldHaveValidateForMinLengthWitResponseMessage()
        {
            _validator.ShouldHaveValidationErrorFor (a=>a.User, "te" ).WithErrorMessage("Usuario deve ter pelo menos 3 caracteres");
        }
        
        [Test]
        public void Date_ShouldHaveValidateForNull()
        {
            var data = new DateTime();
            _validator.ShouldHaveValidationErrorFor (a=>a.Date,data);
        }
        
        [Test]
        public void Date_ShouldHaveValidateForNullWithResponseMessage()
        {
            var data = new DateTime();
            _validator.ShouldHaveValidationErrorFor (a=>a.Date,data).WithErrorMessage("Data não pode ser vazio");
        }
    }
}