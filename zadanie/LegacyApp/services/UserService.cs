using System;
using System.ComponentModel.DataAnnotations;
using LegacyApp.repositories;
using LegacyApp.services;
using Validator = LegacyApp.validators.Validator;

namespace LegacyApp
{


    public class UserService
    {
        private IClientRepository _clientRepository;
        private IUserCreditService _userCreditService;

        public UserService(IClientRepository clientRepository, IUserCreditService userCreditService)
        {
            _clientRepository = clientRepository;
            _userCreditService = userCreditService;
        }

        [Obsolete]
        public UserService()
        {
            _clientRepository = new ClientRepository();
            _userCreditService = new UserCreditService();
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {

            if (!Validator.validName(firstName, lastName) || !Validator.validEmail(email) || !Validator.validAge(dateOfBirth)) 
            {
                return false;
            }

            
            var clientRepository = _clientRepository;
            var client = clientRepository.GetById(clientId);

            var user = CreateUser(client, dateOfBirth, firstName, lastName, email);
            
            if (!Validator.validCredit(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
        private User CreateUser(Client client, DateTime dateOfBirth, string firstName, string lastName, string email)
        {
            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            SetCreditLimit(user); 

            return user;
        }

        private void SetCreditLimit(User user)
        {
            if (user.Client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (user.Client.Type == "ImportantClient")
            {
                using (var userCreditService = _userCreditService)
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    creditLimit = creditLimit * 2;
                    user.CreditLimit = creditLimit;
                }
            }
            else
            {
                user.HasCreditLimit = true;
                using (var userCreditService = _userCreditService)
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }
        }
    }
    
   
}
