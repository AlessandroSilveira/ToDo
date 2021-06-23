using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using ToDo.Domain.Validators.Commands;

namespace ToDo.Tests.ValidatorsTests
{
    [TestFixture]
    public class UpdateTodoCommandValidatorTests
    {
        private UpdateTodoCommandValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new UpdateTodoCommandValidator();
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
        public void Title_ShouldHaveValidateForWithResponseMessage()
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
        public void User_ShouldHaveValidateForWithResponseMessage()
        {
            _validator.ShouldHaveValidationErrorFor (a=>a.User, "te" ).WithErrorMessage("Usuario deve ter pelo menos 3 caracteres");
        }
        
        
        [Test]
        public void Id_ShouldHaveValidateForNull()
        {
            var id = new Guid();
            _validator.ShouldHaveValidationErrorFor (a=>a.Id,id);
        }
        
        [Test]
        public void Id_ShouldHaveValidateForWithResponseMessage()
        {
            var id = new Guid();
            _validator.ShouldHaveValidationErrorFor (a=>a.Id,id).WithErrorMessage("Id Não pode ser nulo");
        }
    }
}