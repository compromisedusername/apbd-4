using System;
using System.ComponentModel.DataAnnotations;

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

        //  private User createUser(Client client, DateTime dateOfBirth, string email, string firstName, string lastName)
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {

            if (!Validator.validName(firstName, lastName) || !Validator.validEmail(email) || !Validator.validAge(dateOfBirth)) 
            {
                return false;
            }

            
            var clientRepository = _clientRepository;
            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (client.Type == "VeryImportantClient")
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == "ImportantClient")
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
                using (var userCreditService = new UserCreditService())
                {
                    int creditLimit = userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                    user.CreditLimit = creditLimit;
                }
            }

            if (!Validator.validCredit(user))
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }
    }
}
