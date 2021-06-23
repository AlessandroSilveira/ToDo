using System;
using FluentValidation.TestHelper;
using NUnit.Framework;
using ToDo.Domain.Validators.Commands;

namespace ToDo.Tests.ValidatorsTests
{
    [TestFixture]
    public class MarkToDoAsDoneCommandValidatorTests
    {
        private MarkToDoAsDoneCommandValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new MarkToDoAsDoneCommandValidator();
        }
        
        [Test]
        public void User_ShouldHaveValidateForNull()
        {
            _validator.ShouldHaveValidationErrorFor (a=>a.User, null as string);
        }
        [Test]
        public void User_ShouldHaveValidateForMinLength()
        {
            _validator.ShouldHaveValidationErrorFor (a=>a.User, "te");
        }
        
        [Test]
        public void User_ShouldHaveValidateForMinLengthWithResponseMessage()
        {
            _validator.ShouldHaveValidationErrorFor (a=>a.User, "te").WithErrorMessage("Usuario deve ter pelo menos 3 caracteres");
        }
        
        [Test]
        public void Id_ShouldHaveValidateForNull()
        {
            Guid id = new Guid();
            _validator.ShouldHaveValidationErrorFor (a=>a.Id, id);
        }
        
        [Test]
        public void Id_ShouldHaveValidateForNullWithResponseMessage()
        {
            Guid id = new Guid();
            _validator.ShouldHaveValidationErrorFor (a=>a.Id, id).WithErrorMessage("Id Não pode ser nulo");
        }
        
    }
}